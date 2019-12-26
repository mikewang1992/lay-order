using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace sys.Models
{
    public class Account
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "電話")]
        [MaxLength(10)]
        public string Tel { get; set; }
        public ICollection<Order> Orders { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [JsonIgnore]//這樣這個欄位就不會被轉出成JSON
        [MaxLength(100)]
        [Display(Name = "密碼鹽")]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "姓名")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "生日")]
        [MaxLength(50)]
        public string Birth { get; set; }

        [Display(Name = "城市")]
        [MaxLength(200)]
        public string County { get; set; }

        [Display(Name = "區域")]
        [MaxLength(200)]
        public string Dist { get; set; }


        
        [Display(Name = "驗證碼")]
        public string Vertify { get; set; }

        
        [Display(Name = "是否驗證")]
        public bool IsCheck { get; set; }

        [Display(Name = "桌子")]
        public bool IsTable { get; set; }

        [Display(Name = "寄送簡訊次數")]
        public int Sent { get; set; }

        [Display(Name = "輸入錯誤次數")]
        public int wrong { get; set; }
    }
}