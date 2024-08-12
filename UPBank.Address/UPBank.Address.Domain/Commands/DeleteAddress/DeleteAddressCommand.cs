﻿using MediatR;
using UPBank.Address.Domain.Contracts;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Address.Domain.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest<IDomainNotificationService>
    {
        public Guid Id { get; set; }
    }
}