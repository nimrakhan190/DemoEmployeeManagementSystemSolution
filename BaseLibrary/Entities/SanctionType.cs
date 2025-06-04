﻿using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {

        //Many to one relationship with vacation
        [JsonIgnore]
        public List<Sanction>? Sanctions { get; set; }

    }
}
