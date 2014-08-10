using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgileWays.ExpressionSearch.Service
{
    public class ExpressionOpFactory
    {
        private IOpConfig _config;
        public ExpressionOpFactory(IOpConfig config)
        {
            _config = config;
        }

        public ExpressionType GetExpressionType(String value)
        {
            return _config[value];
        }
    }
}
