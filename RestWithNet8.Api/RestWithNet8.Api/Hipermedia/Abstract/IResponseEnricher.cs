using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithNet8.Api.Hipermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnriche(ResultExecutingContext context);
        Task Enriche(ResultExecutingContext context);
    }
}
