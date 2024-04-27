using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }     
        public int UnitId { get; set; }
        
    }
}
