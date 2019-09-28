using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Architecture
{
    public class MerchantUser : BaseModel
    {
        public int MerchantID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        [ForeignKey("MerchantID")]
        public Merchant Merchant { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }
}
