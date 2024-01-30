namespace DemoWebAPI.Models.Repositories
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt {ShirtId = 1, Brand = "Brand1", Color = "Color1", Price = 10, Gender = "men", Size = 20},
            new Shirt {ShirtId = 2, Brand = "Brand2", Color = "Color2", Price = 20, Gender = "men", Size = 10},
            new Shirt {ShirtId = 3, Brand = "Brand3", Color = "Color3", Price = 10, Gender = "women", Size = 15},
            new Shirt {ShirtId = 4, Brand = "Brand4", Color = "Color4", Price = 20, Gender = "women", Size = 10},
        };

        public static List<Shirt> GetAllShirts()
        {
            return shirts;
        }

        public static bool ShirtExists(int id)
        {
            return shirts.Any(x => x.ShirtId == id);
        }

        public static Shirt? GetShirtByID(int id)
        {
            return shirts.FirstOrDefault(x => x.ShirtId == id);
        }

        public static void AddShirt(Shirt shirt)
        {
            int maxId = shirts.Max(x => x.ShirtId);
            shirt.ShirtId = maxId + 1;
            shirts.Add(shirt);
        }

        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = shirts.First(x => x.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Gender = shirt.Gender;
        }

        public static void DeleteShirt(int id)
        {
            var shirt = GetShirtByID(id);
            if(shirt != null)
            {
                shirts.Remove(shirt);
            }
        }
    }
}
