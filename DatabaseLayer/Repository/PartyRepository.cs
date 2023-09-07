using DatabaseLayer.Database;
using DatabaseLayer.Entities;
using DatabaseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repository
{
    internal class PartyRepository: IRepository<Party>
    {
        private AppDatabase db;

        public PartyRepository(AppDatabase db) => this.db = db;

        public IEnumerable<Party>? GetAll() => db.Parties;
        
        public async Task Create(Party party) => await db.Parties
            .AddAsync(party);

        public async Task<bool> AddStudent(string id)
        {
            Party? party = await db.Parties
                .FirstOrDefaultAsync(p => p.Id == id);

            if (party is not null)
            {
                party.Students += 1;

                return true;
            }

            return false;
        }
    }
}
