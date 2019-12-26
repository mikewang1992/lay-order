using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sys.Models
{
    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "訂購者")]
        public int Cid { get; set; }
        [ForeignKey("Cid")]
        public virtual Account Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "訂餐時間")]
        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "取餐時間")]
        [DataType(DataType.DateTime)]
        public DateTime GetTime { get; set; }

        
        [Display(Name = "狀態")]
        public string Status { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}