using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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
}
