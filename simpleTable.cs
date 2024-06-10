using SimpleInterpreter.Lex;
using System.Text;

namespace SimpleInterpreter{
    public class SymbolEntry{
        public ETokenType TokenType {
             get; 
             set; 
        }
        public string Name { 
            get; 
            set;
        }
        public double? DataValue { 
            get; 
            set; 
        }

        public SymbolEntry(ETokenType tokenType, string name, double? dataValue = null){
            TokenType = tokenType;
            Name = name;
            DataValue = dataValue;
        }
    }

    public class SymbolTable{
        private int currentKey = 0;
        private Dictionary<int, SymbolEntry> entries;

        public SymbolTable(){
            entries = new Dictionary<int, SymbolEntry>();
        }

        public int Add(string name){
            return Add(name, null);
        }

        public int Add(string name, double? dataValue){
            var (existingEntry, existingKey) = GetByName(name);
            if (existingEntry != null)
                return existingKey;

            entries[++currentKey] = new SymbolEntry(ETokenType.VAR, name, dataValue);
            return currentKey;
        }

        public (SymbolEntry?, int) GetByName(string name){
            foreach (var key in entries.Keys.ToList())
            {
                if (entries[key].Name == name)
                {
                    return (entries[key], key);
                }
            }
            return (null, 0);
        }

        public double? Get(int key){
            if (!entries.ContainsKey(key))
                return null;
            return entries[key].DataValue;
        }

        public SymbolEntry? GetEntry(int key){
            if (!entries.ContainsKey(key))
                return null;
            return entries[key];
        }

        public override string ToString(){
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("ID".PadRight(5));
            stringBuilder.Append("Type".PadRight(10));
            stringBuilder.Append("Name".PadRight(15));
            stringBuilder.Append("Value".PadRight(5));
            stringBuilder.AppendLine();

            foreach (var key in entries.Keys.ToList()){
                var entry = entries[key];
                stringBuilder.Append(key.ToString().PadRight(5));
                stringBuilder.Append(entry.TokenType.ToString().PadRight(10));
                stringBuilder.Append(entry.Name.PadRight(15));
                if (entry.DataValue.HasValue)
                    stringBuilder.Append(entry.DataValue.Value.ToString().PadRight(5));
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
