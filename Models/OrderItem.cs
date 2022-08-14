using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        //
        public int MedicineID { get; set; }
        [ForeignKey("MedicineID")]
        public Medicine Medicine { get; set; }

        //
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
