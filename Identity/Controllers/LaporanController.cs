using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Identity.Models;
using System.Data.SqlClient;
using System.Configuration;
using Identity.DAL;
namespace Identity.Controllers
{
    public class LaporanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InsertProduk()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertProduk(Produk objCustomer)
        {
           // objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);
            if (ModelState.IsValid) // checking model is valid or not
            {
                DataAccessLayer objDB = new DataAccessLayer();
                string result = objDB.InsertData(objCustomer);
                TempData["result1"] = result;
                ModelState.Clear(); // clearing model
                return RedirectToAction("ShowAllProdukDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult ShowAllProdukDetails()
        {
            Produk objCustomer = new Produk();
            DataAccessLayer objDB = new DataAccessLayer(); // calling class DBdata
            objCustomer.ShowallProduk = objDB.Selectalldata();
            return View(objCustomer);
        }

        [HttpGet]
        public ActionResult Details(string ID)
        {
            Produk objCustomer = new Produk();
            DataAccessLayer objDB = new DataAccessLayer(); // calling class DBdata
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            Produk objCustomer = new Produk();
            DataAccessLayer objDB = new DataAccessLayer(); // calling class DBdata
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpPost]
        public ActionResult Edit(Produk objCustomer)
        {
          //  objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);
            if (ModelState.IsValid) // checking model is valid or not
            {
                DataAccessLayer objDB = new DataAccessLayer(); // calling class DBdata
                string result = objDB.UpdateData(objCustomer);
                TempData["result2"] = result;
                ModelState.Clear(); // clearing model
                return RedirectToAction("ShowAllProdukDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(String ID)
        {
            DataAccessLayer objDB = new DataAccessLayer();
            int result = objDB.DeleteData(ID);
            TempData["result3"] = result;
            ModelState.Clear(); // clearing model
            return RedirectToAction("ShowAllProdukDetails");
        }

    }
}
