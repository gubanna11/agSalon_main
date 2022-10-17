using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data.Base
{
    public interface IEntityBaseRepository<T> where T:class, IEntityBase, new()
    {

    }
}
