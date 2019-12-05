﻿using System;
using System.Collections.Generic;

namespace LocationMap.DomainClasses
{
    public partial class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int? LocationTypeId { get; set; }
        public string Logo { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Label { get; set; }
        public bool? Draggable { get; set; }
        public string FileName { get; set; }

        public virtual LocationType LocationType { get; set; }
    }
}
