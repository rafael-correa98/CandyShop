namespace CandyShopApi.Models
{
    public class CandyShopDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CandiesCollectionName { get; set; } = null!;

        public string CategoryCollectionName { get; set; } = null!;

        public string CustomersCollectionName { get; set; } = null!;

        public string OrdersCollectionName { get; set; } = null!;
    }
}
