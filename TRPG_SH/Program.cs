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
    Store,
    Dungeon,
    Rest
}
enum Classes
{
    전사 = 1,
    마법사,
    궁수,
    도적
}
enum Dungeon_Diff
{
    쉬운 = 1,
    일반,
    어려운
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
            Dungeon dungeon = new Dungeon();

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
            while (!IsSelected)
            {
                Console.WriteLine("원하시는 직업을 선택해주세요\n");
                Console.WriteLine("1. 전사\n2. 마법사\n3. 궁수\n4. 도적");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int Value) && Enum.IsDefined(typeof(Classes), Value))
                {
                    Classes classes = (Classes)Value;
                    player.Class = classes.ToString();
                    IsSelected = true;

                    switch (classes)
                    {
                        case Classes.전사:
                            player.HP = 100;
                            player.Stat_Offense = 10;
                            player.Stat_Defense = 5;
                            break;
                        case Classes.마법사:
                            player.HP = 80;
                            player.Stat_Offense = 15;
                            player.Stat_Defense = 3;
                            break;
                        case Classes.궁수:
                            player.HP = 90;
                            player.Stat_Offense = 13;
                            player.Stat_Defense = 4;
                            break;
                        case Classes.도적:
                            player.HP = 95;
                            player.Stat_Offense = 12;
                            player.Stat_Defense = 4;
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
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기");
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

                        case Lobby_info.Dungeon:
                            window.ShowDungeon(player);
                            break;

                        case Lobby_info.Rest:
                            window.ShowRest(player);
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
        Dungeon dungeon = new Dungeon();
        Rest rest = new Rest();


        public Window(Player player)
        {
            this.player = player;
            this.store = new Store();
        }
        public void ShowStatus()
        {
            player.Display_Status();
        }
        public void ShowInventory()
        {
            player.Display_Inventory();
        }
        public void ShowStore(Player player)
        {
            store.Show_Store(player);
        }
        public void ShowDungeon(Player player)
        {
            dungeon.Display_Dungeon(player);
        }
        public void ShowRest(Player player)
        {
            rest.Display_Rest(player);
        }
    }

    class Player
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; } = 1;
        public int Stat_Offense { get; set; } = 10;
        public int Stat_Defense { get; set; } = 5;
        public float HP { get; set; } = 100;
        public int Gold { get; set; } = 15000;
        public Inventory Inventory { get; set; } = new Inventory();
        private Item EquippedWeapon { get; set; }
        private Item EquippedArmor { get; set; }
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]\n");
                Inventory.Display();
                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();
                if (choice == "0")
                {
                    Lobby._Lobby(this);
                }
                else if (choice == "1")
                {
                    Inventory.Manage_Equip(this);
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력했습니다.");
                    Console.ReadKey();
                }
            }
        }

        public void EquipItem(Item item)
        {
            if (item == null) return;

            if (item.IsEquipped)
            {
                if (item == EquippedWeapon)
                {
                    EquippedWeapon = null;
                    Stat_Offense -= item.Stat_Offense;
                }
                else if (item == EquippedArmor)
                {
                    EquippedArmor = null;
                    Stat_Defense -= item.Stat_Defense;
                }
                item.IsEquipped = false;
            }

            else
            {
                if (item.Stat_Offense > 0)
                {
                    if (EquippedWeapon != null)
                    {
                        EquippedWeapon.IsEquipped = false;
                        Stat_Offense -= EquippedWeapon.Stat_Offense;
                    }
                    EquippedWeapon = item;
                    Stat_Offense += EquippedWeapon.Stat_Offense;
                }
                else if (item.Stat_Defense > 0)
                {
                    if (EquippedArmor != null)
                    {
                        EquippedArmor.IsEquipped = false;
                        Stat_Defense -= EquippedArmor.Stat_Defense;
                    }
                    EquippedArmor = item;
                    Stat_Defense += EquippedArmor.Stat_Defense;
                }
                item.IsEquipped = true;
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
        public bool IsEquipped { get; set; }
        public bool IsArmor { get; set; }
        public override string ToString()
        {
            return $"{(IsEquipped ? "[E] " : "")}{Item_Name} | {Item_Info}";
        }
    }

    class Inventory
    {
        public List<Item> items = new List<Item>();

        public void Add_Item(Item item)
        {
            items.Add(item);
        }
        public void Remove_item(Item item)
        {
            items.Remove(item);
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
                    Console.WriteLine($" - {i + 1}. {items[i]}");
                }
            }
        }

        public void Manage_Equip(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Display();
                Console.WriteLine("\n 0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();
                if (choice == "0")
                {
                    Lobby._Lobby(player);
                }
                if (int.TryParse(choice, out int index) && index > 0 && index <= items.Count)
                {
                    player.EquipItem(items[index - 1]);
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력했습니다.");
                    Console.ReadKey();
                }
            }
        }
    }

    class Store
    {
        private List<Item> Store_Items = new List<Item>()
    {
        new Item { Item_Name = "수련자 갑옷", Stat_Defense = 5, Item_Info = "수련에 도움을 주는 갑옷입니다.", Price = 1000, IsPurchased = false,IsArmor=true},
        new Item { Item_Name = "무쇠갑옷",Stat_Defense = 9, Item_Info = "무쇠로 만들어져 튼튼한 갑옷입니다.", Price = 2000, IsPurchased = false,IsArmor=true},
        new Item { Item_Name = "스파르타의 갑옷", Stat_Defense = 15, Item_Info = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", Price = 3500, IsPurchased = false,IsArmor=true},
        new Item { Item_Name = "낡은 검", Stat_Offense = 2, Item_Info = "쉽게 볼 수 있는 낡은 검입니다.", Price = 600, IsPurchased = false,IsArmor=false},
        new Item { Item_Name = "청동 도끼", Stat_Offense = 5, Item_Info = "어디선가 사용됐던 도끼입니다.", Price = 1500, IsPurchased = false ,IsArmor=false},
        new Item { Item_Name = "스파르타의 창", Stat_Offense = 7, Item_Info = "스파르타의 전사들이 사용했다는 전설의 창입니다.", Price = 2500, IsPurchased = false,IsArmor=false},
        };
        public void Show_Store(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine($"[보유 골드] {player.Gold} G\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < Store_Items.Count; i++)
                {
                    var item = Store_Items[i];
                    string _Purchase = item.IsPurchased ? "구매 완료" : $"{item.Price}";
                    if (item.IsArmor)
                        Console.WriteLine($"{i + 1}. {item.Item_Name} | 방어력 {item.Stat_Defense} | {item.Item_Info} | {_Purchase}");
                    else
                        Console.WriteLine($"{i + 1}. {item.Item_Name} | 공격력 {item.Stat_Offense} | {item.Item_Info} | {_Purchase}");
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Buy_Item(player);
                }
                else if(choice == "2")
                {
                    Display_Sell(player);
                }
                else if (choice == "0")
                {
                    Lobby._Lobby(player);
                }

                else
                {
                    Console.WriteLine("잘못된 선택입니다.");
                    Console.ReadKey();
                }
            }
        }

        public void Display_Sell(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매\n");
                Console.WriteLine("필요한 아이템을 얻을수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]\n");
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine($"{player.Gold} G\n");
                for (int i = 0; i < player.Inventory.items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {player.Inventory.items[i].Item_Name} - {player.Inventory.items[i].Price} G");
                }
                Console.WriteLine("\n0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int index) && index > 0 && index <= player.Inventory.items.Count)
                {
                    sell_item(player, choice);
                }
                else if (choice == "0")
                {
                    Show_Store(player);
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력했습니다.");
                    Console.ReadKey();
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
        public void sell_item(Player player, string Selection)
        {
            if (int.TryParse(Selection, out int index) && index > 0 && index <= player.Inventory.items.Count)
            {
                Item selectedItem = player.Inventory.items[index - 1];
                if(selectedItem.Stat_Defense > 0)
                {
                    player.Stat_Defense -= selectedItem.Stat_Defense;
                    selectedItem.IsEquipped = false;
                }
                else if(selectedItem.Stat_Offense>0)
                {
                    player.Stat_Offense -= selectedItem.Stat_Offense;
                    selectedItem.IsEquipped = false;
                }
                int Sell_Price = (int)(selectedItem.Price * 0.8);
                player.Gold += Sell_Price;
                player.Inventory.Remove_item(selectedItem);
                selectedItem.IsPurchased = false;
                Console.WriteLine("해당 아이템을 {0}G 에 판매하였습니다", Sell_Price);
                Console.WriteLine("아무 키를 눌려 계속");
                Console.ReadKey();

            }
        }
    }
    class Dungeon
    {
        float temp_HP;
        int temp_Gold;

        public void Display_Dungeon(Player player)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("\n1.쉬운 던전\t | 방어력 5 이상 권장");
                Console.WriteLine("2.일반 던전\t | 방어력 11 이상 권장");
                Console.WriteLine("3 어려운 던전 \t | 방어력 17 이상 권장\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Lobby._Lobby(player);
                }
                if(int.TryParse(choice, out int difficulty)&& difficulty<=3 && difficulty>0)
                {
                    _Dungeon(player, difficulty);
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력했습니다");
                    Console.ReadKey();
                }
            }
        }
        private void _Dungeon(Player player, int difficulty)
        {
            bool Is_Clear;
            Dungeon_Setting(player, difficulty, out Is_Clear);
            
            if (Is_Clear)
            {
                Clear_Dungeon(player, difficulty);
            }
            else
            {
                Fail_Dungeon(player, difficulty);
            }
        }
        private void Clear_Dungeon(Player player, int difficulty)
        {

            Dungeon_Diff enum_value = (Dungeon_Diff)difficulty;

            while (true)
            {
                temp_HP = player.HP;
                temp_Gold = player.Gold;
                Console.Clear();
                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{enum_value} 던전을 클리어 하였습니다.\n");
                Console.WriteLine("탐험 결과");
                Console.WriteLine($"체력 {temp_HP} -> {player.HP}");
                Console.WriteLine($"Gold {temp_Gold} -> {player.Gold}\n");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Display_Dungeon(player);
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력했습니다.");
                    Console.ReadKey();

                }
            }
        
        }
        private void Fail_Dungeon(Player player, int difficulty)
        {
            temp_HP = player.HP;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전 실패\n");
                Console.WriteLine($"체력 :{temp_HP} -> {player.HP}\n");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Display_Dungeon(player);
                }
                else
                {
                    Console.WriteLine("잘못된 값을 입력했습니다.");
                    Console.ReadKey();

                }
            }
        }
        private void Dungeon_Setting(Player player, int difficulty, out bool Isclearable)
        {
            Dungeon_Diff dungeon_Diff = (Dungeon_Diff)difficulty;
            int Need_Stage_Defense = 0;
            switch(dungeon_Diff)
            {
                case Dungeon_Diff.쉬운: Need_Stage_Defense = 5; break;
                case Dungeon_Diff.일반: Need_Stage_Defense = 11; break;
                case Dungeon_Diff.어려운: Need_Stage_Defense = 17; break;
            }
            if (player.Stat_Defense > Need_Stage_Defense)
            {
                Isclearable = true;
            }
            else
            {
                Random random = new Random();
                int Pencent = random.Next(1, 11);
                if (Pencent < 4)
                {
                    Isclearable = true;
                }
                else
                {
                    Isclearable = false;
                }
            }

        }
    }
    class Rest
    {
        public void Display_Rest(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine("{0} G\n", player.Gold);
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string Choice = Console.ReadLine();
                if (Choice == "0")
                {
                    Lobby._Lobby(player);
                }
                else if (Choice == "1")
                {
                    _Rest(player);
                    Console.WriteLine("체력이 모두 회복되었습니다.");
                    Console.WriteLine("아무 키를 눌려 계속");
                    Console.ReadKey();
                }
            }
        }
        public void _Rest(Player player)
        {
            if (player.Gold >= 500)
            {

                switch (player.Class)
                {
                    case "전사": player.HP = ((player.Level - 1) * 10) + 100; break;
                    case "마법사": player.HP = ((player.Level - 1) * 10) + 80; break;
                    case "궁수": player.HP = ((player.Level - 1) * 10) + 90; break;
                    case "도적": player.HP = ((player.Level - 1) * 10) + 95; break;
                }
                player.Gold -= 500;
            }

            else
            {
                Console.WriteLine("보유 골드가 부족합니다.");
            }
        }
    }
}



