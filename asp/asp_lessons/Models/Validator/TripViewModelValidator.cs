//using aspapp.Models;
//using aspapp.Repositories;
//using aspapp.Services;
//using FluentValidation;
//using aspapp.Models;
//using aspapp.Repositories;
//using aspapp.Services;

//using aspapp.Models.Validator;
//using aspapp.Models.VM;

//namespace aspapp.Models.Validator
//{
//    public class TripViewModelValidator : AbstractValidator<TripViewModel>
//    {
//        public TripViewModelValidator()
//        {
//            RuleFor(x => x.Title)
//                .NotEmpty().WithMessage("Please specify title");

//            RuleFor(x => x.Description)
//                .NotEmpty().WithMessage("Please specify description");

//            RuleFor(x => x.Guides)
//                .NotNull().WithMessage("Lista przewodników nie może być null.")
//                .Must(list => list.Any())
//                .WithMessage("Musi być przynajmniej jeden przewodnik dostępny.");

//            RuleFor(x => x.Travelers)
//                .NotNull().WithMessage("Lista podróżników nie może być null.")
//                .Must(list => list.Any())
//                .WithMessage("Musi być przynajmniej jeden podróżnik dostępny.");
//        }
//    }
//}
