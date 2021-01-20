using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Szperacz.Core.Models
{
    public static class HistoryHandler
    {
        private static readonly string historyPath = "Src/historyJson.txt";

        public static void SerializeHistoryList(List<SearchModel> list)
        {
            var jsonSerializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(historyPath)) 
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jsonSerializer.Serialize(jw, list);
            }
        }

        public static List<SearchModel> DeserializeHistoryList()
        {
            return JsonConvert.DeserializeObject<List<SearchModel>>(File.ReadAllText(historyPath));
        } 
    }
}
