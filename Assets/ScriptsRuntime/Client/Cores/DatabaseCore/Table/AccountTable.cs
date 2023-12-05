namespace DC.Database {

    public class AccountTable {

        internal readonly static string TableName = "AccountTable";

        public int Id { get; set; }
        public string Name { get; set; }

        public AccountTable(string name) {
            Name = name;
        }

    }
}