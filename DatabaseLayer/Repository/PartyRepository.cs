using DatabaseLayer.Database;
using DatabaseLayer.Entities;
using DatabaseLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repository
{
    internal class PartyRepository: Repository<Party>
    {
        private AppDatabase db;

        public PartyRepository(AppDatabase db) => this.db = db;

        public IEnumerable<Party>? GetAll() => db.Parties;
        
        public async Task Create(Party party) => await db.Parties
            .AddAsync(party);
    }
}
