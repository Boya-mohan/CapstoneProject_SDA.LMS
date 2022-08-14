using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        public string Email { get; set; }
        public string UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public ApplicationUser User { get; set; }


        //  ArrayList items = new ArrayList(); // Test_Algorithem from me
        public List<OrderItem> OrderItems { get; set; }
    }
}
