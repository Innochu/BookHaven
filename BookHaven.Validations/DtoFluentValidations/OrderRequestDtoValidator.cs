using BookHaven.Application.Dto.RequestDto;
using FluentValidation;

namespace BookHaven.Validations.DtoFluentValidations
{
    public class OrderRequestDtoValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderRequestDtoValidator(OrderItemDtoValidator orderItemValidator)
        {
            RuleFor(dto => dto.OrderId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(dto => dto.OrderItems)
                .NotEmpty().WithMessage("At least one order item is required.")
                .ForEach(orderItem => orderItem.SetValidator((FluentValidation.Validators.IPropertyValidator<IEnumerable<Domain.Entities.OrderItem>, Domain.Entities.OrderItem>)orderItemValidator));
        }
    }
}
    