﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Utils.CrossCutting.Exception.Pipes
{
    public class FailFastValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IDomainNotificationService _domainNotificationService;

        public FailFastValidation(IEnumerable<IValidator<TRequest>> validators, IDomainNotificationService domainNotificationService)
        {
            _validators = validators;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var results = await GetResultsAsync(context, cancellationToken);
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            return failures.Any() ? await NotifyAsync(failures) : await next();
        }

        private async Task<ValidationResult[]> GetResultsAsync(ValidationContext<TRequest> context, CancellationToken cancellationToken)
        {
            return await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        }

        private async Task<TResponse> NotifyAsync(IEnumerable<ValidationFailure> failures)
        {
            _domainNotificationService.AddRange(failures.Select(x => $"{x.ErrorMessage}"));

            return Task.FromResult(default(TResponse)).Result;
        }
    }
}