namespace EpisconApi.Helpers
{
    public class QueryParameters
    {
        const int _maxSize = 1000;
        private int _size = 50;

        public int Page { get; set; }
 
        public int Size
        {
            get { return _size; }
            set { _size = Math.Min(_maxSize,value); }
        }

        private string? _sortBy;

        public string? SortBy
        {
            get { return _sortBy; }
            set { _sortBy = value; }
        }

        private string _orderBy = "asc";

        public string OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value != null ? value : "asc"; }
        }



    }
}
