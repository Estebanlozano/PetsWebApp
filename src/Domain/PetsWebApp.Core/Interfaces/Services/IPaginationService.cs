namespace PetsWebApp.Core.Interfaces.Services
{
    public interface IPaginationService
    {
        public (int firstPage, int lastPage) GetPagesRange(int pageNumber, int itemsCount);
    }
}
