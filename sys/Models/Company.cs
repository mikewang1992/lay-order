using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sys.Models
{
    public class Company
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "電話")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "結束時間")]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "大單數量設定")]
        public int BigQty { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "備餐時間")]
        public int PrepareTime { get; set; }

    }
}