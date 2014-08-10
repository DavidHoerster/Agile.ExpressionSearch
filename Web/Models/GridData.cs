using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class GridRow
    {
        [DataMember(Name="id")]
        public String ID { get; set; }
        [DataMember(Name="cell")]
        public String[] RowData { get; set; }
    }

    [DataContract]
    public class GridData
    {
        [DataMember(Name="total")]
        public Int32 TotalPages { get; set; }
        [DataMember(Name="page")]
        public Int32 CurrentPage { get; set; }
        [DataMember(Name="records")]
        public Int32 TotalRecords { get; set; }
        [DataMember(Name="rows")]
        public List<GridRow> GridRows { get; set; }
    }
}