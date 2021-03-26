using System.Collections.Generic;
using System.ComponentModel;

namespace Crowdfunding.Domain.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Donors = new List<DonorViewModel>();
            Institutions = new List<CauseViewModel>();
        }

        [DisplayName("How much to collect?")]
        public double RemainingTargetValue { get; set; }
        [DisplayName("How much do we collect?")]
        public double TotalAmountCollected { get; set; }
        [DisplayName("Percentage collected")]
        public double TotalPercentageCollected { get; set; }
        [DisplayName("Amount of Donors")]
        public double AmountOfDonors { get; set; }
        [DisplayName("Days remaining")]
        public int TimeRemainingDays { get; set; }
        [DisplayName("Time remaining hours")]
        public int TimeRemainingHours { get; set; }
        [DisplayName("Time remaining minutes")]
        public int TimeRemainingMinutes { get; set; }
        public IEnumerable<DonorViewModel> Donors { get; set; }
        public IEnumerable<CauseViewModel> Institutions { get; set; }
    }
}