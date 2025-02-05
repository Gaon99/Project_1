using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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
}
