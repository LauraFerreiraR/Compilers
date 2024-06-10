namespace SimpleInterpreter.Lex
{
    public class Lexer{
        public int LineNumber { 
            get; 
            protected set; 
            }
        public int ColumnNumber { 
            get; 
            protected set; 
            }
        public string FilePath { 
            get; 
            protected set; 
            }
        public SymbolTable Symbols { 
            get; p
            rotected set; 
            }

        private char? currentChar;
        private StreamReader fileReader;

        public Lexer(string filePath, SymbolTable? symbolTable = null){
            FilePath = filePath;
            Symbols = symbolTable ?? new SymbolTable();
            fileReader = new StreamReader(FilePath);
            LineNumber = ColumnNumber = 1;
        }

        public Token GetNextToken(){
            if (fileReader.EndOfStream)
                return new Token(TokenType.EndOfFile);

            while (currentChar == null || currentChar == ' ' || currentChar == '\t' || currentChar == '\r'){
                currentChar = NextChar();
            }

            switch (currentChar){
                case '+':
                    currentChar = null;
                    return new Token(TokenType.Plus);
                case '-':
                    currentChar = null;
                    return new Token(TokenType.Minus);
                case '*':
                    currentChar = null;
                    return new Token(TokenType.Multiply);
                case '/':
                    currentChar = null;
                    return new Token(TokenType.Divide);
                case '(':
                    currentChar = null;
                    return new Token(TokenType.OpenParen);
                case ')':
                    currentChar = null;
                    return new Token(TokenType.CloseParen);
                case '=':
                    currentChar = null;
                    return new Token(TokenType.Equals);
                case '\n':
                    currentChar = null;
                    ColumnNumber = 1;
                    LineNumber++;
                    return new Token(TokenType.NewLine);
            }

            if (currentChar == '$'){
                var variableName = "";
                do{
                    currentChar = NextChar();
                    if (char.IsLetter(currentChar.Value))
                        variableName += currentChar;
                } 
                while (char.IsLetter(currentChar.Value));
                var identifier = Symbols.Add(variableName);
                return new Token(TokenType.Variable, identifier);
            }

            if (currentChar == 'r'){
                if (CheckSuffix("ead")){
                    if (NextChar() == ' '){
                        if (NextChar() == '$'){
                            var varName = "";
                            do{
                                currentChar = NextChar();
                                if (char.IsLetter(currentChar.Value))
                                    varName += currentChar;
                            } 
                            while (char.IsLetter(currentChar.Value));

                            var inputValue = Console.ReadLine();
                            var key = Symbols.Add(varName, int.Parse(inputValue));
                        }
                    }
                    return new Token(TokenType.Input);
                }
            }

            if (currentChar == 'w'){
                if (CheckSuffix("rite"))
                    return new Token(TokenType.Output);
            }

            if (char.IsDigit(currentChar.Value)){
                var number = 0;
                do{
                    number = number * 10 + ConvertToInt(currentChar);
                    currentChar = NextChar();
                } 
                while (char.IsDigit(currentChar.Value));

                return new Token(TokenType.Number, number);
            }

            return new Token(TokenType.Error);
        }

        private char NextChar(){
            ColumnNumber++;
            return fileReader.EndOfStream ? '\0' : (char)fileReader.Read();
        }

        private bool CheckSuffix(string suffix){
            var match = true;
            foreach (var ch in suffix){
                currentChar = NextChar();
                if (currentChar != ch){
                    match = false;
                    break;
                }
            }
            currentChar = null;
            return match;
        }

        private int ConvertToInt(char? character){
            return character - '0';
        }
    }
}
