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
}
    
    




