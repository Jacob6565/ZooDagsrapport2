﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyOrder
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public DummyOrder(string name)
        {
            Name = name;
        }
    }
}