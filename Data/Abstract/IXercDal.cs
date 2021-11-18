using System.Collections.Generic;
using Entity;

namespace Data.Abstract
{
    public interface IXercDal:IEntityRepository<Xerc>
    {
         List<Xerc> GetXerclerWithXercTeyinati();
    }
}