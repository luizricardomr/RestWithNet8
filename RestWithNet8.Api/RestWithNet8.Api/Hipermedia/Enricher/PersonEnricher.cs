using Microsoft.AspNetCore.Mvc;
using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Hipermedia.Constants;
using System.Text;

namespace RestWithNet8.Api.Hipermedia.Enricher
{
    public class PersonEnricher : ContentResponserEnricher<PersonVO>
    {
        protected override Task EnricherModel(PersonVO content, IUrlHelper urlHelper)
        {
            var path = "api/person";
            string link = GetLink(content.Id, urlHelper, path);

            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });
            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.PATCH,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPatch
            });
            return Task.CompletedTask;
        }

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (this)
            {
                var url = new {Controller = path, id};
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
