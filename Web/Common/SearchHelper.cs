using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;
using System.Linq.Expressions;
using LinqKit;
using System.Reflection;
using AgileWays.ExpressionSearch2.Repository.Entity;
using AgileWays.ExpressionSearch.Service;
using System.Runtime.CompilerServices;

namespace Web.Common
{
    public static class SearchHelper
    {
        public static Expression<Func<T, bool>> 
            CreateSearchPredicate<T>(ExpressionOpFactory factory, GridOptions options)
        {
            bool isGroupOperationAND = options.Filters.GroupOperation == GridGroupSearchOperation.AND;

            var predicate = isGroupOperationAND ? 
                    PredicateBuilder.True<T>() : 
                    PredicateBuilder.False<T>();

            foreach (var rule in options.Filters.FilterRules)
            {
                var operationAttribute = factory.GetExpressionType(rule.Operation);

                PropertyInfo pi = typeof(T).GetProperty(rule.Field);
                ParameterExpression lhsParam = Expression.Parameter(typeof(T));
                Expression lhs = Expression.Property(lhsParam, pi);
                Expression rhs = Expression.Constant(Convert.ChangeType(rule.FieldData, pi.PropertyType));


                Expression theOperation;
                if (operationAttribute.UseMethod)
                {
                    lhs = Expression.Call(lhs, operationAttribute.Method, null, rhs);
                }
                
                if (operationAttribute.IsBinary)
                {
                    theOperation = Expression.MakeBinary(operationAttribute.ExpressionType, lhs, rhs);
                }
                else  //TODO: need to fix this
                {
                    theOperation = Expression.MakeBinary(operationAttribute.ExpressionType, lhs, Expression.Constant(operationAttribute.MethodResultCompareValue));
                }

                var theLambda = Expression.Lambda<Func<T, bool>>(theOperation, lhsParam);

                if (isGroupOperationAND)
                {
                    predicate = predicate.And(theLambda);
                }
                else
                {
                    predicate = predicate.Or(theLambda);
                }
            }

            return predicate;
        }

        private static GridSearchOperationNodeTypeAttribute ExtractOperationComparisonType(GridSearchOperation op)
        {
            //get the attribute and associated NodeType
            var memberInfo = typeof(GridSearchOperation).GetMember(op.ToString());
            object[] attrInfo = memberInfo[0].GetCustomAttributes(typeof(GridSearchOperationNodeTypeAttribute), false);
            var theAttribute = attrInfo[0] as GridSearchOperationNodeTypeAttribute;
            return theAttribute;
        }

        public static Type GetPropertyTypeOfPropertyName<T>(string propertyName)
        {
            PropertyInfo pi = typeof(T).GetProperty(propertyName);
            return pi.PropertyType;
        }

        public static Expression<Func<TObjectType, TTargetType>> GetOrderByClause<TObjectType, TTargetType>(string propertyName)
        {
            PropertyInfo pi = typeof(TObjectType).GetProperty(propertyName);
            ParameterExpression lhsParam = Expression.Parameter(typeof(TObjectType), "o");
            Expression orderBy = Expression.Property(lhsParam, pi);

            var orderByLambda = Expression.Lambda<Func<TObjectType, TTargetType>>(orderBy, lhsParam);
            return orderByLambda;
        }
    }
}