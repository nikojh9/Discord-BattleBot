using System;

namespace Discord_Bot
{
    internal class Player
    {
        public double Vigor { get; set; }
        public double Endurance { get; set; }
        public double Strength { get; set; }
        public Item[] items = new Item[5];
        public int Gold { get; set; }


        public Player(double Vigor, double Endurance, double Strength)
        {
            this.Vigor = Vigor;
            this.Endurance = Endurance;
            this.Strength = Strength;

        }


        public void addItem(Item item)
        {
            // Check the ItemType and place it in the correct slot
            switch (item.ItemType)
            {
                case Item.Type.Helmet:
                    items[0] = item;
                    break;
                case Item.Type.BreastPlate:
                    items[1] = item;
                    break;
                case Item.Type.Leggins:
                    items[2] = item;
                    break;
                case Item.Type.Boots:
                    items[3] = item;
                    break;
                case Item.Type.Weapon:
                    items[4] = item;
                    break;
                case Item.Type.Trinket:
                    items[5] = item;
                    break;
                default:
                    Console.WriteLine("Item type not recognized or no slot available.");
                    break;
            }

            //Kalder metoden hver gang nyt item bliver tilføjet
            itemAddStats();
        }
        //Resetter alle stats og tilføjer de nye
        public void itemAddStats()
        {
            Vigor = 0;
            Endurance = 0;
            Strength = 0;

            foreach (Item item in items)
            {
                this.Vigor += item.ItemVigor;
                this.Endurance += item.ItemEndurance;
                this.Strength += item.ItemStrength;
            }
        }

        public string Stats()
        {
            return
                   $"Endurance: {Endurance}\n" +
                   $"Strength: {Strength}\n" +
                   $"Vigor: {Vigor}";
        }


    }
}
