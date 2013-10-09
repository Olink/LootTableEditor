using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LootTableEditor
{
    public class Config
    {
        public Dictionary<int, DropReplacement> LootReplacements;

        public Config()
        {
            LootReplacements = new Dictionary<int, DropReplacement>();
        }

        public void WriteConfig(String file)
        {
            DropReplacement replacement = new DropReplacement();
	        replacement.drops = new Dictionary<State, List<Drop>>();
			List<Drop> drops = new List<Drop>();
	        drops.Add(new Drop{chance = 1.0f, high_stack = 99, low_stack = 1, itemID = 73});
			replacement.drops.Add(State.Normal, drops);
			LootReplacements.Add(3, replacement);

            using (var tw = new StreamWriter(file))
            {
                tw.Write(JsonConvert.SerializeObject(LootReplacements, Formatting.Indented));
            }
        }

        public void ReadFile(String file)
        {
            if(!File.Exists(file))
                WriteConfig(file);

            using (var tr = new StreamReader(file))
            {
                String raw = tr.ReadToEnd();
                LootReplacements = JsonConvert.DeserializeObject<Dictionary<int, DropReplacement>>(raw);
            }
        }
    }
}
