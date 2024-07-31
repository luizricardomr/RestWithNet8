using RestWithNet8.Api.Hipermedia;
using RestWithNet8.Api.Hipermedia.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithNet8.Api.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public long Id {  get; set; }
        public string Author { get; set; }
        public DateTime LauchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public List<HyperMediaLink> links { get; set; } = new List<HyperMediaLink>();
    }
}
