using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
	public class LabViewModel : ErrorValidator
    {

        protected override bool Validate()
        {
            return new LabValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }

    public class LabValidator : AbstractValidator<LabViewModel>
    {
        public LabValidator()
        {

        }
    }
}
