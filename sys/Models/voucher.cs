using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sys.Models
{
    public class Voucher
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "優惠標題")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "優惠內容")]
        public string Content { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "開始時間")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "結束時間")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
    }
}