using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sys.Models
{
    public class Permission
    {
        [Key]
        public int id { get; set; }
       
        public int? Pid { get; set; }

        //設定自己關聯自己
        [ForeignKey("Pid")]
        public virtual Permission Parent { get; set; }
        public virtual ICollection<Permission> Childs { get; set; }

        public string Value { get; set; }
        public string PermissionName { get; set; }

        public string Action { get; set; }
        public string Controller { get; set; }
        public string icon { get; set; }
    }
}