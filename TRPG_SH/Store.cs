using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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
                else if (choice == "2")
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
                if (selectedItem.Stat_Defense > 0)
                {
                    player.Stat_Defense -= selectedItem.Stat_Defense;
                    selectedItem.IsEquipped = false;
                }
                else if (selectedItem.Stat_Offense > 0)
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
}
