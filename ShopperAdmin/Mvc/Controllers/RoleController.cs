using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopperAdmin.Extensions.Helpers;
using ShopperAdmin.Mvc.Entities.Identity;
using ShopperAdmin.Attributes;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Mvc.Controllers
{
    [Route("roles")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<Role> _roleManager;

        [ViewData]
        public string Title { get; set; } = "Roles";

        public RoleController(IRoleService roleService, RoleManager<Role> roleManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
        }

        // GET
        [HttpGet(""), Permission("role_view")]
        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles().ToList();

            return View(roles);
        }

        [HttpPost(""),Permission("role_add"), ValidateAntiForgeryToken,
         /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(Role role)
        {
            IdentityResult result;
            if (_roleService.ExistsByDisplayName(role.DisplayName))
            {
                var deleted = await _roleService.FindByDisplayName(role.DisplayName);
                deleted.DisplayName = role.DisplayName;
                result = await _roleManager.UpdateAsync(deleted);
            }
            else
            {
                var roleName = _roleService.GenerateRoleName(role.DisplayName);
                role.Name = roleName;
                result = await _roleManager.CreateAsync(role);
            }
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Role could not be created";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}"),Permission("role_edit"), ValidateAntiForgeryToken, ]
        public async Task<IActionResult> Update(long id, Role role)
        {
            var toUpdate = await _roleService.FindByIdAsync(id);

            if (toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.DisplayName = role.DisplayName;

            var result = await _roleManager.UpdateAsync(toUpdate);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Role could not be created";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete"), Permission("role_delete")]
        public async Task<IActionResult> Delete(long id)
        {
            var role = await _roleService.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleService.DeleteRoleAsync(role);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/permissions"), Permission("role_permission_view")]
        public IActionResult RolePermissions(long id)
        {
            var rolePermissions = _roleService.RolePermissions(id);

            return View(rolePermissions);
        }

        [AcceptVerbs("GET",Route = "validate-role-name", Name = "ValidateRoleDisplayName")]
        public IActionResult ExistsByDisplayName(string displayName)
        {
            return _roleService.ExistsByDisplayName(displayName)
                ? Json("A role with this name already exists")
                : Json(true);
        }

        [HttpPost("{id}/role-permissions")]
        public async Task<IActionResult> SaveRolePermissions(long id)
        {
            var role = await _roleService.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var permissions = HttpContext.Request.Form["permissions"].ToList();

            await _roleService.SaveRolePermissionsAsync(role, permissions);

            return RedirectToAction(nameof(Index));
        }
    }
}
