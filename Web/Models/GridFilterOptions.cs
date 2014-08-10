using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class GridFilters
    {
        [DataMember(Name = "groupOp")]
        public GridGroupSearchOperation GroupOperation { get; set; }
        [DataMember(Name = "rules")]
        public List<GridFilterOptions> FilterRules { get; set; }
    }

    [DataContract]
    public class GridFilterOptions
    {
        [DataMember(Name="field")]
        public string Field { get; set; }
        [DataMember(Name="op")]
        public String Operation { get; set; }
        [DataMember(Name="data")]
        public string FieldData { get; set; }
    }
}