using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Identity.Models;
using System;
using ClosedXML.Excel;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
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

    
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = BaseColor.WHITE;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }
        public FileResult Export()
        {

            string html = "StrTag laporan produk EndTag";
            string ExportData = "This is first pdf generat";
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader reader = new StringReader(ExportData);
                Document PdfFile = new iTextSharp.text.Document(PageSize.A3);
                PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
                PdfFile.Open();
                PdfPCell cell = null;
                PdfPTable table = null;
                Phrase phrase2 = null;
                Phrase phrase1 = null;
                phrase2 = new Phrase();
                var titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);


                table = new PdfPTable(1);
                table.TotalWidth = 800f;
                table.LockedWidth = true;
                table.SpacingBefore = 10;
                table.SpacingAfter = 10;
                table.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfFile.Add(table);
                phrase1 = new Phrase();
                html = html.Replace("StrTag", "<").Replace("EndTag", ">");
                phrase1.Add(new Chunk(html, FontFactory.GetFont("Arial", 14, Font.BOLD)));
                cell = PhraseCell(phrase1, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);
                PdfFile.Add(table);

                PdfFile.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
                PdfFile.Close(); return File(stream.ToArray(), "application/pdf", "laporan produk.pdf");
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
