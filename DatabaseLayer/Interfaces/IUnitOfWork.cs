﻿using DatabaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Party> PartyRepository { get; }
        Task Save();
    }
}
