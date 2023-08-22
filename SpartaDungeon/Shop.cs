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
    }
}
