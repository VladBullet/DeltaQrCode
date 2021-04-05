using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels
{
    using DeltaQrCode.ModelsDto;

    public class HotelListViewModel
    {

        public HotelListViewModel()
        {

        }

        public HotelListViewModel(PaginatedList<SetAnvelopeDto> paginatedModel)
        {
            List = paginatedModel;
        }

        public HotelListViewModel(List<SetAnvelopeDto> list, int pageIndex, int pageSize)
        {
            List = PaginatedList<SetAnvelopeDto>.Create(list, pageIndex, pageSize);
        }

        public PaginatedList<SetAnvelopeDto> List { get; set; }

    }
}
