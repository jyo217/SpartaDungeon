using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    ///<summary>캐릭터 정보</summary>
    internal class Character
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set;  }

        public Item[] EquipedItems { get; set; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold, Item[] equipedItems)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            EquipedItems = equipedItems;
        }
    }
}
