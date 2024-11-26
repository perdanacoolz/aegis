using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Identity.Models;
using System;

namespace Identity.Controllers
{
    public class ProdukController : Controller
    {
        AppIdentityDbContext _context;
        public ProdukController(AppIdentityDbContext context)
        {
            _context = context;
        }
        //get all staff in the database
        public IActionResult List()
        {
            var staff = _context.Produks.ToList();
            return View(staff);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int Id)
        {
            var staff = _context.Produks.FirstOrDefault(e => e.Id == Id);
            return View(staff);
        }

        public IActionResult Delete(int Id)
        {
            //get the staff with the Id
            var staff = _context.Produks.FirstOrDefault(e => e.Id == Id);
            //remove the staff from the database
            _context.Produks.Remove(staff);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Post(Produk staff)
        {
            //add staff to the context
            _context.Produks.Add(staff);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Update(Produk staff)
        {
            //get the existing staff
            var old_staff = _context.Produks.FirstOrDefault(e => e.Id == staff.Id);
            //update with new staff information
            _context.Entry(old_staff).CurrentValues.SetValues(staff);
            _context.SaveChanges();

            return RedirectToAction("List");
        }
    }

}
