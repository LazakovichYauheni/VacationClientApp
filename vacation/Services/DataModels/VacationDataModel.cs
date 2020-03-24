using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Services.DataModels
{
    public class VacationDataModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("vacationType")]
        public int VacationType { get; set; }
        [JsonProperty("vacationStatus")]
        public int VacationStatus { get; set; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }

    public class VacationModelRoot
    {
        [JsonProperty("result")]
        public List<VacationDataModel> Result { get; set; }
    }
}
