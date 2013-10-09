using System;
using System.IO;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using Terraria;

namespace LootTableEditor
{
    [ApiVersion(1,14)]
    public class LootTableEditor : TerrariaPlugin
    {
        private Config config;
        private string path = "";
        public override string Author
        {
            get { return "Zack Piispanen"; }
        }

        public override string Description
        {
            get { return "Override vanilla npc loot tables"; }
        }

        public override string Name
        {
            get { return "NPC Loot table editor"; }
        }

        public override Version Version
        {
            get { return new Version(1, 1, 0, 0); }
        }

        public LootTableEditor(Main game) : base(game)
        {
            Order = 1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.NpcLootDrop.Deregister(this, OnLootDrop);
                GeneralHooks.ReloadEvent -= OnReload;
            }
        }

        public override void Initialize()
        {
            path = Path.Combine(TShock.SavePath, "LootDrop.json");
            config = new Config();
            config.ReadFile(path);
            ServerApi.Hooks.NpcLootDrop.Register(this, OnLootDrop);
            GeneralHooks.ReloadEvent += OnReload;
        }

        Random random = new Random();
        private void OnLootDrop(NpcLootDropEventArgs args)
        {
            //Debug print
            //Console.WriteLine("{0}[{1}]: ({2}, {3}) - Item:{4}", args.NPCID, args.NPCArrayIndex, args.X, args.Y,
            //      args.ItemID);

            if (config.LootReplacements.ContainsKey(args.NpcId))
            {
				DropReplacement repl = config.LootReplacements[args.NpcId];
                double rng = random.NextDouble();
                foreach(Drop d in repl.drops)
                {
                    if(d.chance >= rng)
                    {
                        var item = TShock.Utils.GetItemById(d.itemID);
                        int stack = random.Next(d.low_stack, d.high_stack + 1);
                        Item.NewItem(args.X, args.Y, item.width, item.height, d.itemID, stack, args.Broadcast, d.prefix);

                        args.Handled = true;

                        if (!repl.tryEachItem)
                            break;

                        //Debug print
                        //Console.WriteLine("{0} was replaced with {1} of {2}", args.ItemID, d.itemID, stack);
                    }
                }

                if (repl.alsoDropDefaultLoot)
                    args.Handled = true;
            }
        }

        private void OnReload(ReloadEventArgs args)
        {
            config.ReadFile(path);
        }
    }
}
