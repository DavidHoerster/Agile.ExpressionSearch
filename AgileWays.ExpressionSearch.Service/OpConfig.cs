using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgileWays.ExpressionSearch.Service
{
    public interface IOpConfig
    {
        IOpConfig Add(String value, ExpressionType expressionType);
        ExpressionType this[String exprName] { get; }
    }
    public class OpConfig : IOpConfig
    {
        private Dictionary<String, ExpressionType> _expressionTypeMap;

        public OpConfig()
        {
            _expressionTypeMap = new Dictionary<string, ExpressionType>();
        }
        public IOpConfig Add(string value, ExpressionType expressionType)
        {
            _expressionTypeMap.Add(value, expressionType);
            return this;
        }

        public ExpressionType this[String exprName]
        {
            get
            {
                if (_expressionTypeMap.ContainsKey(exprName))
                {
                    return _expressionTypeMap[exprName];
                }
                else
                {
                    return ExpressionType.Equal;
                }
            }
        }
    }
}
