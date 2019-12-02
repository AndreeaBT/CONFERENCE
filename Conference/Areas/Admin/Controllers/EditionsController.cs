using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Conference.Areas.Admin.Models;
using Conference.Domain.Entities;
using Conference.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace Conference.Controllers
{
    public class EditionsController : Controller
    {
        private readonly IEditionService editionService;
        public EditionsController(IEditionService editionService)
        {
            this.editionService = editionService;
        }



        [Area("Admin")]
        // GET: Editions
        public ActionResult Index()
        {
            var allEditions = editionService.GetAllEditions();
            return View(allEditions);
        }

        [Area("Admin")]
        // GET: Editions/Details/5
        public ActionResult Details(int id)
        {
            //Editions editions = editionService.GetEditionById(id);
            return View();
        }

        [Area("Admin")]
        // GET: Editions/Create
        public ActionResult Create()
        {
            return View();
        }

        [Area("Admin")]
        // POST: Editions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Editions editionToAdd = new Editions();
                editionToAdd.InjectFrom(model);
                var addedEdition = editionService.AddEdition(editionToAdd);
                if (addedEdition == null)
                {
                    ModelState.AddModelError("Name", "Edition name nust be unique!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View(model);
            }
        }

        [Area("Admin")]
        // GET: Editions/Edit/5
        public ActionResult Edit(int id)
        {
            var edition = editionService.GetEditionById(id);
            EditionViewModel model = new EditionViewModel();
            model.InjectFrom(edition);
            return View();
        }

        [Area("Admin")]
        // POST: Editions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingEdition = editionService.GetEditionById(id);
                TryUpdateModelAsync(existingEdition);
                editionService.UpdateEdition(existingEdition);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        [Area("Admin")]
        // GET: Editions/Delete/5
        public ActionResult Delete(int id, bool? saveChangesError = false)
        {


            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if problem persists, contact your system administrator.";
            }

            Editions editionToDelete = editionService.GetEditionById(id);
            if (editionToDelete == null)
            {
                return NotFound();
            }
            else
            {
                return View(editionToDelete);
            }
        }

        [Area("Admin")]
        // POST: Editions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Editions editionToDelete = editionService.GetEditionById(id);
                editionService.Delete(editionToDelete);
                editionService.Save();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction(nameof(Index));
        }
        //public ActionResult Delete(int id, EditionViewModel model)
        //{
        //    Editions deleteEdition = new Editions();
        //    deleteEdition = editionService.GetEditionById(id);
        //    model.InjectFrom(deleteEdition);
        //    editionService.Delete(deleteEdition);
        //    return RedirectToAction(nameof(Index));
    }
    
}