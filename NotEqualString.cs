using System.ComponentModel.DataAnnotations;

public class NotEqualStringCustomAttribute : ValidationAttribute
{
    private readonly string _stringToCompare;

    public NotEqualStringCustomAttribute(string stringToCompare)
    {
        _stringToCompare = stringToCompare;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null && value.ToString()!.Equals(_stringToCompare, StringComparison.OrdinalIgnoreCase))
        {
            return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must not be equal to {_stringToCompare}.");
        }

        return ValidationResult.Success;
    }
}