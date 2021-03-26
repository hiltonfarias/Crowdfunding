using FluentValidation.Results;

using System;
using System.Linq;

namespace Crowdfunding.Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }
        public string[] ErrorsMessages => ValidationResult?.Errors?.Select(errorMessage => errorMessage.ErrorMessage)?.ToArray();
        
        public abstract bool Valid();
    }
}