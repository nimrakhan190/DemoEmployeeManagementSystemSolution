﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class SanctionType
    {
        public int Id { get; set; }

        //Many to one relationship with vacation

        public List<Sanction>? Sanctions { get; set; }


    }
}
