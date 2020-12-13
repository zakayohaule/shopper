using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("expenditure-types"), Authorize]
    public class ExpenditureTypeController : BaseController
    {
        private readonly IExpenditureTypeService _expenditureTypeService;

        public ExpenditureTypeController(IExpenditureTypeService expenditureTypeService)
        {
            _expenditureTypeService = expenditureTypeService;
        }

        [HttpGet("", Name = "expenditure-type-index"), Permission("expenditure_type_view"), Toast]
        public IActionResult Index()
        {
            Title = "Expenditure types";

            var expenditureTypes = _expenditureTypeService.GetAllExpenditureTypes().ToList();

            AddPageHeader(Title);
            return View(expenditureTypes);
        }

        [HttpPost("", Name = "expenditure-type-add"), Permission("expenditure_type_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(ExpenditureType expenditureType)
        {
            expenditureType.Name = expenditureType.Name;

            expenditureType = await _expenditureTypeService.CreateAsync(expenditureType);
            if (expenditureType == null)
            {
                ToastError("Expenditure type could not be created!");
            }
            else
            {
                ToastSuccess("Expenditure type created successfully!");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "expenditure-type-edit"), Permission("expenditure_type_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, ExpenditureType expenditureType)
        {
            if (_expenditureTypeService.IsDuplicate(expenditureType))
            {
                ToastError($"A expenditure type with the name '{expenditureType.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            var toUpdate = await _expenditureTypeService.FindByIdAsync(id);
            toUpdate.Name = expenditureType.Name;

            toUpdate = await _expenditureTypeService.UpdateAsync(toUpdate);

            if (toUpdate != null)
            {
                ToastSuccess("Expenditure type updated successfully!");
            }
            else
            {
                ToastError("Expenditure type could not be updated");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "expenditure-type-delete"), Permission("expenditure_type_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var expenditureType = await _expenditureTypeService.FindByIdAsync(id);
            if (expenditureType.IsNull())
            {
                return NotFound();
            }

            await _expenditureTypeService.DeleteExpenditureTypeAsync(expenditureType);

            ToastSuccess("Expenditure type deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<JsonResult> EditExpenditureTypeModal(ushort id)
        {
            var expenditureType = await _expenditureTypeService.FindByIdAsync(id);

            return Json(expenditureType, new JsonSerializerSettings{ContractResolver = null});
        }

        [AcceptVerbs("GET", Route = "validate-expenditure-type-name", Name = "ValidateExpenditureTypeName")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _expenditureTypeService.IsDuplicate(name, id)
                ? Json("A expenditure type with this name already exists")
                : Json(true);
        }
    }
}
