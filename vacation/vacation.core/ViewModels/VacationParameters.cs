using FlexiMvvm.ViewModels;

namespace vacation.core.ViewModels
{
    public class VacationParameters : Parameters
    {
        public string Model
        {
            get => Bundle.GetString();
            set => Bundle.SetString(value);
        }

        public VacationParameters(string vacationModel)
        {
            Model = vacationModel;
        }
    }
}
