using System;
using Crowdfunding.Domain.Base;
using FluentValidation;

namespace Crowdfunding.Domain.Entities
{
    public class Cause : Entity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        private Cause()
        {
        }

        public Cause(Guid id, string name, string city, string state)
        {
            this.Id = id;
            this.Name = name;
            this.City = city;
            this.State = state;
        }
        public override bool Valid()
        {
            return true;
        }
    }
}