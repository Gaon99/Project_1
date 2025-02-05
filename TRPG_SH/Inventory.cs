using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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
}
