using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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

