using System.Globalization;
using System.Windows.Controls;

namespace PersianMoviesWPFApp.Validators
{
    public class RequiredValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrWhiteSpace(value?.ToString()))
                if (value.ToString() != "0")
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "یک گزینه را انتخاب کنید!");

            return new ValidationResult(false, "فیلد اجباری!");
        }
    }
}
