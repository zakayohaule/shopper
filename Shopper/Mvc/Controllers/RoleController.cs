using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities.Identity;
using Shopper.Attributes;
using Shopper.Database;
using Shopper.Extensions.Helpers;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("roles")]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<Role> _roleManager;


        public RoleController(IRoleService roleService, RoleManager<Role> roleManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
        }

        // GET
        [HttpGet(""), Permission("role_view"), Toast]
        public IActionResult Index()
        {
            Title = "Roles";
            AddPageHeader("Role Management");
            var roles = _roleService.GetAllRoles().ToList();

            return View(roles);
        }

        [HttpPost(""), Permission("role_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(Role role, [FromServices] ApplicationDbContext dbContext)
        {
            IdentityResult result;
            var roleName = _roleService.GenerateRoleName(role.DisplayName);
            role.Name = roleName;
            role.NormalizedName = $"{role.Name}_{HttpContext.GetCurrentTenant().Id}";
            var newRole = dbContext.Roles.Add(role);
            await dbContext.SaveChangesAsync();
            // result = await _roleManager.CreateAsync(role);
            // }
            if (newRole.Entity != null)
            {
                ToastSuccess("Role created successfully!");
                return RedirectToAction(nameof(Index));
            }

            ToastError("Role could not be created!");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}"), Permission("role_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(long id, Role role)
        {
            var toUpdate = await _roleService.FindByIdAsync(id);

            if (toUpdate.IsNull())
            {
                return NotFound();
            }

            if (_roleService.ExistsByDisplayName(role.DisplayName, id))
            {
                ToastError($"A role with the name '{role.DisplayName}', already exists");
                return RedirectToAction(nameof(Index));
            }
            toUpdate.DisplayName = role.DisplayName;

            var result = await _roleManager.UpdateAsync(toUpdate);

            if (result.Succeeded)
            {
                ToastSuccess("Role updated successfully!");
                return RedirectToAction(nameof(Index));
            }

            ToastError("Role could not be updated");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete"), Permission("role_delete")]
        public async Task<IActionResult> Delete(long id)
        {
            var role = await _roleService.FindByIdAsync(id);
            if (role.IsNull())
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

        [AcceptVerbs("GET", Route = "validate-role-name", Name = "ValidateRoleDisplayName")]
        public IActionResult ExistsByDisplayName(string displayName, long id)
        {
            return _roleService.ExistsByDisplayName(displayName, id)
                ? Json("A role with this name already exists")
                : Json(true);
        }

        [HttpPost("{id}/role-permissions")]
        public async Task<IActionResult> SaveRolePermissions(long id, [FromServices] IUserClaimService userClaimService)
        {
            var role = await _roleService.FindByIdAsync(id);
            if (role.IsNull())
            {
                return NotFound();
            }

            var permissions = HttpContext.Request.Form["permissions"].ToList();
            await _roleService.SaveRolePermissionsAsync(role, permissions);
            await userClaimService.ReCacheUsersRoleClaims(role.Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<JsonResult> EditRoleModal(long id)
        {
            var role = await _roleService.FindByIdAsync(id);

            return Json(role, new JsonSerializerSettings{ContractResolver = null});
        }
    }
}
