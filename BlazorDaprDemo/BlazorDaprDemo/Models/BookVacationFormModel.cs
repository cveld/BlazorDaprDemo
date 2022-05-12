using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Models
{
    // DutchPhoneNumberAttribute implemented as workaround because IValidatableObject is not supported out of the box; see https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-3.0
    public class BookVacationFormModel
    {    
        [Required, EmailAddress, Display(Name = "E-mail")]
        public string EmailAddress { get; set; }
        [Required, Display(Name = "Phone prefix")]
        public string PhoneNumberPrefix { get; set; }
        [Required, Display(Name = "Phone number"), DutchPhoneNumber, RegularExpression("[0-9]*", ErrorMessage = "The Phone number field should only contain digits.")]
        public string PhoneNumber { get; set; }
        [Required, DateEqualOrAfterToday, Display(Name = "Booking date")]
        public DateTime? BookingDate { get; set; }                
    }
}
