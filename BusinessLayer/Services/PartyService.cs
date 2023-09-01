using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using DatabaseLayer.Entities;
using DatabaseLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class PartyService: IPartyServices
    {
        private IUnitOfWork _unitOfWork;

        public PartyService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IEnumerable<PartyDTO>? GetAll()
        {
            IEnumerable<Party>? parties = _unitOfWork.PartyRepository
                .GetAll();

            if (parties is not null)
                return parties.Select(p => new PartyDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PartyIdentifier = p.PartyIdentifier
                });

            return null;
        }

        public PartyDTO? GetByName(string name)
        {
            IEnumerable<Party>? parties = _unitOfWork.PartyRepository
                .GetAll();

            if(parties is not null)
            {
                Party? party = parties.FirstOrDefault(p => p.Name == name);

                if (party is not null)
                    return new PartyDTO
                    {
                        Id = party.Id,
                        Name = party.Name,
                        Description = party.Description,
                        PartyIdentifier = party.PartyIdentifier
                    };
            }

            return null;
        }

        public PartyDTO? GetByPartyId(string partyId)
        {
            IEnumerable<Party>? parties = _unitOfWork.PartyRepository
                .GetAll();

            if (parties is not null)
            {
                Party? party = parties.FirstOrDefault(p => p.PartyIdentifier == partyId);

                if (party is not null)
                    return new PartyDTO
                    {
                        Id = party.Id,
                        Name = party.Name,
                        Description = party.Description,
                        PartyIdentifier = party.PartyIdentifier
                    };
            }

            return null;
        }

        public async Task<bool> CreateAsync(PartyDTO dto)
        {
            PartyDTO? search = GetByName(dto.Name);
            string partyIdentifier;

            if (search is null)
            {

                while (true)
                {
                    partyIdentifier = GetPartyIdentifier();

                    if (GetByPartyId(partyIdentifier) is null)
                        break;
                }

                Party party = new Party
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    PartyIdentifier = partyIdentifier
                };

                await _unitOfWork.PartyRepository.Create(party);

                return true;
            }

            return false;
        }

        private string GetPartyIdentifier()
        {
            Random random = new Random();

            return $"({random.Next(0, 9)})-{random.Next(10, 99)}-{random.Next(10, 99)}";
        }
    }
}
