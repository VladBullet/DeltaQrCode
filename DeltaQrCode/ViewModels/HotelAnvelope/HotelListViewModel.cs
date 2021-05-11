using System.Collections.Generic;

namespace DeltaQrCode.ViewModels
{
    using DeltaQrCode.ModelsDto;
    using DeltaQrCode.ViewModels.HotelAnvelope;

    public class HotelListViewModel
    {

        public HotelListViewModel()
        {

        }

        public HotelListViewModel(PaginatedList<HotelAnvelopaVm> paginatedModel)
        {
            List = paginatedModel;
        }

        public HotelListViewModel(List<HotelAnvelopaVm> list, int pageIndex, int pageSize)
        {
            List = PaginatedList<HotelAnvelopaVm>.Create(list, pageIndex, pageSize);
        }

        public PaginatedList<HotelAnvelopaVm> List { get; set; }
        

    }
}
