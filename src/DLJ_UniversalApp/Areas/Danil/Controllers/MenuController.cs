using DefenderServices.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DLJ_UniversalApp.Areas.Danil.Controllers
{
    [Area("danil")]
    public class MenuController : Controller
    {
        private readonly IMenuBuilderService menuBuilderService;

        public MenuController(IMenuBuilderService menuBuilderService)
        {
            this.menuBuilderService = menuBuilderService;
        }

        public async Task<IActionResult> Index()
        {
            var menuItemsResult = await menuBuilderService.GetAvailableMenuItems();
            if (menuItemsResult.IsSuccess)
            {
                return View(menuItemsResult.GetData);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CreateNewMenuItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewMenuItem(MenuItem menuItem)
        {
            menuBuilderService.AddMenuItems(menuItem);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMenuItem(string id)
        {
            var menuItemResult = await menuBuilderService.GetMenuItemById(id);

            if (menuItemResult.IsSuccess)
            {
                return View(menuItemResult.GetData);
            }

            return View(new MenuItem());
        }

        [HttpPost]
        public IActionResult UpdateMenuItemP(MenuItem menuItem)
        {
            menuBuilderService.UpdateMenuItems(menuItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveMenuItem(string id)
        {
            var menuItemResult = await menuBuilderService.GetMenuItemById(id);

            if (menuItemResult.IsSuccess)
            {
                return View(menuItemResult.GetData);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveMenuItemP(string id)
        {
            menuBuilderService.RemoveMenuItems(id);
            return RedirectToAction("Index");
        }

    }
}
