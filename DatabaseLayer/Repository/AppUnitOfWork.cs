using DatabaseLayer.Database;
using DatabaseLayer.Entities;
using DatabaseLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repository
{
    internal class AppUnitOfWork: IUnitOfWork
    {
        private AppDatabase db;

        private PartyRepository? partyRepository;
        private bool disposed = false;

        public AppUnitOfWork(AppDatabase db) => this.db = db; 

        public IRepository<Party> PartyRepository
        {
            get
            {
                if (partyRepository is null)
                    partyRepository = new PartyRepository(db);

                return partyRepository;
            }
        }

        public async Task Save() => await db.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                db.Dispose();
                disposed = true;
            }
        }
    }
}
