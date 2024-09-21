using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace StarRailLauncher.DataPack
{
    internal class MainWindowData
    {
        public static string JsonPath = "MainWindowData.json";

        public string BackgroundPath = "";

        public static MainWindowData Load()
        {
            if (File.Exists(JsonPath))
            {
                return JsonConvert.DeserializeObject<MainWindowData>(File.ReadAllText(JsonPath));
            }
            else
            {
                return new MainWindowData("");
            }
            
        }

        public static void Save(MainWindowData mainWindowData)
        {
            File.WriteAllText(JsonPath, JsonConvert.SerializeObject(mainWindowData, Formatting.Indented));
        }

        public MainWindowData(string BackgroundPath)
        {
            this.BackgroundPath = BackgroundPath;
        }

        public MainWindowData()
        {

        }
    }
}
