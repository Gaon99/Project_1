using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_SH
{
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
}
