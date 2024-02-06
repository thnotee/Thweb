using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thweb.Data.DbContext;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;

namespace Thweb.Data.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {

        private ThwebDbContext _db;

        public ImageRepository(ThwebDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Image image)
        {
            _db.Image.Update(image);
        }
    }
}
