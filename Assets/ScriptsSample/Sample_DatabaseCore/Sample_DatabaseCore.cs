using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using LiteDB;

namespace DC.Database.Sample {

    public class Sample_DatabaseCore_SampleTable {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }

    public static class Sample_DatabaseCore {

        [MenuItem("Samples/DatabaseCore/Run")]
        public static void Run() {

            var dbPath = Path.Combine(Application.dataPath, "ScriptsSample", "Sample_DatabaseCore", "Sample_DatabaseCore.db");
            var dbConnection = new LiteDatabase(dbPath);

            var table = dbConnection.GetCollection<Sample_DatabaseCore_SampleTable>();

            var sampleTable = new Sample_DatabaseCore_SampleTable {
                Id = 1,
                Name = "Sample",
                Description = "This is a sample."
            };

            table.Insert(sampleTable);

            var sampleTableList = table.FindAll().ToList();

            foreach (var sampleTableItem in sampleTableList) {
                Debug.Log(sampleTableItem.Id);
                Debug.Log(sampleTableItem.Name);
                Debug.Log(sampleTableItem.Description);
            }
        }

    }

}