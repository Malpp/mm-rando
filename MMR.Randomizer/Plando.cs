using System.Collections.Generic;
using System.Linq;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Rom;

namespace MMR.Randomizer
{
    public static class Plando
    {
        public static void Modify(RandomizedResult result)
        {
            result.ChangeGossipText(GossipQuote.TerminaGossipLarge, "Call me a gamer in discord");
            result.AddItem(Item.MaskBunnyHood, Item.HeartPieceSouthClockTown);
            result.AddItem(Item.ItemHookshot, Item.ChestSouthClockTownRedRupee);
        }

        private static void ChangeGossipText(this RandomizedResult result, GossipQuote quote, string message)
        {
            result.GossipQuotes.RemoveAll(entry => entry.Id == (int) quote);

            result.GossipQuotes.Add(new MessageEntry {Id = (ushort) quote, Message = FixMessage(message)});
        }

        private static string FixMessage(string message)
        {
            ushort soundEffectId = 0x690C;

            string sfx = $"{(char) ((soundEffectId >> 8) & 0xFF)}{(char) (soundEffectId & 0xFF)}";

            return $"\x1E{sfx}{message}\xBF".Wrap(35, "\x11");
        }

        private static void AddItem(this RandomizedResult result, Item item, Item location)
        {
            var oldItemIndex = result.ItemList.FindIndex(o => o.NewLocation == location);
            var newItemIndex = result.ItemList.FindIndex(o => o.ID == (int) item);

            result.ItemList[oldItemIndex].NewLocation = result.ItemList[newItemIndex].NewLocation;
            result.ItemList[newItemIndex].NewLocation = location;
        }
    }
}