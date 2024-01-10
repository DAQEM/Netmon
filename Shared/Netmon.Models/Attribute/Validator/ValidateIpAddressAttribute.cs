using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Netmon.Models.Attribute.Validator;

public class ValidateIpAddressAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string ipAddressString)
        {
            if (IPAddress.TryParse(ipAddressString, out IPAddress? ipAddress))
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ValidationResult.Success;
                }
            }
        }

        return new ValidationResult("Invalid IP Address format.");
    }
}