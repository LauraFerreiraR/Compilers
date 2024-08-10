using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleInterpreter.Parser
{
    public class LL1Parser
    {
        private Dictionary<string, Dictionary<string, string>> parseTable;
        private Stack<string> stack;

        public LL1Parser(string parseTableFilePath)
        {
            parseTable = new Dictionary<string, Dictionary<string, string>>();
            stack = new Stack<string>();
            LoadParseTable(parseTableFilePath);
        }

        private void LoadParseTable(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                var row = lines[i].Split(',');
                var nonTerminal = row[0];

                parseTable[nonTerminal] = new Dictionary<string, string>();

                for (int j = 1; j < headers.Length; j++)
                {
                    parseTable[nonTerminal][headers[j]] = row[j];
                }
            }
        }

        public bool Parse(List<string> tokens)
        {
            stack.Push("S"); 
            int index = 0;

            while (stack.Count > 0)
            {
                var top = stack.Pop();

                if (IsTerminal(top))
                {
                    if (top == tokens[index])
                    {
                        index++;
                    }
                    else
                    {
                        Console.WriteLine($"Erro de sintaxe: esperado '{top}', mas encontrado '{tokens[index]}'");
                        return false;
                    }
                }
                else if (parseTable.ContainsKey(top) && parseTable[top].ContainsKey(tokens[index]))
                {
                    var production = parseTable[top][tokens[index]];
                    if (production != "ε") 
                    {
                        var symbols = production.Split(' ');
                        for (int i = symbols.Length - 1; i >= 0; i--)
                        {
                            stack.Push(symbols[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Erro de sintaxe: inesperado '{tokens[index]}'");
                    return false;
                }
            }

            if (index == tokens.Count)
            {
                Console.WriteLine("Entrada aceita com sucesso!");
                return true;
            }
            else
            {
                Console.WriteLine("Erro: entrada não consumida completamente.");
                return false;
            }
        }

        private bool IsTerminal(string symbol)
        {
            return !parseTable.ContainsKey(symbol);
        }
    }
}
