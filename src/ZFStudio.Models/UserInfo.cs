using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZFStudio.Models.Interface;

namespace ZFStudio.Models
{
    [Table("UserInfo")]
    public class UserInfo : IBaseCreateTime
    {
        [Key]
        [Column(TypeName="varchar(20)")]
        public string UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string UserName { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

    }
}
