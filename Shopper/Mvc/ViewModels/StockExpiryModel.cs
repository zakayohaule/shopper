namespace Shopper.Mvc.ViewModels
{
    public class StockExpiryModel
    {
        public int LowStockCount { get; set; }
        public int OutOfStockCount { get; set; }
        public int NearExpiryCount { get; set; }
        public int ExpiredCount { get; set; }
    }
}
