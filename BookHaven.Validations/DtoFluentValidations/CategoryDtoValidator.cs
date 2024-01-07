using BookHaven.Application.Dto.RequestDto;
using FluentValidation;

namespace BookHaven.Validations.DtoFluentValidations
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(dto => dto.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
