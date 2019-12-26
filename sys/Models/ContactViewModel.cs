using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sys.Models
{
    public class ContactViewModel
    {
        [Key]//設定主鍵
        [Required]//資料表不允許null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//設定自動編號
        public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")]//{0}是指 [Display(Name = "帳號")]
        [Display(Name = "名稱")]
        [MaxLength(200)]//會自動轉成nvarchar,這裡是設定長度
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0}格式錯誤")]
        [Display(Name = "Email")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "電話")]
        [MaxLength(200)]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }

        [Display(Name = "內容")]
        public string memo { get; set; }
    }
}