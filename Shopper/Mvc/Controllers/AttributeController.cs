using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("attributes"), Authorize]
    public class AttributeController : BaseController
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet("", Name = "attribute-index"), Permission("attribute_view"), Toast]
        public IActionResult Index()
        {
            Title = "Product Attributes";

            var attributes = _attributeService.GetAllAttributes().ToList();

            AddPageHeader(Title);
            return View(attributes);
        }

        [HttpPost("", Name = "attribute-add"), Permission("attribute_add"), ValidateAntiForgeryToken,
            /*ValidateModelWithRedirect()*/]
        public async Task<IActionResult> Create(Attribute attribute)
        {
            attribute.Name = attribute.Name;

            attribute = await _attributeService.CreateAsync(attribute);
            if (attribute == null)
            {
                ToastError("Attribute could not be created!");
            }
            else
            {
                ToastSuccess("Attribute created successfully!");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}", Name = "attribute-edit"), Permission("attribute_edit"), ValidateAntiForgeryToken,]
        public async Task<IActionResult> Update(ushort id, Attribute attribute)
        {
            if (_attributeService.IsDuplicate(attribute))
            {
                ToastError($"An attribute with the name '{attribute.Name}', already exists");
                return RedirectToAction(nameof(Index));
            }

            var toUpdate = await _attributeService.FindByIdAsync(id);
            toUpdate.Name = attribute.Name;

            toUpdate = await _attributeService.UpdateAsync(toUpdate);

            if (toUpdate != null)
            {
                ToastSuccess("Attribute updated successfully!");
            }
            else
            {
                ToastError("Attribute could not be updated");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete", Name = "attribute-delete"), Permission("attribute_delete")]
        public async Task<IActionResult> Delete(ushort id)
        {
            var attribute = await _attributeService.FindByIdAsync(id);
            if (attribute.IsNull())
            {
                return NotFound();
            }

            await _attributeService.DeleteAttributeAsync(attribute);

            ToastSuccess("Attribute deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal"), Permission("attribute_edit")]
        public async Task<JsonResult> EditAttributeModal(ushort id)
        {
            var attribute = await _attributeService.FindByIdAsync(id);

            return Json(attribute, new JsonSerializerSettings{ContractResolver = null});
        }

        [AcceptVerbs("GET", Route = "validate-attribute-name", Name = "ValidateAttributeName"), Permission("attribute_add")]
        public IActionResult ExistsByDisplayName(string name, ushort id)
        {
            return _attributeService.IsDuplicate(name, id)
                ? Json("An attribute with this name already exists")
                : Json(true);
        }
    }
}
