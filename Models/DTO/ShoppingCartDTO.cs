using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItemDTO> CartItems { get; set; }
        public double CartTotal { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
