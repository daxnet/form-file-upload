using Amazon.S3;
using Amazon.S3.Transfer;
using FormFileUpload.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataAccessObject _dao;
        private readonly IAmazonS3 _s3;

        public HomeController(ILogger<HomeController> logger, IAmazonS3 s3, IDataAccessObject dao)
         => (_logger, _s3, _dao) = (logger, s3, dao);

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FileUploadModel model)
        {
            if (model.File != null)
            {
                var fileName = Path.GetFileName(model.File.FileName);
                var fileTransUtility = new TransferUtility(_s3);
                using var stream = model.File.OpenReadStream();
                await fileTransUtility.UploadAsync(stream, "data", $"FormFileUpload/{fileName}");
                await _dao.AddAsync(new SavedFile
                {
                    Timestamp = DateTime.UtcNow,
                    FileName = fileName
                });

                return View();
            }
            else
            {

            }
            return View();
        }

        public async Task<IActionResult> S3List()
        {
            var files = await _s3.GetAllObjectKeysAsync("data", "FormFileUpload", null);
            return View(files);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
