using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Web.Common;
using System.Linq.Expressions;

namespace Web.Models
{
    [DataContract]
    [Flags]
    public enum GridSearchOperation
    {
        /// <summary>
        /// Equals
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.Equal, true, "", "ToUpper")]
        EQ = 1,
        /// <summary>
        /// Does not equal
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.NotEqual, true, "", "ToUpper")]
        NE = 2,
        /// <summary>
        /// Greater than
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.GreaterThan, true)]
        GT = 4,
        /// <summary>
        /// Greater than or equal to
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.GreaterThanOrEqual, true)]
        GE = 8,
        /// <summary>
        /// Less than
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.LessThan, true)]
        LT = 16,
        /// <summary>
        /// Less than or equal to
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.LessThanOrEqual, true)]
        LE = 32,
        /// <summary>
        /// Begins with
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.IsTrue, false, "StartsWith", "ToUpper")]
        BW = 64,
        /// <summary>
        /// Does not begin with
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.IsFalse, false, "StartsWith", "ToUpper")]
        BN = 128,
        /// <summary>
        /// Ends with
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.IsTrue, false, "EndsWith", "ToUpper")]
        EW = 256,
        /// <summary>
        /// Does not ends with
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.IsFalse, false, "EndsWith", "ToUpper")]
        EN = 512,
        /// <summary>
        /// Contains
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.IsTrue, false, "Contains", "ToUpper")]
        CN = 1024,
        /// <summary>
        /// Does not contain
        /// </summary>
        [GridSearchOperationNodeType(ExpressionType.IsFalse, false, "Contains", "ToUpper")]
        NC = 2048
    }
}