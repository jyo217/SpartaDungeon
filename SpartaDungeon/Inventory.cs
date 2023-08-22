using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    ///<summary>인벤토리. 장비 장착, 해제, 정렬 기능/// </summary>
    internal class Inventory
    {
        public Inventory(){}

        public Item Equip(Item item) { item.isEquiped = true; return item; }
        public Item Dequip(Item item) { item.isEquiped = false; return item; }
        public Item[] SortInventory(int sortOrder, Item[] items)
        {
            switch (sortOrder)
            {
                case 1: { return SortDescLabelLength(items); }
                case 2: { return SortDescAtk(items); }
                case 3: { return SortDescDef(items); }
                default: { return items; }
            }
        }

        public Item[] InitSort(Item[] items)
        {
            Item temp;

            for (int i = 0; i < items.Length; i++)
            {
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (items[i].gold < items[j].gold)
                    {
                        temp = items[i];
                        items[i] = items[j];
                        items[j] = temp;
                    }
                }
            }
            return items;
        }

        private Item[] SortDescLabelLength(Item[] items)
        {
            Item temp;

            for (int i = 0; i < items.Length; i++)
            {
                for (int j = i+1; j < items.Length; j++)
                {
                    if (items[i].label.Length < items[j].label.Length) {
                        temp = items[i];
                        items[i] = items[j];
                        items[j] = temp;
                    }
                }
            }
            return items;
        }

        private Item[] SortDescAtk(Item[] items)
        {
            Item temp;

            for (int i = 0; i < items.Length; i++)
            {
                for (int j = i+1; j < items.Length; j++)
                {
                    if (items[i].atk < items[j].atk)
                    {
                        temp = items[i];
                        items[i] = items[j];
                        items[j] = temp;
                    }
                }
            }
            return items;
        }

        private Item[] SortDescDef(Item[] items)
        {
            Item temp;

            for (int i = 0; i < items.Length; i++)
            {
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (items[i].def < items[j].def)
                    {
                        temp = items[i];
                        items[i] = items[j];
                        items[j] = temp;
                    }
                }
            }
            return items;
        }
    }
}
