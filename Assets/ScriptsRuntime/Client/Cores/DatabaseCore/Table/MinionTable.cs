namespace DC.Database {

    public class MinionTable {

        internal readonly static string TableName = "MinionTable";

        public int Id { get; set; }
        public int AccountId { get; set; }

        public MinionTable(int accountId) {
            AccountId = accountId;
        }

    }

}