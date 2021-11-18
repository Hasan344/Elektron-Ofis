using System.Collections.Generic;
using System.Linq;
using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.Ef
{
    public class EfXercDal : EfEntityRepositoryBase<Xerc, OfficeContext>, IXercDal
    {
        public List<Xerc> GetXerclerWithXercTeyinati()
        {
             using (var context=new OfficeContext())
             {
                 return context.Xercler.Include(i=>i.XercTeyinati).ToList();             
             }
        }
    }
}