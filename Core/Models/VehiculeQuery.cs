using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Extensions;

namespace autobook.Core.Models
{
    public class VehiculeQuery : IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}