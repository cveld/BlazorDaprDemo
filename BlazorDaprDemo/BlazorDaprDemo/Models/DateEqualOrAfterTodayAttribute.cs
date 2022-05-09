using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Models
{
    // took from https://stackoverflow.com/questions/46184818/dataanotation-to-validate-a-model-how-do-i-validate-it-so-that-the-date-is-not
    public class DateEqualOrAfterTodayAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field should be a future date.";
        }

        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();   
            
            // Due to a 'feature' of the Date Picker; DateTime objects are submitted with local time offset whereas the Kind attribute is set to UTC
            // E.g. in the Netherlands when a user submits 30 October 2019; it will be send to this method as 2019-10-29 22:00
            // We need to compensate:
            if (dateValue.ToUniversalTime().TimeOfDay.Hours >= 12)
            {
                dateValue = dateValue.AddDays(1);
            }

            if (dateValue.ToUniversalTime().Date < DateTime.UtcNow.Date)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new string[] { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}
