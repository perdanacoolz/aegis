using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Identity.Models;
using System;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using System.Text;
using Microsoft.AspNetCore.Authorization;
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
        
        [Authorize(Roles = "Admin,kasir")]
        public IActionResult List()
        {
            var staff = _context.Produks.ToList();
            return View(staff);
        }

    
 
        [HttpPost]
        public FileResult Export()
        { 

            List<object> customers = (from customer in this._context.Produks.Take(10)
                                      select new[] {
                                      
                                      customer.nama_produk,
                                      customer.jenis_produk,
                                      customer.Designation,
                                      customer.StaffNo
                                 }).ToList<object>();

            //Building an HTML string.
            StringBuilder sb = new StringBuilder();

            //Table start.
            sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial;'>");

            //Building the Header row.
            sb.Append("<tr>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>CustomerID</th>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>ContactName</th>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>City</th>");
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Country</th>");
            sb.Append("</tr>");

            //Building the Data rows.
            for (int i = 0; i < customers.Count; i++)
            {
                string[] customer = (string[])customers[i];
                sb.Append("<tr>");
                for (int j = 0; j < customer.Length; j++)
                {
                    //Append data.
                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(customer[j]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }

            //Table end.
            sb.Append("</table>");

            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(sb.ToString())))
            {
                ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                PdfWriter writer = new PdfWriter(byteArrayOutputStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                pdfDocument.SetDefaultPageSize(PageSize.A4);
                HtmlConverter.ConvertToPdf(stream, pdfDocument);
                pdfDocument.Close();
                return File(byteArrayOutputStream.ToArray(), "application/pdf", "Laporan produk.pdf");
            }
        }

        

        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("nama_produk"),
                new DataColumn("jenis_produk"),
                new DataColumn("qty"),
                new DataColumn("Designation"),
                new DataColumn("StaffNo")
               
            });

            var insuranceCertificate = from InsuranceCertificate in _context.Produks select InsuranceCertificate;

            foreach (var insurance in insuranceCertificate)
            {
                dt.Rows.Add(insurance.nama_produk, insurance.jenis_produk, insurance.qty, insurance.Designation,
                    insurance.StaffNo);
            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
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
