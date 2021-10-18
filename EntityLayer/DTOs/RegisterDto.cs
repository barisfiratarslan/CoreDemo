using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
    public class RegisterDto
    {
        public string WriterName { get; set; }
        public string WriterPlace { get; set; }
        public string WriterImage { get; set; }
        public string WriterMail { get; set; }
        public string WriterPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
