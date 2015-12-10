namespace Business.Database
{
    public static class ShopData
    {
        public static ShopDataContext DataContext
        {
            get { return new ShopDataContext(); }
        }
    }
}