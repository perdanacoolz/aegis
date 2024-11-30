using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Identity.Models;
using System.Configuration;
using Identity.DAL;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace Identity.Controllers
{
   
    public class LaporanController : Controller
    {
       
        [Authorize(Roles = "Admin,kasir")]
        public IActionResult Index()
        {
            DataAccessLayer model_units = new DataAccessLayer();
            List<Produk> lst = new List<Produk>();
            lst = model_units.GetAll_();
            return View(lst);
        }

        [HttpGet]
        public ActionResult InsertProduk()
        {
            return View();
        }

        

    }
}
