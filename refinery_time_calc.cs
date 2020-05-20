#region EnvironmentSetup
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineersScripting2
{
    class Program
    {
        IMyGridTerminalSystem GridTerminalSystem = null;

        IMyGridProgramRuntimeInfo Runtime = null;
        IMyProgrammableBlock Me = null;
        Action<string> Echo;

        #endregion
        #region CodeEditor


        List<IMyTerminalBlock> cargoBlockList = new List<IMyTerminalBlock>();

        //Dictionary<MyDefinitionId, string> typeCargoCache = new Dictionary<MyDefinitionId, string>();
        public Program()
        {

            List<IMyTerminalBlock> allBlocks = new List<IMyTerminalBlock>();

            GridTerminalSystem.GetBlocks(allBlocks);
            // Make loop to query all the blocks for Cargo blocks

            foreach (var block in allBlocks)
            {

                if (IsValidCargoBlock(block))
                {
                    cargoBlockList.Add(block);
                }

            }

            // Make loop to query blocks for ores.
        }

        public void Save()
        {

        }

        public void Main(string argument, UpdateType updateSource)
        {
            foreach (var cargoBlock in cargoBlockList)
            {
                if (cargoBlock.InventoryCount >= 1)
                {
                    IMyInventory inv = cargoBlock.GetInventory();
                    if (!inv.IsItemAt(0)) continue;
                    List<MyInventoryItem> itemList = new List<MyInventoryItem>();
                    inv.GetItems(itemList);
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        // Create a dictionary of all ores. Dump the rest of the items.
                        Echo($"Item is {itemList[i]}");
                    }

                }
            }


        }

        bool IsValidCargoBlock(IMyTerminalBlock tb)
        {
            if (tb == null || !tb.IsFunctional) return false;

            if (!tb.HasInventory) return false;

            return true;
        }

        #endregion
        #region EnvironmentSetup
    }
}
#endregion
