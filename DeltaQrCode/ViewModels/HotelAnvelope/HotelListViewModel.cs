using System.Collections.Generic;

namespace DeltaQrCode.ViewModels
{
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

        public HotelListViewModel(PaginatedList<AnvelopaVM> paginatedResult)
        {
            anvList = paginatedResult;
        }
        public HotelListViewModel(List<HotelAnvelopaVm> list, int pageIndex, int pageSize)
        {
            List = PaginatedList<HotelAnvelopaVm>.Create(list, pageIndex, pageSize);
        }

        public PaginatedList<HotelAnvelopaVm> List { get; set; }

        public HotelListViewModel(List<AnvelopaVM> list, int pageIndex, int pageSize)
        {
            anvList = PaginatedList<AnvelopaVM>.Create(list, pageIndex, pageSize);
        }

        public PaginatedList<AnvelopaVM> anvList { get; set; }
    }
}
