using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("attribute-option-options"), Authorize]
    public class AttributeOptionController : BaseController
    {
        private readonly IAttributeOptionService _attributeOptionService;

        public AttributeOptionController(IAttributeOptionService attributeOptionService)
        {
            _attributeOptionService = attributeOptionService;
        }

        [HttpGet("", Name = "attribute-option-index"), Permission("attribute_option_view"), Toast]
        public IActionResult Index()
        {
            Title = "Attribute Options";

            var attributeOptions = _attributeOptionService.GetAllAttributeOptions().ToList();

            AddPageHeader(Title);
            return View(attributeOptions);
        }

        [HttpPost("", Name = "attribute-option-add"), Permission("attribute_option_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(AttributeOption attributeOption)
        {
            attributeOption.Name = attributeOption.Name;

            attributeOption = await _attributeOptionService.CreateAsync(attributeOption);
            if (attributeOption == null)
            {
                ToastError("Attribute Option could not be created!");
            }
            else
            {
                ToastSuccess("Attribute Option created successfully!");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "attribute-option-edit"), Permission("attribute_option_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, AttributeOption attributeOption)
        {
            if (_attributeOptionService.IsDuplicate(attributeOption))
            {
                ToastError($"An attributeOption with the name '{attributeOption.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            var toUpdate = await _attributeOptionService.FindByIdAsync(id);
            toUpdate.Name = attributeOption.Name;

            toUpdate = await _attributeOptionService.UpdateAsync(toUpdate);

            if (toUpdate != null)
            {
                ToastSuccess("Attribute Option updated successfully!");
            }
            else
            {
                ToastError("Attribute Option could not be updated");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "attribute-option-delete"), Permission("attribute_option_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var attributeOption = await _attributeOptionService.FindByIdAsync(id);
            if (attributeOption.IsNull())
            {
                return NotFound();
            }

            await _attributeOptionService.DeleteAttributeOptionAsync(attributeOption);

            ToastSuccess("Attribute Option deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<PartialViewResult> EditAttributeOptionModal(ushort id)
        {
            var attributeOption = await _attributeOptionService.FindByIdAsync(id);

            return PartialView("../AttributeOption/_EditAttributeOptionModal", attributeOption);
        }

        [AcceptVerbs("GET", Route = "validate-attribute-option-name", Name = "ValidateAttributeOptionName")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _attributeOptionService.IsDuplicate(name, id)
                ? Json("An attribute with this name already exists")
                : Json(true);
        }
    }
}
