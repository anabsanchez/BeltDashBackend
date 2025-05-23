using BeltDash.API.Controllers.Common;
using BeltDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeltDash.API.Controllers.v1
{
    /// <summary>
    /// Controller to manage user roles.
    /// Accessible only to administrators.
    /// </summary>
    public class RolesController : ApiControllerBase
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// Constructor that injects the role service.
        /// </summary>
        /// <param name="roleService">Role service</param>
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Endpoint to get all available roles.
        /// Restricted to users with the 'admin' role.
        /// GET: /api/v1/roles
        /// </summary>
        /// <returns>List of all roles</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRolesAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
