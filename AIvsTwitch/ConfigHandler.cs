using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIvsTwitch
{
    public class ConfigHandler
    {
        public static Dictionary<string, string> ConfigData;

        public static void LoadConfigFile()
        {
            ConfigData = new Dictionary<string, string>();

            string[] ConfigFile = File.ReadAllLines("config.cfg");
            foreach(string s in ConfigFile)
            {
                string[] buf = s.Split('\t');
                ConfigData.Add(buf[0], buf[1]);
            }
        }
    }
}
