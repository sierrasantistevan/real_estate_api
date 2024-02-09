namespace RealEstate.API.Models
{
    public class QueryParameters
    {
        const int _maxSize = 50;
        private int _size = 15;
        public int Page { get; set; } = 1;
        public int Size 
        {
            get { return _size; }
            set { _size = Math.Min(_maxSize, value); }
        }

        public string SortBy { get; set; } = "Id";
        private string _sortOrder = "asc";
        public string SortOrder 
        {
            get 
            {
                return _sortOrder;
            }
            set
            {
                if(value == "asc" || value == "desc")
                {
                    _sortOrder = value;
                }
            }
        }
    }
}