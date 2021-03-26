using AutoMapper;

using System;

using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Service.AutoMapper
{
    public class CrowdfundingOnLineMappingProfile : Profile
    {
        public CrowdfundingOnLineMappingProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<Donation, DonationViewModel>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<Cause, CauseViewModel>();
            CreateMap<CreditCard, CreditCardViewModel>();

            CreateMap<Donation, DonorViewModel>()
                .ForMember(
                    destinationMember => destinationMember.Name, 
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.PersonalData.Name)
                )
                .ForMember(
                    destinationMember => destinationMember.Anonymous, 
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.PersonalData.Anonymous)
                )
                .ForMember(
                    destinationMember => destinationMember.SupportMessage,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.PersonalData.SupportMessage)
                )
                .ForMember(
                    destinationMember => destinationMember.Value,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Value)
                )
                .ForMember(
                    destinationMember => destinationMember.DateTime,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.DateTime)
                );
            
            CreateMap<PersonViewModel, Person>()
                .ConstructUsing(source => new Person(
                    Guid.NewGuid(), 
                    source.Name, 
                    source.Email, 
                    source.Anonymous, 
                    source.SupportMessage
                ));
            CreateMap<CreditCardViewModel, CreditCard>()
                .ConstructUsing(source => new CreditCard(
                    source.CardholderName,
                    source.CreditCardNumber,
                    source.Validity,
                    source.CVV
                ));
            
            CreateMap<CauseViewModel, Cause>()
                .ConstructUsing(source => new Cause(
                    Guid.NewGuid(),
                    source.Name,
                    source.City,
                    source.State
                ));
            
            CreateMap<AddressViewModel, Address>()
                .ConstructUsing(source => new Address(
                    Guid.NewGuid(),
                    source.CEP,
                    source.TextAddress,
                    source.Complement,
                    source.Number,
                    source.City,
                    source.State,
                    source.Phone                    
                ));
            
            CreateMap<DonationViewModel, Donation>()
                .ForCtorParam("value", opt => opt.MapFrom(source => source.Value))
                .ForCtorParam("personalData", opt => opt.MapFrom(source => source.PersonalData))
                .ForCtorParam("formPayment", opt => opt.MapFrom(source => source.FormPayment))
                .ForCtorParam("billingAddress", opt => opt.MapFrom(source => source.BillingAddress));
        }
    }
}