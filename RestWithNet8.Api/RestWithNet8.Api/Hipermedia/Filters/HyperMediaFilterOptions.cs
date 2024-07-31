using RestWithNet8.Api.Hipermedia.Abstract;

namespace RestWithNet8.Api.Hipermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
