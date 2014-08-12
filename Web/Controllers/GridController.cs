using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;
using System.Configuration;
using AgileWays.ExpressionSearch2.Repository;
using AgileWays.ExpressionSearch2.Repository.Entity;
using Web.Common;
using LinqKit;
using AgileWays.ExpressionSearch.Service;

namespace Web.Controllers
{
    public class GridController : ApiController
    {
        string _connectionString, _dbName;
        IOpConfig _config;
        ExpressionOpFactory _factory;
        public GridController()
        {
            _connectionString = "mongodb://localhost:27020";
            _dbName = "Baseball2013";

            _config = new OpConfig()
                        .Add("eq", new ExpressionBehavior { IsBinary = true, ExpressionType = ExpressionType.Equal })
                        .Add("ne", new ExpressionBehavior { IsBinary = true, ExpressionType = ExpressionType.NotEqual })
                        .Add("gt", new ExpressionBehavior { IsBinary = true, ExpressionType = ExpressionType.GreaterThan })
                        .Add("ge", new ExpressionBehavior { IsBinary = true, ExpressionType = ExpressionType.GreaterThanOrEqual })
                        .Add("lt", new ExpressionBehavior { IsBinary = true, ExpressionType = ExpressionType.LessThan })
                        .Add("le", new ExpressionBehavior { IsBinary = true, ExpressionType = ExpressionType.LessThanOrEqual })
                        .Add("bw", new ExpressionBehavior { IsBinary = false, MethodResultCompareValue = true, ExpressionType = ExpressionType.Equal, UseMethod = true, Method = "StartsWith" })
                        .Add("bn", new ExpressionBehavior { IsBinary = false, MethodResultCompareValue = false, ExpressionType = ExpressionType.Equal, UseMethod = true, Method = "StartsWith" })
                        .Add("ew", new ExpressionBehavior { IsBinary = false, MethodResultCompareValue = true, ExpressionType = ExpressionType.Equal, UseMethod = true, Method = "EndsWith" })
                        .Add("en", new ExpressionBehavior { IsBinary = false, MethodResultCompareValue = false, ExpressionType = ExpressionType.Equal, UseMethod = true, Method = "EndsWith" })
                        .Add("cn", new ExpressionBehavior { IsBinary = false, MethodResultCompareValue = true, ExpressionType = ExpressionType.Equal, UseMethod = true, Method = "Contains" })
                        .Add("nc", new ExpressionBehavior { IsBinary = false, MethodResultCompareValue = false, ExpressionType = ExpressionType.Equal, UseMethod = true, Method = "Contains" });
            _factory = new ExpressionOpFactory(_config);
        }

        [Web.Common.GridDataFilter]
        public GridData GetData(GridOptions options)
        {
            int skipValue = ((options.Page - 1) * options.Rows);

            using (var ctx = new BaseballContext(_connectionString, _dbName, "complexBatter"))
            {
                var results = ctx.SelectAll<ExtendedBatter>()
                                    .Where(b => b.Year > 2005)
                                    .Take(500);

                return CreateJSONStringForGridData(options, skipValue, results);
            }
        }


        [Web.Common.GridDataFilter]
        public GridData GetData2(GridOptions options)
        {
            int skipValue = ((options.Page - 1) * options.Rows);
            using (var ctx = new BaseballContext(_connectionString, _dbName, "complexBatter"))
            {
                IEnumerable<ExtendedBatter> results;
                if (options.IsSearch)
                {
                    if (options.Filters.FilterRules[0].Operation.Equals("eq"))
                    {
                        results = ctx.SelectAll<ExtendedBatter>()
                                    .Where(b => b.HomeRuns == Convert.ToInt32(options.Filters.FilterRules[0].FieldData));
                    }
                    else if (options.Filters.FilterRules[0].Operation.Equals("ne"))
                    {
                        results = ctx.SelectAll<ExtendedBatter>()
                                    .Where(b => b.HomeRuns != Convert.ToInt32(options.Filters.FilterRules[0].FieldData))
                                    .Take(500);
                    }
                    else
                    {
                        results = ctx.SelectAll<ExtendedBatter>()
                                        .Where(b => b.Year > 2005)
                                        .Take(500);
                    }
                }
                else
                {
                    results = ctx.SelectAll<ExtendedBatter>()
                                    .Where(b => b.Year > 2005)
                                    .Take(500);
                }

                return CreateJSONStringForGridData(options, skipValue, results);
            }
        }


        [Web.Common.GridDataFilter]
        public GridData GetData3(GridOptions options)
        {
            int skipValue = ((options.Page - 1) * options.Rows);
            using (var ctx = new BaseballContext(_connectionString, _dbName, "complexBatter"))
            {
                IEnumerable<ExtendedBatter> results;
                Expression<Func<ExtendedBatter, bool>> predicate;

                if (options.IsSearch)
                {
                    predicate = SearchHelper.CreateSearchPredicate<ExtendedBatter>(_factory, options);
                }
                else
                {
                    predicate = b => b.Year > 2005;
                }

                results = ctx.SelectAll<ExtendedBatter>()
                                .AsExpandable()     //special thing for EF and Mongo
                                .Where(predicate);

                if (!String.IsNullOrWhiteSpace(options.SortIndex))
                {
                    if (options.SortOrder.Equals("asc", StringComparison.CurrentCultureIgnoreCase))
                    {
                        results = results.AsQueryable().OrderBy(SearchHelper.GetOrderByClause<ExtendedBatter, Int32>(options.SortIndex));
                    }
                    else
                    {
                        results = results.AsQueryable().OrderByDescending(SearchHelper.GetOrderByClause<ExtendedBatter, Int32>(options.SortIndex));
                    }
                }

                results = results.Take(500);

                return CreateJSONStringForGridData(options, skipValue, results);
            }
        }

        private static GridData CreateJSONStringForGridData(GridOptions options, int skipValue, IEnumerable<ExtendedBatter> results)
        {
            List<GridRow> rows = results.Skip(skipValue).Take(options.Rows).Select(r => new GridRow
                {
                    ID = r.PlayerId,
                    RowData = new string[] { r.FirstName, r.LastName, r.Games.ToString(), r.Hits.ToString(), r.Doubles.ToString(), r.Triples.ToString(), r.HomeRuns.ToString(), r.RunsBattedIn.ToString(), r.Salary.ToString(), r.TeamId, r.Year.ToString() }
                }).ToList();
            int resultCount = results.Count();

            return new GridData()
            {
                CurrentPage = options.Page,
                TotalRecords = resultCount,
                TotalPages = (resultCount / options.Rows) + 1,
                GridRows = rows
            };
        }
    }
}
