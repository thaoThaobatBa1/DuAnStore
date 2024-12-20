﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Catalog
{
    public class ProductImageViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? ImagePath { get; set; }
        public bool IsDefault { get; set; }
        public string Caption { get; set; }
        public long FileSize { get; set; }
        public string? CreateBy { get; set; }
        public int SortOrder { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
