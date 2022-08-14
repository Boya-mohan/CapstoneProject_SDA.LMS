using mohan_CapstoneProject_SDA.LMS.Data;
using mohan_CapstoneProject_SDA.LMS.Data.Services;
using mohan_CapstoneProject_SDA.LMS.Data.Static;
using mohan_CapstoneProject_SDA.LMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Controllers
{
    [Authorize(Roles = UserRoles.Admin)] // V.97
    public class MedicinesController : Controller
    {
        private readonly IMedicinesService _Service;

        public MedicinesController(IMedicinesService Service)
        {
            _Service = Service;
        }



        // <summary>
        [AllowAnonymous] // V.96
        public async Task<IActionResult> Index()
        {
            var Data = await _Service.GetAll();
            return View(Data);
        }// End of GetAll </summary>

        [AllowAnonymous] // V.96
        public async Task<IActionResult> Filter(string searchString) 
        {
            var Allmedicines = await _Service.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                var FilterResult = Allmedicines.Where(n => n.Name.Contains(searchString) || n.MedicineCategory.ToString().Contains(searchString)).ToList(); // Search by name, and by MedicineCategory to sort. MSH
                if (FilterResult.Count != 0)
                {
                    return View("Index", FilterResult);
                }
                TempData["Error"] = "Hmm no result, check letter case OR Sort by use category name"; // Optmize Code [+if&TempData] MSH
            }
            return View("Index",Allmedicines);
        } // End of Filter V.61

        // <summary> Get Medicine for CreateView
        public IActionResult Create()
        {
            return View();
        }
        // Post CreateMedicine
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ImageCode,Name,Description,Price,MedicineCategory")] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return View(medicine);
            }
           await _Service.Add(medicine);
            return RedirectToAction(nameof(Index));

        }// End of Create Get&Post </summary>


        // <summary> Get Medicine for GetEditView + [used to get for delete also]
        public async Task<IActionResult> Edit(int id)
        {
            // <QA> lins insure data still in Db not just in UI _ Case1:
            var MedicineDetails = await _Service.GetByID(id);
            if(MedicineDetails == null ) return View ("NotFounded"); // </QA>

            return View(MedicineDetails);
        }
        // Post EditMedicine
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("ID,ImageCode,Name,Description,Price,MedicineCategory")] Medicine medicine)
        {
            /*// <QA> lins insure data still in Db not just in UI _ Case2:
             var MedicineDetails = await _Service.GetByID(id);         // Error ocurred with QA lins Case2 | MSG_[Model tracked by other instance]
             if (MedicineDetails == null) return View("NotFounded");   // </QA> */   
            if (!ModelState.IsValid)
            {
                return View(medicine);
            }
            //Edit or Update
            
            await _Service.Update(id,medicine);
            return RedirectToAction(nameof(Index));

        }// End of Edit Get&Post </summary>


        // <summary> Get Medicine to DeletetView Using EditView
        // Post DeleteMedicine
        public async Task<IActionResult> Delete(int id)
        {
            // <QA> lins insure data still in Db not just in UI _ Case3:
            var MedicineDetails = await _Service.GetByID(id);
            if (MedicineDetails == null) return View ("NotFounded"); // </QA>

            await _Service.Delete(id);
            return RedirectToAction(nameof(Index));

        }//  End of Delete Post </summary>

    }
}
