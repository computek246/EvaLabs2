using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
	public class TestBranchsViewModel : ErrorValidator
    {

        protected override bool Validate()
        {
            return new TestBranchsValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }

    public class TestBranchsValidator : AbstractValidator<TestBranchsViewModel>
    {
        public TestBranchsValidator()
        {

        }
    }
}
