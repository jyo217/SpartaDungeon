namespace SpartaDungeon
{
    internal class Program
    {
        /// <summary>
        /// 게임 시작 => 초기 화면 => 상태 확인, 인벤토리 점검, 모험 떠나기, 상점 등 여러 기능들 있음.
        /// 상태 확인에서는 현재 캐릭터의 상태를 보여준다. 
        ///캐릭터의 상태는 아이템 착용이나 모험, 상점 등의 요소로 인한 변동이 반영되어야 한다.
        ///
        ///
        /// 인벤토리 : 인벤토리의 아이템 정보는 배열로 관리한다. 각 아이템들은 구조체 또는 클래스로 만든다. 
        /// 제시된 내용 말고도 아이템 항목을 좀 추가해둘 것. 
        /// 인벤토리의 정렬 기능 구현(이름순, 공격력순, 방어력순)
        /// 레이아웃을 이용해 아이템 텍스트의 길이에 영향 없이 출력 형식을 유지할 수 있게 할 것
        ///
        /// 상점 : 아이템 사고팔기 가능. 캐릭터의 보유 골드와 아이템 정보, 가격이 표시된다.
        /// 여기에도 마찬가지로 레이아웃을 적용할 것. 이미 구매한 아이템은 구매완료 표시를 띄울 것
        /// 
        /// 여유가 된다면 콘솔 꾸미기 정도는 해 볼수도.
        /// </summary>
        public static bool projectMemo = false;

        private static Character player;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }
        
        ///<summary>게임 데이터 초기화</summary>
        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
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
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    // 작업해보기
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
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

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
            Console.Clear();

            Console.WriteLine("[인벤토리]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
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