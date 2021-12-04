using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageRepository());

        public IActionResult Inbox()
        {
            int id = 1;
            var values = messageManager.GetInboxListByWriter(id);
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);
        }
    }
}
