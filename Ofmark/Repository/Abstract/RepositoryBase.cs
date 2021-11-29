using Ofmark.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Repository.Abstract
{
    public class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _objLock = new object();
        public RepositoryBase()
        {
            if (db==null)
            {
                lock (_objLock)
                {
                    db = new DatabaseContext();
                }
            }
        }
    }
}
