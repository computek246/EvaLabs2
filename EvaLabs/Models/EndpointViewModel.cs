﻿using System.Collections.Generic;
using System.Reflection;

namespace EvaLabs.Models
{
    public class EndpointViewModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string DisplayableName { get; set; }
        public string ReturnType { get; set; }
        public List<string> Attributes { get; set; }


        public string Description { get; set; }
    }
}
