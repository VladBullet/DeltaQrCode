using System.Collections.Generic;

namespace DeltaQrCode.ViewModels
{
    using DeltaQrCode.ModelsDto;

    public class HotelListViewModel
    {

        public HotelListViewModel()
        {

        }

        public HotelListViewModel(PaginatedList<AnvelopDto> paginatedModel)
        {
            List = paginatedModel;
        }

        public HotelListViewModel(List<AnvelopDto> list, int pageIndex, int pageSize)
        {
            List = PaginatedList<AnvelopDto>.Create(list, pageIndex, pageSize);
        }

        public PaginatedList<AnvelopDto> List { get; set; }

    }
}
