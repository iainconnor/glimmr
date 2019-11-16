﻿using HueDream.HueControl;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace HueDream.HueDream {
    [Serializable]
    public class DreamData {
        public string iniPath = "huedream.ini";
        public string DS_IP { get; set; }
        public string HUE_IP { get; set; }
        public bool HUE_SYNC { get; set; }
        public bool HUE_AUTH { get; set; }
        public string HUE_KEY { get; set; }
        public string HUE_USER { get; set; }
        public List<KeyValuePair<int, string>> HUE_LIGHTS { get; set; }
        public List<KeyValuePair<int, string>> HUE_MAP { get; set; }

        public DreamData() {
            if (!File.Exists(iniPath)) {
                DS_IP = "0.0.0.0";
                HUE_IP = HueBridge.findBridge();
                HUE_SYNC = false;
                HUE_AUTH = false;
                HUE_KEY = "";
                HUE_USER = "";
                HUE_LIGHTS = new List<KeyValuePair<int, string>>();
                HUE_MAP = new List<KeyValuePair<int, string>>();
                var parser = new FileIniDataParser();
                IniData data = new IniData();
                data["MAIN"]["DS_IP"] = DS_IP;
                data["MAIN"]["HUE_IP"] = HUE_IP;
                data["MAIN"]["HUE_SYNC"] = HUE_SYNC ? "True" : "False";
                data["MAIN"]["HUE_AUTH"] = HUE_AUTH ? "True" : "False";
                data["MAIN"]["HUE_KEY"] = HUE_KEY;
                data["MAIN"]["HUE_USER"] = HUE_USER;
                data["MAIN"]["HUE_LIGHTS"] = JsonConvert.SerializeObject(HUE_LIGHTS);
                data["MAIN"]["HUE_MAP"] = JsonConvert.SerializeObject(HUE_MAP);
                parser.WriteFile(iniPath, data);
            } else {
                Console.Write("We have our ini!");
                loadData();
            }
        }

        public void saveData() {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);
            data["MAIN"]["DS_IP"] = DS_IP;
            data["MAIN"]["HUE_IP"] = HUE_IP;
            data["MAIN"]["HUE_SYNC"] = HUE_SYNC ? "True" : "False";
            data["MAIN"]["HUE_AUTH"] = HUE_AUTH ? "True" : "False";
            data["MAIN"]["HUE_KEY"] = HUE_KEY;
            data["MAIN"]["HUE_USER"] = HUE_USER;
            data["MAIN"]["HUE_LIGHTS"] = JsonConvert.SerializeObject(HUE_LIGHTS);
            data["MAIN"]["HUE_MAP"] = JsonConvert.SerializeObject(HUE_MAP);
            parser.WriteFile(iniPath, data);
        }

        public bool loadData() {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);
            DS_IP = data["MAIN"]["DS_IP"];
            HUE_IP = data["MAIN"]["HUE_IP"];
            HUE_SYNC = data["MAIN"]["HUE_SYNC"] == "True";
            HUE_AUTH = data["MAIN"]["HUE_AUTH"] == "True";
            HUE_KEY = data["MAIN"]["HUE_KEY"];
            HUE_USER = data["MAIN"]["HUE_USER"];
            HUE_LIGHTS = JsonConvert.DeserializeObject<List<KeyValuePair<int, string>>>(data["MAIN"]["HUE_LIGHTS"]);
            HUE_MAP = JsonConvert.DeserializeObject<List<KeyValuePair<int, string>>>(data["MAIN"]["HUE_MAP"]);
            return false;
        }
    }
}
