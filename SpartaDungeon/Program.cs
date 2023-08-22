using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace SpartaDungeon
{
    internal class Program
    {
        //아이템 장착,탈착 및 교체 + 상태 반영, 인벤토리 정렬 3종

        /// 게임 시작 => 초기 화면 => 상태 확인, 인벤토리 점검, 상점 등 여러 기능들 있음.
        /// 상태 확인에서는 현재 캐릭터의 상태를 보여준다. 
        /// 캐릭터의 상태는 아이템 착용이나 모험, 상점 등의 요소로 인한 변동이 반영되어야 한다.
        ///
        ///
        /// 인벤토리 : 인벤토리의 아이템 정보는 배열로 관리한다. 각 아이템은 클래스로 만든다. 
        /// 제시된 내용 말고도 아이템 항목을 좀 추가해둘 것. 
        /// 인벤토리의 정렬 기능 구현(이름순, 공격력순, 방어력순)
        /// 레이아웃을 이용해 아이템 텍스트의 길이에 영향 없이 출력 형식을 유지할 수 있게 할 것
        /// 
        /// (한글이다 보니, 단순히 PadLeft,PadRight 만으로는 제대로 기능하지 않음. 
        /// 다른 방법을 찾긴 했지만 조율 과정이 복잡해보여서 스킵했다)
        ///
        /// 
        /// 상점 : 아이템 사고팔기 가능. 캐릭터의 보유 골드와 아이템 정보, 가격이 표시된다.
        /// 아이템 판매, 구매 가능. 이미 구매한 아이템 구매 시도, 골드 부족, 구매 완료 시 각각 메시지 출력함.
        /// 판매 시 장착 중이던 아이템은 강제로 해제된 뒤 판매하게 된다
        /// 보유 중인 아이템은 판매하지 않도록 함. 상점의 아이템 리스트는 모든 종류의 아이템들을 포함함.
        /// 
        /// 
        /// 파일입출력? 으로 진행사항 저장하는 기능은 해보고 싶긴 한데, 여전히 다른 과제가 밀려 있어서 힘들겠다.
        /// 
        /// 캐릭터 클래스 - 이름, 직업(string), 레벨, 공격력, 방어력, 체력, 돈(int), 장착 아이템 배열
        /// 
        /// 
        /// 인벤토리 클래스 - 따로 가지는 건 없음. 아이템 정렬, 아이템 장착 및 탈착 기능만. 
        /// 
        /// 
        /// 아이템 클래스 - 아이템 이름, 아이템 설명, 아이템 타입(방어구, 무기), 공격력, 방어력, 가격

        public static int ARMOR = 0;
        public static int WEAPON = 1;

        public static int MaxItemList = 8;

        private static Inventory inventory = new Inventory();
        private static Character player;
        private static Shop shop;
        private static Item[] itemList = new Item[MaxItemList];

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }
        
        ///<summary>게임 데이터 초기화</summary>
        static void GameDataSetting()
        {
            // 언젠가 파일 입출력으로 저장된 데이터 가져오는 부분 추가할 예정

            //아이템 정보 세팅
            Item item1 = new Item("부서져가는 갑옷", "이걸 입고 어떻게 싸우란 거지?", ARMOR, 0, 2, 100, false, false);
            Item item2 = new Item("낡은 검", "헐값에 구한, 흔해빠진 낡은 검이다.", WEAPON, 5, 0, 400, false, false);
            Item item3 = new Item("누비 갑옷", "대부분 천과 솜으로 이루어졌지만, 꽤 튼튼하다.", ARMOR, 0, 7, 600, false, true);
            Item item4 = new Item("쓸만한 강철검", "사용감은 있지만, 아직 쓸만하다.", WEAPON, 8, 0, 900, false, true);
            Item item5 = new Item("쓸만한 강철 갑옷", "잘 관리된 강철 갑옷.", ARMOR, 0, 14, 1300, false, true);
            Item item6 = new Item("제련된 흑철 갑옷", "매우 튼튼한 흑철로 된 갑옷이다.", ARMOR, 0, 23, 2400, false, true);
            Item item7 = new Item("제련된 흑철검", "한 눈에 봐도 예사롭지 않은 검이다.", WEAPON, 15, 0, 2500, false, true);
            Item item8 = new Item("집행", "부러진 검. 그나마 남아 있는 검신에는 희미하게 새겨진 글씨가 보인다.", WEAPON, 1, 0, 3000, false, false);


            itemList[0] = item1; itemList[1] = item2; itemList[2] = item3; itemList[3] = item4;
            itemList[4] = item5; itemList[5] = item6; itemList[6] = item7; itemList[7] = item8;


            // 캐릭터 정보 세팅
            Item[] equipments = new Item[2] { null, null};
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500, equipments);

            
            inventory = new Inventory();
            shop = new Shop();
        }

        ///<summary>초기 화면 출력</summary>
        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;

                case 3:
                    DisplayShop();
                    break;
            }
        }

        ///<summary>상태보기 화면 출력</summary>
        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("[상태보기]");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");

            if (player.EquipedItems[WEAPON] == null) { Console.WriteLine($"공격력 :{player.Atk}"); }
            else { Console.WriteLine($"공격력 :{player.Atk} (+ {player.EquipedItems[WEAPON].atk})"); }
            if (player.EquipedItems[ARMOR] == null) { Console.WriteLine($"방어력 : {player.Def}"); }
            else { Console.WriteLine($"방어력 : {player.Def} (+ {player.EquipedItems[ARMOR].def})"); }

            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");

            if (player.EquipedItems[ARMOR] == null) {Console.WriteLine($"장착한 방어구 : 없음");}
            else { Console.WriteLine($"장착한 방어구 : {player.EquipedItems[ARMOR].name}"); }
            if (player.EquipedItems[WEAPON] == null) { Console.WriteLine($"장착한 무기 : 없음"); }
            else { Console.WriteLine($"장착한 무기 : {player.EquipedItems[WEAPON].name}"); }

            Console.WriteLine("\n0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        ///<summary>인벤토리 화면 출력</summary>
        static void DisplayInventory()
        {
            itemList = inventory.InitSort(itemList);

            Console.Clear();

            Console.WriteLine("[인벤토리]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            //아이템 배열 내용 중, 인벤토리 목록만 출력
            if(itemList.Length > 0) {
                int count = 0;
                Console.WriteLine("[아이템 목록]\n");
                foreach (Item i in itemList)
                {
                    if (!(i.isOnSale))
                    {
                        //장착한 아이템이라면
                        if (i.isEquiped) { Console.Write("[E]"); }
                        Console.Write($"{i.name}".PadRight(15) + "|");

                        //아이템 타입에 따른 능력 표시
                        if (i.type == ARMOR) { Console.Write($" 방어력 + {i.def}".PadRight(15) + "|"); }
                        else if (i.type == WEAPON) { Console.Write($" 공격력 + {i.atk}".PadRight(15) + "|"); }
                        Console.WriteLine($"{i.label}".PadRight(30));
                    }
                }

                Console.WriteLine();
                Console.WriteLine("2. 정렬하기");
                Console.WriteLine("1. 장착하기");
                Console.WriteLine("0. 나가기");

                int input = CheckValidInput(0, 3);

                switch (input)
                {
                    case 2:
                        SortInventory();
                        break;
                    case 1:
                        ManageEquipments();
                        break;
                    case 0:
                        DisplayGameIntro();
                        break;
                }
            }
            else
            {
                Console.WriteLine("***보유한 아이템이 존재하지 않습니다*** \n");
                Console.WriteLine("0. 나가기");

                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                }
            }
        }

        ///<summary>상점 화면 출력</summary>
        static void DisplayShop()
        {
            itemList = inventory.InitSort(itemList);

            Console.Clear();

            Console.WriteLine("[상점]");
            Console.WriteLine("아이템을 구입하거나 판매할 수 있습니다.");
            Console.WriteLine();

            //아이템 배열 내용 전체 출력, 플레이어가 보유 중인 아이템은 별도의 표시 추가
            if (itemList.Length > 0)
            {
                Console.WriteLine("[아이템 목록]\n");
                foreach (Item i in itemList)
                {
                    //보유한 아이템이라면
                    if (!(i.isOnSale)) { Console.Write("[보유 중]"); }

                    //아이템 이름 표시
                    Console.Write($"{i.name}".PadRight(15) + "|");

                    //아이템 타입에 따른 능력 표시
                    if (i.type == ARMOR) { Console.Write($" 방어력 + {i.def}".PadRight(15) + "|"); }
                    else if (i.type == WEAPON) { Console.Write($" 공격력 + {i.atk}".PadRight(15) + "|"); }
                    Console.WriteLine($"{i.label}".PadRight(30));
                }

                Console.WriteLine();
                Console.WriteLine("2. 판매하기");
                Console.WriteLine("1. 구매하기");
                Console.WriteLine("0. 나가기");

                int input = CheckValidInput(0, 3);

                switch (input)
                {
                    case 2:
                        Selling();
                        break;
                    case 1:
                        Purchase();
                        break;
                    case 0:
                        DisplayGameIntro();
                        break;
                }
            }
            else
            {
                Console.WriteLine("***보유한 아이템이 존재하지 않습니다*** \n");
                Console.WriteLine("0. 나가기");

                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                }
            }
        }

        /// <summary> 상점에 원가의 85% 로 판매. 플레이어 보유 아이템만 표시, 장착 중인 아이템이라면 탈착 후 판매 </summary>
        static void Selling()
        {
            double sellingPercentage = 85 / 100d;
            bool isEnd = false;
            int price = 0;
            int count = 0;

            while (!isEnd)
            {
                Console.Clear();

                Console.WriteLine("[판매]");
                Console.WriteLine("판매할 아이템을 선택하세요.");
                Console.WriteLine();

                if (itemList.Length > 0)
                {
                    Console.WriteLine("[아이템 목록]\n");
                    foreach (Item i in itemList)
                    {
                        if (!(i.isOnSale))
                        {
                            count++;

                            Console.Write($"{count}. ");

                            price = (int)(i.gold * sellingPercentage);
                            //장착한 아이템이라면
                            if (i.isEquiped) { Console.Write("[E]"); }
                            Console.Write($"{i.name}".PadRight(15) + "|");

                            //아이템 타입에 따른 능력 표시, 가격 표시
                            if (i.type == ARMOR) { Console.Write($" 방어력 + {i.def}".PadRight(15) + "|"); }
                            else if (i.type == WEAPON) { Console.Write($" 공격력 + {i.atk}".PadRight(15) + "|"); }
                            Console.WriteLine($"판매가 : {price}".PadRight(15));
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");

                    int input = CheckValidInput(0, count);

                    switch (input)
                    {
                        case 0:
                            isEnd = true;
                            break;
                        default:
                            {
                                count = 0;
                                Item thisItem;

                                for (int i = 0; i < itemList.Length; i++)
                                {
                                    if (!(itemList[i].isOnSale))//인벤토리에 들어있는
                                    {
                                        count++;
                                        thisItem = itemList[i];
                                        if (count == input)//지정한 n번째 장비일 때
                                        {
                                            if (thisItem.isEquiped)//장착 중인 장비라면
                                            {
                                                thisItem = DequipItem(thisItem);//장착 해제 후
                                            }

                                            shop.Selling(thisItem);
                                            itemList[i] = thisItem;
                                            price = (int)(thisItem.gold * sellingPercentage);

                                            player.Gold += price;
                                        }
                                    }
                                }

                                Selling();
                                break;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("***보유한 아이템이 존재하지 않습니다*** \n");
                    Console.WriteLine("0. 나가기");

                    int input = CheckValidInput(0, 0);
                    switch (input)
                    {
                        case 0:
                            isEnd = true;
                            break;
                    }
                }
            }

            DisplayGameIntro();
        }

        
        /// <summary> 상점에서 구입. 구입 가능 아이템만 표시</summary>
        static void Purchase()
        {
            bool isEnd = false;

            
            int flag = 0;//1 : 구입완료, 2 : 골드 부족, 3 : 이미 보유중

            int price;
            int count;

            while (!isEnd)
            {
                count = 0;

                Console.Clear();

                Console.WriteLine("[구입]");
                Console.WriteLine("구입할 아이템을 선택하세요.");
                Console.WriteLine();

                if (itemList.Length > 0)
                {
                    Console.WriteLine("[아이템 목록]\n");
                    foreach (Item i in itemList)
                    {
                        price = i.gold;
                        count++;

                        Console.Write($"{count}. ");
                        Console.Write($"{i.name}".PadRight(15) + "|");

                        //아이템 타입에 따른 능력 표시, 가격 표시
                        if (i.type == ARMOR) { Console.Write($" 방어력 + {i.def}".PadRight(15) + "|"); }
                        else if (i.type == WEAPON) { Console.Write($" 공격력 + {i.atk}".PadRight(15) + "|"); }
                        Console.Write($"가격 : {price}".PadRight(15));

                        if (!(i.isOnSale))
                        {
                            Console.Write("구매완료");
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine();

                    switch (flag)
                    {
                        case 1:
                            Console.WriteLine("**구매를 완료했습니다!**\n"); flag = 0;
                            break;
                        case 2:
                            Console.WriteLine("**골드가 부족합니다!**\n"); flag = 0;
                            break;
                        case 3:
                            Console.WriteLine("**이미 구매한 아이템입니다!**\n"); flag = 0;
                            break;
                        default: break;
                    }

                    Console.WriteLine("0. 나가기");

                    int input = CheckValidInput(0, count);

                    switch (input)
                    {
                        case 0:
                            isEnd = true;
                            break;
                        default:
                            {
                                count = 0;
                                Item thisItem;

                                for (int i = 0; i < itemList.Length; i++)
                                {
                                    if (itemList[i].isOnSale)//판매 중인 아이템들 중
                                    {
                                        count++;
                                        if (count == input)//지정한 n번째 장비일 때
                                        {
                                            thisItem = itemList[i];

                                            if (thisItem.isOnSale != true)//플래그 설정 - 보유 중
                                            {
                                                flag = 3;
                                                break;
                                            }
                                            else if (thisItem.gold > player.Gold)//플래그 설정 - 골드 부족 
                                            {
                                                flag = 2;
                                                break;
                                            }
                                            else//구매 처리 => 플래그 설정 - 구매 완료
                                            {
                                                shop.Purchase(thisItem);
                                                price = thisItem.gold;
                                                player.Gold -= price;

                                                flag = 1;
                                                break;
                                            }
                                            
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("***보유한 아이템이 존재하지 않습니다*** \n");
                    Console.WriteLine("0. 나가기");

                    int input = CheckValidInput(0, 0);
                    switch (input)
                    {
                        case 0:
                            isEnd = true;
                            break;
                    }
                }
            }

            DisplayGameIntro();
        }

        /// <summary>
        /// 인벤토리 정렬. 1번 - 라벨 길이 내림차순, 2번 - 공격력 내림차순, 3번 - 방어력 내림차순
        /// </summary>
        static void SortInventory()
        {
            bool isEnd = false;

            while (!isEnd)
            {
                Console.Clear();

                Console.WriteLine("[인벤토리]");
                Console.WriteLine("정렬 방식을 선택하세요.");
                Console.WriteLine();

                if (itemList.Length > 0)
                {
                    int count = 0;
                    Console.WriteLine("[아이템 목록]\n");
                    foreach (Item i in itemList)
                    {
                        if (!(i.isOnSale))
                        {
                            count++;

                            //장착한 아이템이라면
                            if (i.isEquiped) { Console.Write("[E]"); }
                            Console.Write($"{i.name}".PadRight(15) + "|");

                            //아이템 타입에 따른 능력 표시
                            if (i.type == ARMOR) { Console.Write($" 방어력 + {i.def}".PadRight(15) + "|"); }
                            else if (i.type == WEAPON) { Console.Write($" 공격력 + {i.atk}".PadRight(15) + "|"); }
                            Console.WriteLine($"{i.label}".PadRight(30));
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("3. 방어력 높은 순으로 정렬하기");
                    Console.WriteLine("2. 공격력 높은 순으로 정렬하기");
                    Console.WriteLine("1. 설명 긴 순으로 정렬하기");
                    Console.WriteLine("0. 나가기");

                    int input = CheckValidInput(0, 3);

                    switch (input)
                    {
                        case 0:
                            isEnd = true;
                            break;
                        default:
                            itemList = inventory.SortInventory(input, itemList);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("***보유한 아이템이 존재하지 않습니다*** \n");
                    Console.WriteLine("0. 나가기");

                    int input = CheckValidInput(0, 0);
                    switch (input)
                    {
                        case 0:
                            isEnd = true;
                            break;
                    }
                }
            }

            DisplayGameIntro();
        }

        ///<summary>장비 장착, 탈착, 교체 총괄</summary>
        static void ManageEquipments()
        {
            Console.Clear();

            Console.WriteLine("[인벤토리]");
            Console.WriteLine("장착 / 장착 해제 할 아이템을 선택하세요.");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]\n");

            int count = 0;

            foreach (Item i in itemList)
            {
                if (!(i.isOnSale))
                {
                    count++;
                    Console.Write($"{count}. ");
                    //장착한 아이템이라면
                    if (i.isEquiped) { Console.Write("[E]"); }
                    Console.Write($"{i.name}".PadRight(15) + "|");

                    //아이템 타입에 따른 능력 표시
                    if (i.type == ARMOR) { Console.Write($" 방어력 + {i.def}".PadRight(15) + "|"); }
                    else if (i.type == WEAPON) { Console.Write($" 공격력 + {i.atk}".PadRight(15) + "|"); }
                    Console.WriteLine($"{i.label}".PadRight(30));
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, count);

            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                default:
                    {
                        count = 0;
                        int sameTypeEquipment = -999;
                        Item thisItem;
                        
                        for (int i = 0; i < itemList.Length; i++)
                        {                          
                            if (!(itemList[i].isOnSale))//인벤토리에 들어있는
                            {
                                count++; 
                                thisItem = itemList[i];
                                if (count == input)//지정한 n번째 장비일 때
                                {
                                    if (thisItem.isEquiped)//장착 중인 장비라면
                                    {
                                        thisItem = DequipItem(thisItem);//그대로 해제한다.
                                    }
                                    else//장착 중인 장비가 아니라면
                                    {
                                        //해당 타입의 다른 장비를 장착 중인지 확인
                                        for(int j = 0; j < itemList.Length; j++)
                                        {
                                            if (itemList[j].isEquiped && itemList[j].type == thisItem.type) 
                                            { sameTypeEquipment = j; }
                                        }

                                        //해당 타입의 다른 장비를 장착 중이라면 기존 장비를 해제하고 장착
                                        if (sameTypeEquipment != -999)
                                        {
                                            itemList[sameTypeEquipment] = DequipItem(itemList[sameTypeEquipment]);
                                            thisItem = EquipItem(thisItem);
                                        }
                                        //해당 타입의 다른 장비를 장착하고 있지 않다면 그대로 장착
                                        else { thisItem = EquipItem(thisItem); }                                    }

                                    itemList[i] = thisItem;
                                }
                            }
                        }

                        ManageEquipments();
                        break;
                    }
            }
        }

        ///<summary>ManageEquipments 부속 기능. 장비 장착과 교체</summary>
        static Item EquipItem(Item item)
        {
            item = inventory.Equip(item);

            if (item.type == ARMOR)
            {
                player.EquipedItems[ARMOR] = item;
                player.Def += item.def;
            }
            else
            {
                player.EquipedItems[WEAPON] = item;
                player.Atk += item.atk;
            }

            return item;
        }

        ///<summary>ManageEquipments 부속 기능. 장비 탈착</summary>
        static Item DequipItem(Item item)
        {
            item = inventory.Dequip(item);

            if (item.type == ARMOR)
            {
                player.EquipedItems[ARMOR] = null;
                player.Def -= item.def;
            }
            else
            {
                player.EquipedItems[WEAPON] = null;
                player.Atk -= item.atk;
            }

            return item;
        }

        ///<summary>최소값 이상, 최대값 이하의 유효값을 입력받을 때 까지 Console.ReadLine() 반복, 올바른 입력값 반환</summary>>
        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}