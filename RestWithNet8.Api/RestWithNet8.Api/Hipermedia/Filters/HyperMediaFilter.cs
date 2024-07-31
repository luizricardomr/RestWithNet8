using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestWithNet8.Api.Hipermedia.Abstract;

namespace RestWithNet8.Api.Hipermedia.Filters
{
    public class HyperMediaFilter: ResultFilterAttribute
    {
        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;

        public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
        {
            _hyperMediaFilterOptions = hyperMediaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult)
            {
                var enricher = _hyperMediaFilterOptions.ContentResponseEnricherList
                    .FirstOrDefault(x=> x.CanEnriche(context));

                if(enricher != null) Task.FromResult(enricher.Enriche(context));

            }
        }
    }
}
