using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using DatabaseLayer.Entities;
using DatabaseLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    PartyIdentifier = p.PartyIdentifier,
                    Students = p.Students,
                    DateCreated = p.CreatedDate
                });

            return null;
        }

        public PartyDTO? GetById(string id)
        {
            IEnumerable<Party>? parties = _unitOfWork.PartyRepository
                .GetAll();

            if (parties is not null)
            {
                Party? party = parties.FirstOrDefault(p => p.Id == id);

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
        public IEnumerable<PartyDTO>? GetPartiesByName(string name)
        {
            Regex regex = new Regex($@"\w*{name}\w*", RegexOptions.IgnoreCase);

            List<PartyDTO>? dtos = GetAll()?
                .ToList();

            if(dtos is not null)
            {
                foreach(var d in dtos.ToList())
                {
                    if(!regex.IsMatch(d.Name))
                        dtos.Remove(d);
                }
            }

            return dtos;
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

        public async Task<bool> AddStudent(string id)
        {
            bool response = await _unitOfWork.PartyRepository
                .AddStudent(id);
            await _unitOfWork.Save();

            return response;
        }

        public IEnumerable<PartyDTO>? FilterParties(FilterDTO filter)
        {
            var dtos = GetAll();

            if(dtos is not null)
            {
                if(filter.Alphabetically != 0)
                    dtos = AlphabeticallyFilter(dtos, filter.Alphabetically);

                if(filter.Students != 0)
                    dtos = StudentsFilter(dtos, filter.Students);

                if (filter.Date != 0)
                    dtos = DateFilter(dtos, filter.Date);
            }

            return dtos;
        }

        private IEnumerable<PartyDTO>? AlphabeticallyFilter(IEnumerable<PartyDTO> dtos, int number)
        {
            switch(number)
            {
                case 1: return dtos.OrderBy(p => p.Name);
                case -1: return dtos.OrderBy(p => p.Name)
                        .Reverse();
                default: return dtos;
            }
        }

        private IEnumerable<PartyDTO> StudentsFilter(IEnumerable<PartyDTO> dtos, int number)
        {
            switch (number)
            {
                case 1: return dtos.OrderBy(p => p.Students);
                case -1: return dtos.OrderBy(p => p.Students)
                        .Reverse();
                default: return dtos;
            }
        }

        private IEnumerable<PartyDTO> DateFilter(IEnumerable<PartyDTO> dtos, int number)
        {
            switch (number)
            {
                case 1: return dtos.OrderBy(p => p.DateCreated);
                case -1:
                    return dtos.OrderBy(p => p.DateCreated)
                        .Reverse();
                default: return dtos;
            }
        }
    }
}
