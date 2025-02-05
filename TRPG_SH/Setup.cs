using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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
}
