using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    ///<summary>상점. 구입, 판매 기능</summary>
    internal class Shop
    {
        public Shop() {}

        public void Purchase(Item item) { item.isOnSale = false; }

        public void Selling(Item item) { item.isOnSale = true; }

        public Item[] Displaying(Item[] items)
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
    }
}
