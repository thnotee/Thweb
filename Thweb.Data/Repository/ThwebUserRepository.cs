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
    public class ThwebUserRepository : Repository<ThwebUser>, IThwebUserRepository
    {

        private ThwebDbContext _db;

        public ThwebUserRepository(ThwebDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ThwebUser applicationUser)
        {
            _db.ThwebUsers.Update(applicationUser);
        }
    }
}
