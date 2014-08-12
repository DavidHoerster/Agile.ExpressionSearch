using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AgileWays.ExpressionSearch.Service
{
    public class ExpressionBehavior
    {
        public ExpressionType ExpressionType { get; set; }
        public Boolean IsBinary { get; set; }
        public Boolean UseMethod { get; set; }
        public String Method { get; set; }
        public Boolean MethodResultCompareValue { get; set; }
    }
    public interface IOpConfig
    {
        IOpConfig Add(String value, ExpressionBehavior expressionType);
        ExpressionBehavior this[String exprName] { get; }
    }
    public class OpConfig : IOpConfig
    {
        private Dictionary<String, ExpressionBehavior> _expressionTypeMap;

        public OpConfig()
        {
            _expressionTypeMap = new Dictionary<string, ExpressionBehavior>();
        }
        public IOpConfig Add(string value, ExpressionBehavior expressionType)
        {
            _expressionTypeMap.Add(value, expressionType);
            return this;
        }

        public ExpressionBehavior this[String exprName]
        {
            get
            {
                if (_expressionTypeMap.ContainsKey(exprName))
                {
                    return _expressionTypeMap[exprName];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
