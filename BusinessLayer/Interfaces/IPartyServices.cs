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
        PartyDTO? GetByName(string name);
        PartyDTO? GetByPartyId(string partyId);
        Task<bool> CreateAsync(PartyDTO dto);
    }
}
