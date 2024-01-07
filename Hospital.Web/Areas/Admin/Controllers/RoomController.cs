using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoomController : Controller
    {
        private IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_roomService.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _roomService.GetRoomById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoomViewModel vm)
        {
            _roomService.UpdateRoom(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomViewModel vm)
        {
            _roomService.InsertRoom(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _roomService.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}
