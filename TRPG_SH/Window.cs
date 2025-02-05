using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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
}
