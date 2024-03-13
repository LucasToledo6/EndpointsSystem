using EndpointSystem.Application.Input.Model;
using FluentValidation;

namespace EndpointSystem.Application.Input.Validation
{
    public class EditEndpointInputValidator : AbstractValidator<EditEndpointInput>
    {
        public EditEndpointInputValidator()
        {
            RuleFor(x => x.EndpointSerialNumber).NotEmpty().MaximumLength(64).Matches("^[a-zA-Z0-9]*$");
            RuleFor(x => x.SwitchState).Must(x => Enum.IsDefined(x));
        }
    }
}
