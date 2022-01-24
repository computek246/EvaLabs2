using System.Linq;
using EvaLabs.ViewModels.Common.Helper;
using FluentValidation;

namespace EvaLabs.ViewModels
{
    public class BranchViewModel : ErrorValidator
    {
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string AreaName { get; set; }
        public string CityName { get; set; }
        protected override bool Validate()
        {
            return new BranchValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }

    public class BranchValidator : AbstractValidator<BranchViewModel>
    {
        public BranchValidator()
        {

        }
    }
}
