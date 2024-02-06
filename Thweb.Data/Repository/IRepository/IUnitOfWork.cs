using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thweb.Data.Repository.IRepository
{
   public interface IUnitOfWork
    {
        IThwebUserRepository ThwebUser { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IImageRepository Image { get; }

        void Save();
    }

}

