using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Models
{
    public class FavoriteClickedEventModel
    {
        public int VacationId { get; set; }
        public bool Liked { get; set; }
        public string User { get; set; }
    }
}
