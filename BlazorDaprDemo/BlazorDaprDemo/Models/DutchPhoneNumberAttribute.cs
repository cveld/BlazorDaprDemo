using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerSide.Models
{
    public class DutchPhoneNumberAttribute : ValidationAttribute
    {        
        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var bookVacationFormModel = validationContext.ObjectInstance as BookVacationFormModel;
            var phoneNumber = objValue as string;
            
            if (bookVacationFormModel?.PhoneNumberPrefix == PhoneNumberPrefixModel.PREFIXNETHERLANDS && phoneNumber?.Length != 9)
            {
                return new ValidationResult("A phone number in the Netherlands should have exactly 9 digits.", new string[] { validationContext.MemberName });
            }
            
            return ValidationResult.Success;
        }        
    }
}