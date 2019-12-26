using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace sys.Models
{
    public class ProductImg
    {

        [Key]//設定主鍵
        [Required]//資料表不允許null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//設定自動編號
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "產品編號")]
        public int Pid { get; set; }
        [ForeignKey("Pid")]
        [JsonIgnore]
        public virtual ProductList ProductList { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "檔名")]
        public string Pimg { get; set; }
    }
}