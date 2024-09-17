using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    internal class Item
    {

        public String ItemName { get; set; }
        public String ItemDescription { get; set; }
        public double ItemVigor { get; set; }
        public double ItemEndurance { get; set; }
        public double ItemStrength { get; set; }
        public Type ItemType { get; set; }
      
        public enum Type
        {
            Helmet,
           BreastPlate,
           Leggins,
           Boots,
           Weapon,
           Trinket
            
        }

        public Item(string itemName, string itemDescription, double itemVigor, double itemEndurance, double itemStrength,Type type)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            ItemVigor = itemVigor;
            ItemEndurance = itemEndurance;
            ItemStrength = itemStrength;
            ItemType = type;
        }
    }
}
