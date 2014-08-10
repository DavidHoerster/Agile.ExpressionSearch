using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class GridOptions
    {
        public GridFilters Filters { get; set; }
        public bool IsSearch { get; set; }
        public string ND { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
        public string SortIndex { get; set; }
        public string SortOrder { get; set; }
    }
}