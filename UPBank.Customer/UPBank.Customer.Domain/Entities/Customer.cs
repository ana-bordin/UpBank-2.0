﻿namespace UPBank.Customer.Domain.Entities
{
    public class Customer : Person.Domain.Entities.Person
    {
        public bool Restriction { get; set; }
        public bool Active { get; set; }
    }
}