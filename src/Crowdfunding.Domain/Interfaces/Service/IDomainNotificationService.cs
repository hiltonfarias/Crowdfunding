using FluentValidation.Results;

using System.Collections.Generic;
using System.Linq;

using Crowdfunding.Domain.Base;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Domain
{
    public interface IDomainNotificationService
    {
         bool HasErrors { get; }
         IEnumerable<DomainNotification> RecoverDomainErrors();
         string RecoverDomainErrorsFormattedHTML();

        void Add<T>(T entity) where T : Entity;
        void Add(ValidationResult validationResult);
        void Add(DomainNotification domainNotification);
    }

    public class DomainNotificationService : IDomainNotificationService
    {
        private readonly List<DomainNotification> _notifications;
        public bool HasErrors => _notifications.Any();
        public DomainNotificationService()
        {
            _notifications = new List<DomainNotification>();
        }
        public IEnumerable<DomainNotification> RecoverDomainErrors()
        {
            return _notifications;
        }
        public void Add(DomainNotification domainNotification)
        {
            _notifications.Add(domainNotification);
        }
        public void Add<T>(T entity) where T : Entity
        {
            _notifications.AddRange(
                entity.ValidationResult.Errors.Select(
                    returned => new DomainNotification(returned.ErrorCode, returned.ErrorMessage)
                )
            );
        }
        public void Add(ValidationResult validationResult)
        {
            if (validationResult == null) return;

            _notifications.AddRange(validationResult.Errors.Select(
                returned => new DomainNotification(returned.ErrorCode, returned.ErrorMessage)
            ));
        }
        private static bool ThereIsAnErrorForTheProperty(string field)
        {
            return !string.IsNullOrEmpty(field);
        }

        private void AddErrorsDomain(DomainNotification domainNotification)
        {
            if (FilledField(domainNotification) && !ThereAreErrors(domainNotification.ErrorMessage))
            {
                _notifications.Add(domainNotification);
            }
        }
        private static bool FilledField(DomainNotification domainNotification)
        {
            return !string.IsNullOrEmpty(domainNotification.ErrorMessage);
        }
        private bool ThereAreErrors(string field)
        {
            return _notifications.Any(a => a.ErrorMessage == field);
        }
        public string RecoverDomainErrorsFormattedHTML()
        {
            var errors = string.Join("", RecoverDomainErrors()
                .Select(a => $"<li>{a.ErrorMessage}</li>").ToArray()
            );
            return $"<ul>{errors}</ul>";
        }
    }
}