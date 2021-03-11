﻿using System;
using System.Collections.Generic;

namespace LocationMap.DomainClasses
{
    public partial class LocationType
    {
        public LocationType()
        {
            Location = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Location> Location { get; set; }
    }
}
