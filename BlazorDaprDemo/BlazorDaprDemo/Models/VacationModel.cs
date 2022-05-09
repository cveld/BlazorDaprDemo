using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Models
{
    [Table("Vacation")]
    public class VacationModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string OriginalPrice { get; set; }
        public string Price { get; set; }
        public string Rating { get; set; }
        public int Stars { get; set; }
        virtual public ICollection<User> Likes { get; set; }
        public int Persons { get; set; }
        public int SleepingRooms { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string PriceUnit { get; set; }
        public bool Booked { get; set; }
        public VacationModel()
        {
            //Likes = new HashSet<User>();
        }        
    }
}
