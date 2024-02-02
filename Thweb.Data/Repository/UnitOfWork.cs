using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thweb.Data.DbContext;
using Thweb.Data.Repository.IRepository;

namespace Thweb.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ThwebDbContext _db;
        public IThwebUserRepository ThwebUser { get; private set; }
        public UnitOfWork(ThwebDbContext db)
        {
            _db = db;
            ThwebUser = new ThwebUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
