using System;

namespace Services.DataModels
{
    public class PostVacationModel
    {
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int VacationType { get; set; }
        public int VacationStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
