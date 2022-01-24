using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
	public class UserTestViewModel : ErrorValidator
    {

        protected override bool Validate()
        {
            return new UserTestValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }

    public class UserTestValidator : AbstractValidator<UserTestViewModel>
    {
        public UserTestValidator()
        {

        }
    }
}
