using RestWithNet8.Api.Data.VO;
using System.IO;
using System.Runtime;

namespace RestWithNet8.Api.Business.Implementations
{
    public class FileBusiness : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusiness(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }
        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDatail = new FileDetailVO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" ||
                fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".png" ||
                fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDatail.DocumentName = docName;
                    fileDatail.DocType = fileType;
                    fileDatail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDatail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }

            }
            return fileDatail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>();

            foreach (IFormFile file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }
            return list;
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;
            return File.ReadAllBytes(filePath);
        }




    }
}
