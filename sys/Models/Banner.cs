using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sys.Models
{
    public class Banner
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "Banner名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "Banner圖片")]
        public string Img { get; set; }

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