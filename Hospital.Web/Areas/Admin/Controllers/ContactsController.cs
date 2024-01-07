using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ContactsController : Controller
    {
        private IContactService _contactService;
        private IHospitalInfo _hospitalInfo;

        public ContactsController(IContactService contactService, IHospitalInfo hospitalInfo)
        {
            _contactService = contactService;
            _hospitalInfo   = hospitalInfo;
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_contactService.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(),"Id","Name");
            var viewModel = _contactService.GetConatcById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ContactViewModel vm)
        {
            _contactService.UpdateContact(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactViewModel vm)
        {
            _contactService.InsertContact(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _contactService.DeleteContact(id);
            return RedirectToAction("Index");
        }
    }
}
