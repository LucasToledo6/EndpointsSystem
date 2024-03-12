using EndpointSystem.Application.Input.Model;
using FluentValidation;

namespace EndpointSystem.Application.Input.Validation
{
    public class CreateEndpointInputValidator : AbstractValidator<CreateEndpointInput>
    {
        public CreateEndpointInputValidator()
        {
            RuleFor(x => x.EndpointSerialNumber).NotEmpty().MaximumLength(64).Matches("^[a-zA-Z0-9]*$");
            RuleFor(x => x.MeterModelId).NotEmpty().Must(x => Enum.IsDefined(x));
            RuleFor(x => x.MeterNumber).NotEmpty();
            RuleFor(x => x.MeterFirmwareVersion).NotEmpty().MaximumLength(32);
            RuleFor(x => x.SwitchState).NotEmpty().Must(x => Enum.IsDefined(x));
        }
    }
}
