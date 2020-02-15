using FluentValidation;
using FluentValidation.Results;
using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Business.Notifications;

namespace Store.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notificator.Handle(new Notification(message));
        }

        protected bool Validate<TValidation, TEntity>(TValidation validation, TEntity entity) 
            where TValidation : AbstractValidator<TEntity> where TEntity : Entity
        {
            var validator = validation.Validate(entity);
            if (validator.IsValid)
                return true;
            Notify(validator);
            return false;
        }
    }
}
