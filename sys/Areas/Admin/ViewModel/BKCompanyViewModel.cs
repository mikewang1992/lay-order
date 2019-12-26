using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using sys.Models;

namespace sys.Areas.Admin.ViewModel
{
    public class BKCompanyViewModel
    {
        [Display(Name = "id")]
        public string CompanySetID { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "電話")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "營業開始時間")]
        [DataType(DataType.DateTime)]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "營業結束時間")]
        [DataType(DataType.DateTime)]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "大單數量設定")]
        public int BigQty { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "備餐時間")]
        public int PrepareTime { get; set; }

        
        //[Display(Name = "id")]
        //public int id { get; set; }
        //[Required(ErrorMessage = "{0}必填")]
        //[Display(Name = "假期開始時間")]
        //[DataType(DataType.DateTime)]
        //public DateTime HolidayStartTime { get; set; }

        //[Required(ErrorMessage = "{0}必填")]
        //[Display(Name = "假期結束時間")]
        //[DataType(DataType.DateTime)]
        //public DateTime HolidayEndTime { get; set; }

        //public IEnumerable<holiday> Holidays { get; set; }
    }
}