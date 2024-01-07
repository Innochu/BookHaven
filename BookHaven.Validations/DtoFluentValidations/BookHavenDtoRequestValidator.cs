using BookHaven.Application.Dto.RequestDto;
using FluentValidation;

namespace BookHaven.Validations.DtoFluentValidations
{
    public class BookHavenDtoRequestValidator : AbstractValidator<BookHavenRequestDto>
    {
        public BookHavenDtoRequestValidator()
        {
          RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(dto => dto.ShortDescription)
                .NotEmpty().WithMessage("ShortDescription is required.")
                .MaximumLength(500).WithMessage("ShortDescription cannot exceed 500 characters.");

            RuleFor(dto => dto.Author)
                .NotEmpty().WithMessage("Author is required.")
                .MaximumLength(50).WithMessage("Author cannot exceed 50 characters.")
                .Matches("^[a-zA-Z\\s]*$").WithMessage("Author should only contain letters and spaces.");

            RuleFor(dto => dto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .Must(price => IsValidDecimal(price)).WithMessage("Price should be a valid decimal number with up to two decimal places.");

            RuleFor(dto => dto.Categories)
                .NotEmpty().WithMessage("At least one category is required.")
                .Must(categories => categories.All(category => !string.IsNullOrEmpty(category.Name)))
                .WithMessage("Category name in each category is required.");

        }

        private bool IsValidDecimal(decimal? value)
        {
            if (value == null)
                return false;

            var stringValue = value.ToString();
            return decimal.TryParse(stringValue, out _) && stringValue.Count(c => c == '.') <= 1;
        }

    }
}
