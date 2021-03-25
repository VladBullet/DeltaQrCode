﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels
{
    public class HotelListViewModel
    {
        public HotelListViewModel()
        {

        }
        public HotelListViewModel(List<HotelViewModel> list, int totalPagesNR, int currentPage)
        {
            List = list;
            TotalPages = totalPagesNR;
            CurrentPage = currentPage;
        }

        public List<HotelViewModel> List { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

    }
}
