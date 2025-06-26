using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]/{id?}")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ContactViewModel();
            var result = await _contactService.GetAllContacts();
            if (!result.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View(viewModel);
            }
            viewModel.Contacts = result.Data;
            return View(viewModel);
        }
    }
}
