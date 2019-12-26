using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace sys.Models
{
    public class OrderDetail
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "訂單編號")]
        public int Oid { get; set; }
        [JsonIgnore]
        [ForeignKey("Oid")]
        public virtual  Order order { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "產品ID")]
        public int Pid { get; set; }
        [JsonIgnore]
        [ForeignKey("Pid")]
        public virtual ProductList ProductList { get; set; }

        [Display(Name = "附餐選項")]
        public string Options { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "訂購數量")]
        public int Qty { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "單品金額")]
        public int Price { get; set; }

        [Display(Name = "單品狀態")]
        public string Status { get; set; }
    }

    //訂單傳送型別
    public class OrderContent
    {
        public int Pid { get; set; }
        public string Options { get; set; }
        public int Qty { get; set; }
        public string orderTime { get; set; }   
        public string getTime { get; set; }
    }
}