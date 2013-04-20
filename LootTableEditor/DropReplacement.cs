using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LootTableEditor
{
    public class DropReplacement
    {
        public List<Drop> drops;
        public bool tryEachItem = true;
        public bool alsoDropDefaultLoot = false;

        public DropReplacement()
        {
            drops = new List<Drop>();
        }
    }

    public class Drop
    {
        public float chance = 1.0f;

        public int low_stack = 0;
        public int high_stack = 1;

        public int itemID;

        public int prefix;

        public Drop()
        {
            
        }
    }
}
