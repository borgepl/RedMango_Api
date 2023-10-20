using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class SD
    {
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";
        public const string Role_Employee = "Employee";

        //public const string Local_Token = "JWT Token";
        //public const string Local_UserDetails = "User Details";

        public const string Status_Pending = "Pending";
        public const string Status_Confirmed = "Confirmed";
        public const string Status_BeingCooked = "Being Cooked";
        public const string Status_ReadyForPickup = "Ready for Pickup";
        public const string Status_Completed = "Completed";
        public const string Status_Cancelled = "Cancelled";
    }
}
