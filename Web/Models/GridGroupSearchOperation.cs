using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public enum GridGroupSearchOperation
    {
        AND = 1,
        OR = 2
    }
}