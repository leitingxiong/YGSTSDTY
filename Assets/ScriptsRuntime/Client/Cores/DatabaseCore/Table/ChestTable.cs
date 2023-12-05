namespace DC.Database {

    // ChestTable
    public class ChestTable {

        internal readonly static string TableName = "ChestTable";

        public int Id { get; set; }
        public int AccountId { get; set; }

        public ChestTable(int accountId) {
            AccountId = accountId;
        }

    }

}