using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithNet8.Api.Hipermedia.Abstract;
using RestWithNet8.Api.Hipermedia.Utils;
using System.Collections.Concurrent;

namespace RestWithNet8.Api.Hipermedia
{
    public abstract class ContentResponserEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {
        protected ContentResponserEnricher()
        {

        }

        public bool CanEnriche(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>) || contentType == typeof(PagedSearchVO<T>);
        }

        protected abstract Task EnricherModel(T content, IUrlHelper urlHelper);
        bool IResponseEnricher.CanEnriche(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult ok)
            {
                return CanEnriche(ok.Value.GetType());
            }
            return false;
        }

        public async Task Enriche(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if (response.Result is OkObjectResult ok)
            {
                if (ok.Value is T model)
                {
                    await EnricherModel(model, urlHelper);
                }
                else if (ok.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnricherModel(element, urlHelper);
                    });
                }
                else if (ok.Value is PagedSearchVO<T> pagedSearch)
                {
                    Parallel.ForEach(pagedSearch.List.ToList(), (element) =>
                    {
                        EnricherModel(element, urlHelper);
                    });
                }
            }

        }

    }
}
