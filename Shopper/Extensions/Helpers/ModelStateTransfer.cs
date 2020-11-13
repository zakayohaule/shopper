﻿using System.Collections.Generic;

namespace Shopper.Extensions.Helpers
{
    public class ModelStateTransfer
    {
        public string Key { get; set; }
        public string AttemptedValue { get; set; }
        public object RawValue { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
    }
}