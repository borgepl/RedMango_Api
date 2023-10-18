using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        [NotMapped]
        public double CartTotal { get; set; }

        [NotMapped]
        public string StripePaymentIntentId { get; set; }
        
        [NotMapped]
        public string ClientSecret { get; set; }
    }
}
