﻿using Microservices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task AddRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
    }
}
