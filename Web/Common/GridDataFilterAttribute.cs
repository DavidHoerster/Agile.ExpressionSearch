using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Filters;
using Web.Models;

namespace Web.Common
{
    public class GridDataFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            GridOptions requestOptions = new GridOptions();

            var requestQuery = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);
            if (requestQuery.HasKeys())
            {
                GridFilters filterOps = null;
                string filterString = requestQuery["filters"];
                if (!String.IsNullOrWhiteSpace(filterString))
                {
                    filterOps = Newtonsoft.Json.JsonConvert.DeserializeObject<GridFilters>(filterString);
                }
                requestOptions.Filters = filterOps;
                requestOptions.IsSearch = Boolean.Parse(requestQuery["_search"]);
                requestOptions.ND = requestQuery["nd"];
                requestOptions.Page = Int32.Parse(requestQuery["page"]);
                requestOptions.Rows = Int32.Parse(requestQuery["rows"]);
                requestOptions.SortIndex = requestQuery["sidx"];
                requestOptions.SortOrder = requestQuery["sord"];
            }

            if (actionContext.ActionArguments.ContainsKey("options"))
            {
                actionContext.ActionArguments["options"] = requestOptions;
            }
            else
            {
                actionContext.ActionArguments.Add("options", requestOptions);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}