namespace SimpleInterpreter.Lex{
    public class TokenData{

        public TokenData(ETokenType tokenType, int? tokenValue = null){
            TokenType = tokenType;
            TokenValue = tokenValue;
        }

        public ETokenType TokenType { 
            get; 
            set; }
        public int? TokenValue { 
            get; 
            set; }
    }
}
