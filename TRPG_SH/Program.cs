using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Numerics;
using TRPG_SH;
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
        static void Main(string[] args)
        {
            Setup setup = new Setup();
            Player player = new Player();
            Store store = new Store();
            setup._Setup(player);
            Lobby._Lobby(player);
        }
    }

    class Setup
    {
        bool IsSelected = false;
        public void _Setup(Player player)
        {

                Console.WriteLine("원하시는 캐릭터의 이름을 입력해주세요");
                string name = Console.ReadLine();
                player.Name = name;
                Console.Clear();

            while (!IsSelected) { 
                Console.WriteLine("원하시는 직업을 선택해주세요\n");
                Console.WriteLine("1. 전사\n2. 마법사\n3. 궁수\n4. 도적");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int Value) && Enum.IsDefined(typeof(Classes), Value))
                {
                    Classes classes = (Classes)Value;
                    player.Class = classes.ToString();
                    IsSelected = true;

                    switch (classes) // 추후에 마개조(?)인지 아닌지 컨펌,, 직업마다, 세팅 설정
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
                    Console.WriteLine("잘못된 값을 입력 하셨습니다.\n");
                }
            }
        }
    }

    static class Lobby
    {
        public static void _Lobby(Player player)
        {
            Window window = new Window(player);
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
                            window.ShowStatus();
                            break;
                        case Lobby_info.Inventory:
                            window.ShowInventory();
                            break;
                        case Lobby_info.Store:
                            window.ShowStore(player);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력을 하셨습니다.");
                }
            }
        }
    }

    class Window
    {
        public Player player;
        public Store store;

        public Window(Player player)
        {
            this.player = player;
            this.store = new Store();
        }
        public void ShowStatus()
        {
            if (player == null) return;
            Console.Clear();
            player.Display_Status();
        }
        public void ShowInventory()
        {
            if (player == null) return;
            Console.Clear();
            player.Display_Inventory();

        }
        public void ShowStore(Player player)
        {
            if (player == null) return;
            Console.Clear();
            store.Show_Store(player);

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
        public int Gold { get; set; } = 1500;
        public Inventory Inventory { get; set; } = new Inventory();
        public void Display_Status()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine("Lv. " + Level);
                Console.WriteLine(Name + " " + Class);
                Console.WriteLine("공격력 : " + Stat_Offense);
                Console.WriteLine("방어력 : " + Stat_Defense);
                Console.WriteLine("체 력 : " + HP);
                Console.WriteLine("Gold : " + Gold + "\n");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string Choice = Console.ReadLine();
                if (Choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력했습니다");
                }
            }
            Lobby._Lobby(this);

        }
        public void Display_Inventory()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            Inventory.Display();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string choice = Console.ReadLine();
            if (choice == "0")
            {
                Lobby._Lobby(this);
            }
            else if (choice == "1")
            {
                Inventory.Manage_Equip();
            }
        }
    }

    class Item
    {
        public string Item_Name { get; set; }
        public int Price { get; set; }
        public string Item_Info { get; set; }
        public bool IsPurchased { get; set; }
        public int Stat_Offense { get; set; }
        public int Stat_Defense { get; set; }
        public bool IsArmor { get; set; }
        public bool IsUsable { get; set; }
        public void use()
        {
            if (false)//usable item
            {
            }

            else if (false) //equip item
            {

            }
            else // 예외 처리
            {

            }
        }
    }

    class Inventory
    {
        private List<Item> items = new List<Item>();

        public void Add_Item(Item item)
        {
            items.Add(item);
        }

        public void Display()
        {
            if (items.Count == 0)
            {
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"- {i + 1}. {items[i].Item_Name} : {items[i].Item_Info}");
                }
            }
        }
        public Item GetItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                return items[index];
            }
            return null;
        }

        public void Manage_Equip()
        {

        }
    }

    class Store
    {
        private List<Item> Store_Items = new List<Item>()
    {
        new Item { Item_Name = "수련자 갑옷", Stat_Defense = 5, Item_Info = "수련에 도움을 주는 갑옷입니다.", Price = 1000, IsPurchased = false,IsArmor = true, IsUsable=false },
        new Item { Item_Name = "무쇠갑옷",Stat_Defense = 9, Item_Info = "무쇠로 만들어져 튼튼한 갑옷입니다.", Price = 2000, IsPurchased = false ,IsArmor = true, IsUsable=false },
        new Item { Item_Name = "스파르타의 갑옷", Stat_Defense = 15, Item_Info = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", Price = 3500, IsPurchased = false ,IsArmor = true, IsUsable = false},
        new Item { Item_Name = "낡은 검", Stat_Offense = 2, Item_Info = "쉽게 볼 수 있는 낡은 검입니다.", Price = 600, IsPurchased = false ,IsArmor = false, IsUsable=false },
        new Item { Item_Name = "청동 도끼", Stat_Offense = 5, Item_Info = "어디선가 사용됐던 도끼입니다.", Price = 1500, IsPurchased = false ,IsArmor = false, IsUsable=false },
        new Item { Item_Name = "스파르타의 창", Stat_Offense = 7, Item_Info = "스파르타의 전사들이 사용했다는 전설의 창입니다.\t", Price = 2500, IsPurchased = false,IsArmor = false, IsUsable=false }
    };
        public void Show_Store(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점에 오신 것을 환영합니다!");
                Console.WriteLine($"[보유 골드] {player.Gold} G\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < Store_Items.Count; i++)
                {
                    var item = Store_Items[i];
                    string _Purchase = item.IsPurchased ? "구매 완료" : $"{item.Price}";
                    if(item.IsArmor)
                    Console.WriteLine($"{i + 1}. {item.Item_Name} | 방어력 {item.Stat_Defense} | {item.Item_Info} | {_Purchase}");
                    else
                    Console.WriteLine($"{i + 1}. {item.Item_Name} | 공격력 {item.Stat_Offense} | {item.Item_Info} | {_Purchase}");
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Buy_Item(player);
                }
                else if (choice == "0")
                {
                    Lobby._Lobby(player);
                }

                else
                {
                    Console.WriteLine("잘못된 선택입니다.");
                }
            }
        }
        private void Buy_Item(Player player)
        {
            Console.Write("구매할 아이템 번호를 선택해주세요.\n>> ");
            string Selection = Console.ReadLine();
            if (int.TryParse(Selection, out int index) && index > 0 && index <= Store_Items.Count)
            {
                Item selectedItem = Store_Items[index - 1];
                if (selectedItem.IsPurchased)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.WriteLine("아무 키를 눌려 계속");
                    Console.ReadKey();
                }
                else if (player.Gold >= selectedItem.Price)
                {
                    player.Gold -= selectedItem.Price;
                    player.Inventory.Add_Item(selectedItem);
                    selectedItem.IsPurchased = true;
                    Console.WriteLine($"{selectedItem.Item_Name}을(를) 구매했습니다.");
                    Console.WriteLine("아무 키를 눌려 계속");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("보유 중인 골드가 부족합니다.");
                    Console.WriteLine("아무 키를 눌려 계속");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("잘못된 선택입니다.");
                Console.WriteLine("아무 키를 눌려 계속");
                Console.ReadKey();
            }
        }
    }
}

