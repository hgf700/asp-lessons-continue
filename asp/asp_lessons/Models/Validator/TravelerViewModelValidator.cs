//using aspapp.Data.Models.VM;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace aspapp.Data.Models.Validator
//{
//    public class TravelerViewModelValidator : AbstractValidator<TravelerViewModel>
//    {
//        public TravelerViewModelValidator() {
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
