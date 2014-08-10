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

namespace Web.Controllers
{
    public class GridController : ApiController
    {
        string _connectionString, _dbName;
        public GridController()
        {
            _connectionString = "mongodb://localhost:27020";
            _dbName = "Baseball2013";
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
                    if (options.Filters.FilterRules[0].Operation == GridSearchOperation.EQ)
                    {
                        results = ctx.SelectAll<ExtendedBatter>()
                                    .Where(b => b.HomeRuns == Convert.ToInt32(options.Filters.FilterRules[0].FieldData));
                    }
                    else if (options.Filters.FilterRules[0].Operation == GridSearchOperation.NE)
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
                    predicate = SearchHelper.CreateSearchPredicate(options);
                }
                else
                {
                    predicate = b => b.Year > 2005;
                }

                results = ctx.SelectAll<ExtendedBatter>()
                                .AsExpandable()     //special thing for EF and Mongo
                                .Where(predicate)
                                .Take(500);

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
