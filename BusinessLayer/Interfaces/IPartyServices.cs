using BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPartyServices
    {
        IEnumerable<PartyDTO>? GetAll();
        PartyDTO? GetById(string id);
        PartyDTO? GetByName(string name);
        PartyDTO? GetByPartyId(string partyId);
        IEnumerable<PartyDTO>? GetPartiesByName(string name);
        Task<bool> CreateAsync(PartyDTO dto);
        Task<bool> AddStudent(string id);
    }
}
