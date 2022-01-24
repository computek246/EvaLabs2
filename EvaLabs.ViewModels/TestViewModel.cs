using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
    public class TestViewModel : ErrorValidator
    {
        public string TestName { get; set; }
        public string TestDetails { get; set; }
        public decimal Price { get; set; }
        public bool AtHome { get; set; }

        protected override bool Validate()
        {
            return new TestValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }

    public class TestValidator : AbstractValidator<TestViewModel>
    {
        public TestValidator()
        {

        }
    }
}
