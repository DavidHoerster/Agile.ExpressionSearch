using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Web.Common
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class GridSearchOperationNodeTypeAttribute : Attribute
    {
        public GridSearchOperationNodeTypeAttribute(System.Linq.Expressions.ExpressionType nodeType, bool isBinary)
        {
            NodeType = nodeType;
            IsBinary = isBinary;
            StringComparisonMethod = StringManipulationMethod = String.Empty;
        }

        public GridSearchOperationNodeTypeAttribute(System.Linq.Expressions.ExpressionType nodeType,
                                                    bool isBinary,
                                                    String stringComparisonMethod = null,
                                                    String stringManipulationMethod = null)
        {
            NodeType = nodeType;
            IsBinary = isBinary;
            StringComparisonMethod = stringComparisonMethod;
            StringManipulationMethod = stringManipulationMethod;
        }

        public ExpressionType NodeType { get; set; }
        public Boolean IsBinary { get; set; }
        public String StringComparisonMethod { get; set; }
        public String StringManipulationMethod { get; set; }
    }
}