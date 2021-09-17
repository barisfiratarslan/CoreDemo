using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        void CatgeoryAdd(Category category);
        void CatgeoryDelete(Category category);
        void CatgeoryUpdate(Category category);
        List<Category> GetList();
        Category GetByID(int id);
    }
}
