using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core.Models;
using Microservices.Services.Interfaces;
using Microservices.Repository.Interfaces;
namespace Microservices.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return _roleRepository.GetAllRolesAsync();
        }

        public Task<Role?> GetRoleByIdAsync(int id)
        {
            return _roleRepository.GetRoleByIdAsync(id);
        }

        public Task AddRoleAsync(Role role)
        {
            return _roleRepository.AddRoleAsync(role);
        }

        public Task UpdateRoleAsync(Role role)
        {
            return _roleRepository.UpdateRoleAsync(role);
        }

        public Task DeleteRoleAsync(int id)
        {
            return _roleRepository.DeleteRoleAsync(id);
        }
    }
}
