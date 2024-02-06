using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thweb.Model.Model;

namespace Thweb.Data.Repository.IRepository
{
    public interface IImageRepository : IRepository<Image>
    {
        public void Update(Image image);
    }
}
