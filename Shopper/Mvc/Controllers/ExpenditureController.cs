using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Mvc.Entities;
using Shopper.Attributes;
using Shopper.Mvc.ViewModels;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("expenditures"), Authorize]
    public class ExpenditureController : BaseController
    {
        private readonly IExpenditureService _expenditureService;
        private readonly IExpenditureTypeService _expenditureTypeService;

        public ExpenditureController(IExpenditureService expenditureService, IExpenditureTypeService expenditureTypeService)
        {
            _expenditureService = expenditureService;
            _expenditureTypeService = expenditureTypeService;
        }

        [HttpGet("", Name = "expenditure-index"), Permission("expenditure_view"), Toast]
        public IActionResult Index()
        {
            Title = "Expenditures";
            AddPageHeader(Title);
            var expenditures = _expenditureService.GetAllExpenditures().ToList();
            ViewData["ExpenditureTypes"] = _expenditureTypeService.GetExpenditureTypeSelectListItems();
            return View(expenditures);
        }

        [HttpPost("", Name = "expenditure-add"), Permission("expenditure_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(ExpenditureModel formModel)
        {
            var expenditure = new Expenditure
            {
                Date = formModel.Date,
                Amount = uint.Parse(formModel.Amount.Replace(",", "")),
                ExpenditureTypeId = formModel.ExpenditureTypeId,
                Description = formModel.Description
            };

            expenditure = await _expenditureService.CreateAsync(expenditure);
            if (expenditure == null)
            {
                ToastError("Expenditure could not be created!");
            }
            else
            {
                ToastSuccess("Expenditure created successfully!");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "expenditure-edit"), Permission("expenditure_edit"),
         ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ulong id, ExpenditureModel formModel)
        {

            if (!await _expenditureService.ExistsByIdAsync(id))
            {
                return NotFound("Expenditure not found");
            }

            var expenditure = new Expenditure
            {
                Id = id,
                Date = formModel.Date,
                Amount = uint.Parse(formModel.Amount.Replace(",", "")),
                ExpenditureTypeId = formModel.ExpenditureTypeId,
                Description = formModel.Description
            };

            expenditure.Id = id;
            expenditure = await _expenditureService.UpdateAsync(expenditure);

            if (expenditure != null)
            {
                ToastSuccess("Expenditure updated successfully!");
            }
            else
            {
                ToastError("Expenditure could not be updated");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "expenditure-delete"), Permission("expenditure_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            if (!await _expenditureService.ExistsByIdAsync(id))
            {
                return NotFound();
            }

            await _expenditureService.DeleteExpenditureAsync(new Expenditure{Id = id});

            ToastSuccess("Expenditure deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<JsonResult> EditExpenditureModal(ulong id)
        {
            var expenditure = await _expenditureService.FindByIdAsync(id);

            return Json(expenditure, new JsonSerializerSettings{ContractResolver = null});
        }
    }
}
