using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Models
{
    public class Produk
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public string nama_produk { set; get; }
        [Required]
        public string jenis_produk { set; get; }
        [Required]
        public string qty { set; get; }
        [Required]
        public string Designation { set; get; }
        [Required]
        public string StaffNo { set; get; }

         
    }
}
