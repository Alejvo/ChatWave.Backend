﻿using Domain.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGroupRepository:IRepository<Group>
    {
        Task<IEnumerable<Group?>> GetByNames(string name);
        Task AddUserToGroup(string storedProcedure,string groupId,string userId);
        Task RemoveUserToGroup(string storedProcedure, string groupId,string userId);
    }
}
