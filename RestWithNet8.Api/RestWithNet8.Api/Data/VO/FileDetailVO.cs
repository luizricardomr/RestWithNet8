namespace RestWithNet8.Api.Data.VO
{
    public class FileDetailVO
    {
        public string DocumentName { get; set; }
        public string DocType { get; set; }
        public string DocUrl { get; set; }
        public IFormFile file {  get; set; }
    }
}
