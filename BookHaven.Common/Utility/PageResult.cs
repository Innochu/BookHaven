namespace BookHaven.Common.Utility
{
    public class PageResult<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; } = 0;    
    }
}
