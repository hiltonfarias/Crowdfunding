namespace Crowdfunding.Domain.ViewModels
{
    public class DonationViewModel
    {
        public decimal Value { get; set; }
        public PersonViewModel PersonalData { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public CreditCardViewModel FormPayment { get; set; }
    }
}