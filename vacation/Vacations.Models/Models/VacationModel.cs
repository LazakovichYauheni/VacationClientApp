using System;
using System.ComponentModel;

namespace Vacations.Models.Models
{
    public class VacationModel : INotifyPropertyChanged
    {
        private int _currentImageOnPage;
        private int _vacationType;
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Period => $"{StartDate.ToString("MMM dd")}-{EndDate.ToString("MMM dd")}";
        public int VacationStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public string DateDayStart => $"{StartDate.ToString("dd")} ";
        public string DateMonthStart => $"{StartDate.ToString("MMM")}";
        public string DateYearStart => $"{StartDate.ToString("yyyy")}";
        public string DateDayEnd => $"{EndDate.ToString("dd")} ";
        public string DateMonthEnd => $"{EndDate.ToString("MMM")}";
        public string DateYearEnd => $"{EndDate.ToString("yyyy")}";
        public string VacationStatusTitle => Enum.IsDefined(typeof(VacationStatuses), VacationStatus) ? Enum.GetName(typeof(VacationStatuses), VacationStatus) : string.Empty;
        public string VacationTypeTitle => Enum.IsDefined(typeof(VacationTypes), VacationType) ? Enum.GetName(typeof(VacationTypes), VacationType) : string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public int CurrentImageOnPage
        {
            get => _currentImageOnPage;
            set
            {
                _currentImageOnPage = value;
                OnPropertyChanged(nameof(CurrentImageOnPage));
                _vacationType = value + 1;
                OnPropertyChanged(nameof(VacationType));
            }
        }

        public int VacationType
        {
            get => _vacationType;
            set
            {
                _vacationType = value;
                OnPropertyChanged(nameof(VacationType));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
