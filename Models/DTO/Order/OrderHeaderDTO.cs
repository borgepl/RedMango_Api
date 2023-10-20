using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO.Login;

namespace Models.DTO.Order
{
    public class OrderHeaderDTO
    {
        [Key]
        public int OrderHeaderId { get; set; }
        [Required]
        public string PickupName { get; set; } = string.Empty;
        [Required]
        public string PickupPhoneNumber { get; set; } = string.Empty;
        [Required]
        public string PickupEmail { get; set; } = string.Empty;

        public string ApplicationUserId { get; set; } = string.Empty;
        [ForeignKey("ApplicationUserId")]
        public UserDTO User { get; set; }
        public double OrderTotal { get; set; }


        public DateTime OrderDate { get; set; }
        public string StripePaymentIntentID { get; set; }
        public string Status { get; set; }
        public int TotalItems { get; set; }

        public IEnumerable<OrderDetailsDTO> OrderDetailsDTO { get; set; }
    }
}
