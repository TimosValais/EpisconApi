namespace EpisconApi.Helpers
{
    public class SearchQueryParameters
    {
        private string _searchField;

        public string SearchField
        {
            get { return _searchField; }
            set { _searchField = value; }
        }

        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { _searchTerm = value; }
        }

    }
}
