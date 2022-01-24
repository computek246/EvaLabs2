using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
    public class CityViewModel : ErrorValidator
    {
        public string CityName { get; set; }

        protected override bool Validate()
        {
            return new CityValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }

    public class CityValidator : AbstractValidator<CityViewModel>
    {
        public CityValidator()
        {

        }
    }
}