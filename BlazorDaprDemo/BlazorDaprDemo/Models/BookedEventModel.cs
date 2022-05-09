using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Models
{
    public class BookedEventModel
    {
        public int VacationID { get; set; }
        public BookVacationFormModel BookVacationFormModel { get; set; }
    }
}
