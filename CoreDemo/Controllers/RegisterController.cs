using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<string> cities = new List<string>(){ "Adana", "Ankara", "İstanbul", "İzmir", "Muğla", "Sivas", "GaziAntep" };
            IEnumerable<SelectListItem> selectList = (from x in cities
                                               select new SelectListItem()
                                               {
                                                   Text = x,
                                                   Value = x
                                               }).ToList();
            ViewBag.place = selectList;
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegisterDto register)
        {
            WriterValidatator validationRules = new WriterValidatator();
            ValidationResult validationResult = validationRules.Validate(register);

            if (validationResult.IsValid)
            {
                Writer writer = new Writer()
                {
                    WriterID=register.WriterID,
                    WriterImage = register.WriterImage,
                    WriterMail = register.WriterMail,
                    WriterName = register.WriterName,
                    WriterPassword = register.WriterPassword
                };
                writer.WriterStatus = true;
                //writer.WriterAbout = register.WriterPlace;
                writer.WriterAbout = "Deneme Test";

                writerManager.TAdd(writer);
                return RedirectToAction("Index", "Blog");
            }

            IEnumerable<string> cities = new List<string>() { "Adana", "Ankara", "İstanbul", "İzmir", "Muğla", "Sivas", "GaziAntep" };
            IEnumerable<SelectListItem> selectList = (from x in cities
                                                      select new SelectListItem()
                                                      {
                                                          Text = x,
                                                          Value = x
                                                      }).ToList();
            ViewBag.place = selectList;

            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();

        }
    }
}
