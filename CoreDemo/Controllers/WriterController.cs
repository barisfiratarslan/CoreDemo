using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class WriterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            var writerValues = writerManager.GetByID(1);
            RegisterDto register = new RegisterDto()
            {
                WriterAbout = writerValues.WriterAbout,
                ConfirmPassword = writerValues.WriterPassword,
                WriterPassword = writerValues.WriterPassword,
                WriterID = writerValues.WriterID,
                WriterImage = writerValues.WriterImage,
                WriterMail = writerValues.WriterMail,
                WriterName = writerValues.WriterName
            };
            return View(register);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterEditProfile(RegisterDto register)
        {
            WriterValidatator writerValidator = new WriterValidatator();
            ValidationResult results = writerValidator.Validate(register);
            if (results.IsValid)
            {
                Writer writer = new Writer()
                {
                    WriterID = 1,
                    WriterImage = register.WriterImage,
                    WriterMail = register.WriterMail,
                    WriterName = register.WriterName,
                    WriterPassword = register.WriterPassword,
                    WriterAbout=register.WriterAbout
                };
                writerManager.TUpdate(writer);
                return RedirectToAction("Index", "Dashboard");
            }
            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage addProfileImage)
        {
            Writer writer = new Writer();
            if(addProfileImage.WriterImage != null)
            {
                var extension = Path.GetExtension(addProfileImage.WriterImage.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                writer.WriterImage = newImageName;
            }
            writer.WriterMail = addProfileImage.WriterMail;
            writer.WriterName = addProfileImage.WriterName;
            writer.WriterPassword = addProfileImage.WriterPassword;
            writer.WriterStatus = true;
            writer.WriterAbout = addProfileImage.WriterAbout;
            writerManager.TAdd(writer);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
