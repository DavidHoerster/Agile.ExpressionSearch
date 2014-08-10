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
                //var operationAttribute = ExtractOperationComparisonType(rule.Operation);

                PropertyInfo pi = typeof(T).GetProperty(rule.Field);
                ParameterExpression lhsParam = Expression.Parameter(typeof(T));
                Expression lhs = Expression.Property(lhsParam, pi);
                Expression rhs = Expression.Constant(Convert.ChangeType(rule.FieldData, pi.PropertyType));

                //TODO: Not handling strings at the moment!!
                //if (pi.PropertyType.Name == "String")
                //{
                //    ModifyExpressionParametersForStringFunctions(operationAttribute, ref lhs, ref rhs);
                //}

                Expression theOperation;
                //TODO: need to account for binary/unary operations here!
                //if (operationAttribute.IsBinary)
                //{
                    theOperation = Expression.MakeBinary(operationAttribute, lhs, rhs);
                //}
                //else
                //{
                //    theOperation = Expression.MakeUnary(operationAttribute.NodeType, lhs, null);
                //}

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

        //private static void ModifyExpressionParametersForStringFunctions(GridSearchOperationNodeTypeAttribute operationAttribute, ref Expression lhs, ref Expression rhs)
        //{
        //    if (!String.IsNullOrWhiteSpace(operationAttribute.StringManipulationMethod))
        //    {
        //        lhs = Expression.Call(lhs, operationAttribute.StringManipulationMethod, null);
        //        rhs = Expression.Call(rhs, operationAttribute.StringManipulationMethod, null);
        //    }

        //    if (!String.IsNullOrWhiteSpace(operationAttribute.StringComparisonMethod))
        //    {
        //        lhs = Expression.Call(lhs, operationAttribute.StringComparisonMethod, null, rhs);
        //    }
        //}

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