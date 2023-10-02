using PetsWebApp.Core.Interfaces.Services;
using PetsWebApp.Core.Static;

namespace PetsWebApp.Application.Services
{
    public class PaginationService : IPaginationService
    {
        public (int firstPage, int lastPage) GetPagesRange(int pageNumber, int totalPages)
        {
            int displayedPages = Math.Min(totalPages, Constants.MinimumPages);

            int halfDisplayedPages = (int)Math.Ceiling(displayedPages / 2.0);

            int startPage = Math.Max(1, pageNumber - halfDisplayedPages + 1);
            int endPage = Math.Min(startPage + displayedPages - 1, totalPages);

            if (endPage - startPage + 1 < displayedPages)
            {
                startPage = Math.Max(1, endPage - displayedPages + 1);
            }

            return (startPage, endPage);
        }
    }
}