using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterList()
        {
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }

        public IActionResult GetWriterByID(int writerID)
        {
            var findWriter = writers.FirstOrDefault(x => x.Id == writerID);
            var jsonWriters = JsonConvert.SerializeObject(findWriter);
            return Json(jsonWriters);
        }

        [HttpPost]
        public IActionResult AddWriter(WriterClass writer)
        {
            writers.Add(writer);
            var jsonWriters = JsonConvert.SerializeObject(writer);
            return Json(jsonWriters);
        }

        [HttpPost]
        public IActionResult DeleteWriter(int writerID)
        {
            var writer = writers.FirstOrDefault(x => x.Id == writerID);
            writers.Remove(writer);
            var jsonWriters = JsonConvert.SerializeObject(writer);
            return Json(jsonWriters);
        }

        public static List<WriterClass> writers = new List<WriterClass>()
        {
            new WriterClass
            {
                Id=1,
                Name="Ayşe"
            },
            new WriterClass
            {
                Id=2,
                Name="Ahmet"
            },
            new WriterClass
            {
                Id=3,
                Name="Buse"
            }
        };
    }
}
