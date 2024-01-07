using BookHaven.Application.Dto.RequestDto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BookHaven.Validations.DtoFluentValidations
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(dto => dto.BookId)
                .GreaterThan(0).WithMessage("BookId must be greater than zero.");

            RuleFor(dto => dto.BookTitle)
                .NotEmpty().WithMessage("BookTitle is required.")
                .MaximumLength(100).WithMessage("BookTitle cannot exceed 100 characters.");

            RuleFor(dto => dto.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(dto => dto.UnitPrice)
                .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.")
                .Must(price => IsValidDecimal(price)).WithMessage("UnitPrice should be a valid decimal number with up to two decimal places.");
        }

        private bool IsValidDecimal(decimal value)
        {
            // Convert decimal to string for regex validation
            string decimalString = value.ToString();

            // Regular expression to match up to two decimal places
            string decimalPattern = @"^\d+(\.\d{1,2})?$";

            // Validate using Regex.IsMatch
            return Regex.IsMatch(decimalString, decimalPattern);
        }

    }
}
