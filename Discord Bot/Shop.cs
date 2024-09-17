using System.Collections.Generic;

namespace Discord_Bot
{
    internal class Shop
    {
        public static List<Item> ShopAllItems { get; set; } = new List<Item>();
        public Item[] ShopItemsForSale = new Item[5];


        public static void initiateShopItems()
        {

            Item it1 = new Item("Evalds Tand", "Kraftfuld trinket fra den episke kamp mellem kevin og evald", 5, 5, 5, Item.Type.Trinket);
            Item it2 = new Item("Juhls Kødsværd", "Ikke så stor men meget masse", 1, 1, 10, Item.Type.Weapon);
            Item it3 = new Item("Ziztos designersko", "Fucking dyrt", 5, 10, 1, Item.Type.Boots);
            Item it4 = new Item("Ribers Underhakker", "Modstander hader lugten", 1, 10, 10, Item.Type.Leggins);
            Item it5 = new Item("Jonas blazer", "Forlanger respekt", 10, 3, 3, Item.Type.BreastPlate);
            Item it6 = new Item("Den hellige hat", "Kan alt", 10, 10, 10, Item.Type.Helmet);

            ShopAllItems.Add(it1);
            ShopAllItems.Add(it2);
            ShopAllItems.Add(it3);
            ShopAllItems.Add(it4);
            ShopAllItems.Add(it5);
            ShopAllItems.Add(it6);





        }






    }
}
