using DefenderServices.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var menuItemsResult = await menuBuilderService.GetAvailableMunuItems();
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
            menuBuilderService.AddMunuItems(menuItem);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMenuItem(int id)
        {
            var menuItemResult = await menuBuilderService.GetMunuItemById(id);

            if (menuItemResult.IsSuccess)
            {
                return View(menuItemResult.GetData);
            }

            return View(new MenuItem());
        }

        [HttpPost]
        public IActionResult UpdateMenuItemP(MenuItem menuItem)
        {
            menuBuilderService.UpdateMunuItems(menuItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveMenuItem(int id)
        {
            var menuItemResult = await menuBuilderService.GetMunuItemById(id);

            if (menuItemResult.IsSuccess)
            {
                return View(menuItemResult.GetData);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveMenuItemP(int id)
        {
            menuBuilderService.RemoveMunuItems(id);
            return RedirectToAction("Index");
        }

    }
}
