using Microservices.Core.Models;
using Microservices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            await _roleService.AddRoleAsync(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role role)
        {
            if (id != role.Id)
                return BadRequest("Role ID mismatch.");

            var existing = await _roleService.GetRoleByIdAsync(id);
            if (existing == null)
                return NotFound($"Role with ID {id} not found.");

            await _roleService.UpdateRoleAsync(role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var existing = await _roleService.GetRoleByIdAsync(id);
            if (existing == null)
                return NotFound($"Role with ID {id} not found.");

            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}