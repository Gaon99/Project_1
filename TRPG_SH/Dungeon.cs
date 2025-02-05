using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
    class Dungeon
    {
        Random random = new Random();

        float temp_HP;
        int temp_Gold;
        int Need_Stage_Defense = 0;
        float Damage = 0;
        int Gain_Gold = 0;
        int Stage_Gold = 0;

        public void Display_Dungeon(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("\n1.쉬운 던전\t | 방어력 5 이상 권장");
                Console.WriteLine("2.일반 던전\t | 방어력 11 이상 권장");
                Console.WriteLine("3 어려운 던전 \t | 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Lobby._Lobby(player);
                }
                if (int.TryParse(choice, out int difficulty) && difficulty <= 3 && difficulty > 0)
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
                Fail_Dungeon(player);
            }
        }
        private void Clear_Dungeon(Player player, int difficulty)
        {
            Dungeon_Diff enum_value = (Dungeon_Diff)difficulty;

            temp_Gold = player.Gold;
            Gain_Gold = Stage_Gold += Stage_Gold * random.Next(player.Stat_Offense, player.Stat_Offense * 2 + 1)/100;
            player.Gold += Gain_Gold;
            _Damage(player);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{enum_value} 던전을 클리어 하였습니다.\n");
                Console.WriteLine("탐험 결과");
                Console.WriteLine($"체력 {temp_HP} -> {player.HP}");
                Console.WriteLine($"Gold {temp_Gold} -> {player.Gold}\n");
                Console.WriteLine("아무 키를 눌려 계속");
                Console.ReadKey();
                Display_Dungeon(player);
            }

        }
        private void Fail_Dungeon(Player player)
        {
            _Damage(player);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전 실패\n");
                Console.WriteLine($"체력 :{temp_HP} -> {player.HP}\n");
                Console.WriteLine("아무 키를 눌려 계속");
                Console.ReadKey();
                Display_Dungeon(player);
            }
        }
        private void Dungeon_Setting(Player player, int difficulty, out bool Isclearable)
        {
            Dungeon_Diff dungeon_Diff = (Dungeon_Diff)difficulty;
            switch (dungeon_Diff)
            {
                case Dungeon_Diff.쉬운: Need_Stage_Defense = 5; Stage_Gold = 1000; break;
                case Dungeon_Diff.일반: Need_Stage_Defense = 11; Stage_Gold = 1700; break;
                case Dungeon_Diff.어려운: Need_Stage_Defense = 17; Stage_Gold = 2500; break;
            }
            if (player.Stat_Defense >= Need_Stage_Defense)
            {
                Isclearable = true;
            }
            else
            {
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

        private void _Damage(Player player)
        {
            temp_HP = player.HP;
            Damage = random.Next(25, 36);
            Damage += Need_Stage_Defense - player.Stat_Defense;
            if (player.HP - Damage < 0)
            {
                player.GameOver();
            }
            else
                player.HP -= Damage;
        }

    }
}

