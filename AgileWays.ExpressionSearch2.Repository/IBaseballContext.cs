using System;
using System.Linq;
namespace AgileWays.ExpressionSearch2.Repository
{
    public interface IBaseballContext
    {
        IQueryable<T> SelectAll<T>();
    }
}
