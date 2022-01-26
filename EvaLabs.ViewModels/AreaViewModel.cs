using System.ComponentModel.DataAnnotations;
using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
	public class AreaViewModel : ErrorValidator
    {
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }

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
