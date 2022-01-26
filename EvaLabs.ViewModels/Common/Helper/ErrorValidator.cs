using System.Collections.Generic;
using System.Text.Json.Serialization;
using EvaLabs.Common.Models;

namespace EvaLabs.ViewModels.Common.Helper
{
    public abstract class ErrorValidator : Auditable
    {
        [JsonIgnore] public bool HasErrors => Validate();

        [JsonIgnore] public List<string> Errors { get; protected set; }

        protected abstract bool Validate();
    }
}