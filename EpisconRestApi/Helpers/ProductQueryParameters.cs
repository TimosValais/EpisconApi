namespace EpisconApi.Helpers
{
    public class ProductQueryParameters : QueryParameters
    {

        private decimal? _minPrice;

        public decimal? MinPrice
        {
            get { return _minPrice; }
            set { _minPrice = value; }
        }

        private decimal? _maxPrice;

        public decimal? MaxPrice
        {
            get { return _maxPrice; }
            set { _maxPrice = value; }
        }

    }
}
