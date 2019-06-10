using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class AuthorizationPayment : BaseModel
    {
        public int AuthorizationID { get; set; }
        public int PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        public Payment Payment { get; set; }
        [ForeignKey("AuthorizationID")]
        public Authorization Authorization { get; set; }
    }
}
