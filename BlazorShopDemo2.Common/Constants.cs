using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShopDemo2.Common
{
    public static class Constants
    {
        public const string ShoppingCart = "Pending";
        public const string StatusConfirmed = "Confirmed";
        public const string StatusShipped = "Shipped";
        public const string StatusRefunded = "Refunded";
        public const string StatusCanceled = "Canceled";

        public const string RoleAdmin = "Admin";
        public const string RoleCustomer = "Customer";
    }
}