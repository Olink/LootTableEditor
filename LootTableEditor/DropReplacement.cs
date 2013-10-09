using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LootTableEditor
{
    public class DropReplacement
    {
		public Dictionary<State, List<Drop>> drops;
        public bool tryEachItem = true;
        public bool alsoDropDefaultLoot = false;

        public DropReplacement()
        {
			drops = new Dictionary<State, List<Drop>>();
        }
    }

	public enum State
	{
		Normal,
		Bloodmoon,
		Eclipse,
		Night,
		Day,
		Fullmoon
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
