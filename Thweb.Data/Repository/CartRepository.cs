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
    public class CartRepository : Repository<Cart>, ICartRepository
    {

        private ThwebDbContext _db;

        public CartRepository(ThwebDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Cart obj)
        {
            _db.Cart.Update(obj);
        }
    }
}
