using System;
using System.Collections.Generic;
using SimpleInterpreter.Lex;
using SimpleInterpreter.Parser;

namespace SimpleInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string parseTablePath = "ParserTables/parse_table.csv";
            var parser = new LL1Parser(parseTablePath);

            List<string> tokens = new List<string> { "VAR", "AT", "NUM", "EOF" };
            
        
            bool result = parser.Parse(tokens);

            if (result)
                Console.WriteLine("Análise sintática concluída com sucesso!");
            else
                Console.WriteLine("Análise sintática falhou.");
        }
    }
}
