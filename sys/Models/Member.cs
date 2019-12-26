using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace sys.Models
{
    public class Member
    {
        [Key]//設定主鍵
        [Required]//資料表不允許null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//設定自動編號
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]//{0}是指 [Display(Name = "帳號")]
        [Display(Name = "帳號")]
        [MaxLength(50)]//會自動轉成nvarchar,這裡是設定長度
        public string Account { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [JsonIgnore]//這樣這個欄位就不會被轉出成JSON
        [MaxLength(100)]
        [Display(Name = "密碼鹽")]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "姓名")]
        [MaxLength(50)]
        public string Name { get; set; }


        [Display(Name = "性別")]
        public EnumList.GenderType Gender { get; set; }//EnumList的型別

        
        [Display(Name = "電話")]
        [MaxLength(200)]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }

        [Display(Name = "照片")]
        [MaxLength(200)]
        public string Img { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0}格式錯誤")]
        [Display(Name = "Email")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(500)]
        [Display(Name = "權限")]
        public string Permission { get; set; }

        
        [Display(Name = "建立時間")]
        [DataType(DataType.Date)]
        public DateTime initDate { get; set; }
    }

}