using FluentValidation;
using FluentValidation.Results;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Services;

namespace MovieStore.WebApi.Validations
{
    public class SActorValidator : AbstractValidator<IActor>
    {
        public SActorValidator()
        {
            RuleFor(actor => actor.ActorId).GreaterThan(0).When(actor => actor.ActorCreateModel == null && actor.ActorUpdateModel == null);

            RuleFor(actor => actor.ActorCreateModel).NotNull().When(actor => actor.ActorId == 0 && actor.ActorUpdateModel == null);
            RuleFor(actor => actor.ActorCreateModel.Name)
               .NotEmpty().MinimumLength(3).MaximumLength(25)
               .When(actor => actor.ActorCreateModel!= null && actor.ActorId == 0 && actor.ActorUpdateModel == null);
            RuleFor(actor => actor.ActorCreateModel.Surname)
               .NotEmpty().MinimumLength(3).MaximumLength(25)
               .When(actor => actor.ActorCreateModel != null && actor.ActorId == 0 && actor.ActorUpdateModel == null);

            RuleFor(actor => actor.ActorUpdateModel).NotNull()
                .When(actor => actor.ActorId == 0 && actor.ActorCreateModel == null);
            RuleFor(actor => actor.ActorUpdateModel.Id).GreaterThan(0)
               .When(actor => actor.ActorUpdateModel != null && actor.ActorId == 0 && actor.ActorCreateModel == null);
            RuleFor(actor => actor.ActorUpdateModel.Name)
               .NotEmpty().MinimumLength(3).MaximumLength(25)
               .When(actor => actor.ActorUpdateModel != null && actor.ActorId == 0 && actor.ActorCreateModel == null);
            RuleFor(actor => actor.ActorUpdateModel.Surname)
               .NotEmpty().MinimumLength(3).MaximumLength(25)
               .When(actor => actor.ActorUpdateModel != null &&  actor.ActorId == 0 && actor.ActorCreateModel == null);
        }
    }
}
