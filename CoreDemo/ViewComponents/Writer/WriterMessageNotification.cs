using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        MessageManager messageManager = new MessageManager(new EfMessageRepository());

        public IViewComponentResult Invoke()
        {
            string p;
            p = "baris@deneme.com";
            var values = messageManager.GetInboxListByWriter(p);
            return View(values);
        }
    }
}
