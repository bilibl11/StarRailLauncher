using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StarRailLauncher.DataPack
{
    public class PathData
    {
        public static string JsonPath = "PathData.json";

        public string gameExePath = "";

        public string modExePath = "";

        public string modManagerExePath = "";

        public static PathData Load()
        {
            if (File.Exists(JsonPath))
            {
                return JsonConvert.DeserializeObject<PathData>(File.ReadAllText(JsonPath));
            }
            else
            {
                return new PathData("","","");
            } 

        }

        public static void Save(string data,string dataType)
        {
            PathData pathData = Load();
            switch(dataType)
            {
                case "gameExePath":
                    {
                        pathData.gameExePath = data;
                        break;
                    }
                case "modExePath":
                    {
                        pathData.modExePath = data;
                        break;
                    }
                case "modManagerExePath":
                    {
                        pathData.modManagerExePath = data;
                        break;
                    }
                default: break;
            }
            File.WriteAllText(JsonPath, JsonConvert.SerializeObject(pathData, Formatting.Indented));
        }

        public PathData(string gameExePath,string modExePath, string modManagerExePath)
        {
            this.gameExePath = gameExePath;
            this.modExePath = modExePath;
            this.modManagerExePath = modManagerExePath;
        }

        public PathData()
        {
        }
    }
}
