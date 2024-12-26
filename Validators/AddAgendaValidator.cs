using FluentValidation;
using project_cache.Models;

namespace project_cache.Validators
{
    public class AddAgendaValidator : AbstractValidator<DoctorAgenda>
    {
        public AddAgendaValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name must not be null or empty.")
                .MinimumLength(5)
                .WithMessage("Name length must be more than 5.")
                .MaximumLength(15)
                .WithMessage("Name length must be less than 15.");

            RuleFor(m => m.Specialty)
                .NotEmpty()
                .WithMessage("Specialty must not be null or empty.")
                .MinimumLength(5)
                .WithMessage("Specialty length must be more than 5.")
                .MaximumLength(15)
                .WithMessage("Specialty length must be less than 15.");
        }
    }
}
