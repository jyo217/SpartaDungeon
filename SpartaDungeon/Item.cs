using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Item
    {
        public string name { get; }
        public string label { get; }
        public int type { get; }
        public int atk { get; }
        public int def { get; }
        public int gold { get; }
        public bool isEquiped { get; set; }

        public bool isOnSale { get; set; }

        // 아이템 클래스 - 아이템 이름, 아이템 설명, 아이템 타입(방어구, 무기), 공격력, 방어력, 가격, 
        // 장착 여부, 위치(상점인지, 인벤토리인지)
        public Item(string name, string label, int type, int atk, int def, int gold, bool isEquiped, bool isOnSale)
        {
            this.name = name; this.label = label; this.type = type; this.atk = atk; 
            this.def = def; this.gold = gold;  this.isEquiped = isEquiped; this.isOnSale = isOnSale;
        }


    }
}
