
namespace PetsWebApp.Core.Entities
{
    public class Pagination<T>
    {
        public int CurrentPage { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; }

        public Pagination(IEnumerable<T> items)
        {
            Items = items;
        }
    }
}
