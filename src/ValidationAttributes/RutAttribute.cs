using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TallerIDWM.src.ValidationAttributes
{
    public partial class RutAttribute : ValidationAttribute
    {
         [GeneratedRegex(@"^\d{1,2}\.\d{3}\.\d{3}-[\dkK]$")]
        private static partial Regex RutRegex();

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            var rut = value as string;

            if (string.IsNullOrEmpty(rut))
                return new ValidationResult("El RUT es obligatorio.");

            var regex = RutRegex();

            if (!regex.IsMatch(rut))
                return new ValidationResult("El formato del RUT no es válido.");

            var rutWithoutFormat = rut.Replace(".", "").Replace("-", "");
            var rutDigits = rutWithoutFormat.Substring(0, rutWithoutFormat.Length - 1);
            var dv = rutWithoutFormat[^1].ToString().ToUpper();

            if (!IsValidRutDv(rutDigits, dv))
                return new ValidationResult("El dígito verificador del RUT no es válido.");

            return ValidationResult.Success;
        }

        private static bool IsValidRutDv(string rutDigits, string dv)
        {
            int sum = 0;
            int multiplier = 2;

            for (int i = rutDigits.Length - 1; i >= 0; i--)
            {
                sum += int.Parse(rutDigits[i].ToString()) * multiplier;
                multiplier = multiplier == 7 ? 2 : multiplier + 1;
            }

            int remainder = 11 - (sum % 11);

            string calculatedDv = remainder switch
            {
                11 => "0",
                10 => "K",
                _ => remainder.ToString()
            };

            return dv == calculatedDv;
        }
    }
}