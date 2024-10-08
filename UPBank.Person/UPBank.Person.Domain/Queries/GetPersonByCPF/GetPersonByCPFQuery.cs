﻿using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Person.Domain.Queries.GetPersonByCPF
{
    public class GetPersonByCPFQuery : IRequest<CreatePersonCommandResponse>
    {
        public string CPF { get; set; } = string.Empty;

        public GetPersonByCPFQuery(string cpf)
        {
            CPF = cpf;
        }
    }
}