﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenInBible.Interfaces
{
    public interface IModel
    {
        void FillAllProperties<T>(T item);
    }
}
