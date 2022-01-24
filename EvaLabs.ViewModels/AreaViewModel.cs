using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
	public class AreaViewModel : ErrorValidator
    {

        protected override bool Validate()
        {
            return new AreaValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }

    public class AreaValidator : AbstractValidator<AreaViewModel>
    {
        public AreaValidator()
        {

        }
    }
}
