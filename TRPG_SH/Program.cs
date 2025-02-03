using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
enum Lobby_info
{
    Status=1,
    Inventory,
    Store
}
enum Classes
{
    전사 = 1,
    마법사,
    궁수,
    도적
}
namespace TRPG_SH
{
    internal class Program
    {
        static Player player = new Player();
        static private void Setup()
        {
            Console.WriteLine("원하시는 캐릭터의 이름을 입력해주세요");
            string name = Console.ReadLine();
            player.Name = name;
            Console.Clear();
            Console.WriteLine("원하시는 직업을 선택해주세요\n");
            Console.WriteLine("1. 전사\n2. 마법사\n3. 궁수\n4. 도적");

            string choice = Console.ReadLine();

            if(int.TryParse(choice, out int Value)&&Enum.IsDefined(typeof(Classes),Value))
            {
                Classes classes = (Classes)Value;
                player.Class = classes.ToString();

                switch(classes) // 추후에 마개조(?)인지 아닌지 컨펌,, 직업마다, 세팅 설정
                {
                    case Classes.전사:
                        // 전사
                        break;
                    case Classes.마법사:
                        // 마법사
                        break;
                    case Classes.궁수:
                        //궁수
                        break;
                    case Classes.도적:
                        //도적
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 값을 입력 하셨습니다.");
            }
        }
        static private void Lobby()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이 곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n");
                Console.Write("원하시는 행동을 입력해주세요. \n >> ");

                string choice = Console.ReadLine();


                if (int.TryParse(choice, out int Value) && Enum.IsDefined(typeof(Lobby_info), Value))
                {
                    Lobby_info enumValue = (Lobby_info)Value;

                    switch (enumValue)
                    {
                        case Lobby_info.Status:
                            ShowStatus();
                            //status class
                            break;
                        case Lobby_info.Inventory:
                            ShowInventory();
                            //inven
                            break;
                        case Lobby_info.Store:
                            ShowStore();
                            // store
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력을 하셨습니다.");
                }
                static void ShowStatus()
                {
                    Console.Clear();
                    player.DisplayStatus();
                    Console.ReadKey();
                }
                static void ShowInventory()
                {

                }
                static void ShowStore()
                {

                }
            }
        }

        class Player
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Level { get; private set; } = 1;
            public int Stat_Offense { get; private set; } = 10;
            public int Stat_Defense { get; private set; } = 5;
            public float HP { get; private set; } = 100;
            public int Gold { get; private set; } = 1500;

            public Player()
            {
                Name = "NoName";
                Class = Classes.전사.ToString(); 
            }

            
            public void DisplayStatus()
            {
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine("Lv. "+Level);
                Console.WriteLine(Name + " "+player.Class.ToString());
                Console.WriteLine("공격력 : " + Stat_Offense);
                Console.WriteLine("방어력 : " + Stat_Defense);
                Console.WriteLine("체 력 : " + HP);
                Console.WriteLine("Gold : " + Gold+"\n");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string Choice = Console.ReadLine();

            }
        }
        static void Main(string[] args)
        {
            Setup();
            Lobby();
        }
        class Item
        {
            
        }

        class inventory
        {

        }
    }
}
