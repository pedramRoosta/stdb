﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShirazTronic.Data;
using ShirazTronic.Extensions;
using ShirazTronic.Models;
using ShirazTronic.Models.ViewModels;

namespace ShirazTronic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        ApplicationDbContext db;
        [TempData]
        public string tempMessage { get; set; }
        public SubCategoryController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var subCategories = db.SubCategory.Include(s => s.Category).ToList();
            return View(subCategories);
        }
        public async Task<IActionResult> Create()
        {
            var subCategoryAndCategory = new VmSubcategoryAndCategory()
            {
                Categories = await db.Category.ToListAsync(),
                SubCategory = new SubCategory(),
                // subCategoryList = await db.SubCategory.OrderBy(p => p.Title).Select(p => p.Title).Distinct().ToListAsync()
            };

            return View(subCategoryAndCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<IFormFile> Picture,VmSubcategoryAndCategory model)
        {
            if (ModelState.IsValid)
            {
                var isExistSubCat = db.SubCategory.Include(s => s.Category).Where(s => s.Title == model.SubCategory.Title && s.Category.Id == model.SubCategory.CategoryId);
                if (isExistSubCat.Count() > 0)
                    tempMessage = "Subcategory already exists";
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        Picture[0].CopyTo(ms);
                        model.SubCategory.Picture = ms.ToArray();
                    }
                    db.SubCategory.Add(model.SubCategory);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            VmSubcategoryAndCategory tempModel = new VmSubcategoryAndCategory()
            {
                Categories = db.Category.ToList(),
                SubCategory = model.SubCategory,
                statusMessage = tempMessage,
                //subCategoryList = db.SubCategory.OrderBy(p => p.Title).Select(s => s.Title).ToList()
            };
            return View(tempModel);
        }

        public IActionResult Delete(int id)
        {
            var subCat = db.SubCategory.SingleOrDefault(s => s.Id == id);
            if (subCat == null)
                return NotFound();
            db.SubCategory.Remove(subCat);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            var subCat = db.SubCategory.SingleOrDefault(x => x.Id == id);
            if (subCat == null)
                return NotFound();

            var subCategoryAndCategory = new VmSubcategoryAndCategory()
            {
                Categories = db.Category.ToList(),
                SubCategory = subCat,
                //subCategoryList = await db.SubCategory.OrderBy(p => p.Title).Select(p => p.Title).Distinct().ToListAsync()
            };

            return View(subCategoryAndCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VmSubcategoryAndCategory model)
        {
            if (ModelState.IsValid)
            {
                var subCatinDB = db.SubCategory.SingleOrDefault(s => s.Id == id);
                subCatinDB.Title = model.SubCategory.Title;
                subCatinDB.Picture = model.SubCategory.Picture;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            VmSubcategoryAndCategory tempModel = new VmSubcategoryAndCategory()
            {
                Categories = db.Category.ToList(),
                SubCategory = model.SubCategory,
                statusMessage = "Subcategory already exists",
                //subCategoryList = db.SubCategory.OrderBy(p => p.Title).Select(s => s.Title).ToList()
            };
            return View(tempModel);
        }

        [ActionName("GetSubCategory")]
        public IActionResult GetSubCategory(int id)
        {
            List<SubCategory> subCats = new List<SubCategory>();
            subCats = (from subCategory in db.SubCategory
                       where subCategory.CategoryId == id
                       select subCategory).ToList();
            return Json(new SelectList(subCats, "Id", "Title"));


        }
    }
}