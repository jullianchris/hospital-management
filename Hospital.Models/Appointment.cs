using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public ApplicationUser Doctor { get; set; }
        public ApplicationUser Patient { get; set; }
    }
}
