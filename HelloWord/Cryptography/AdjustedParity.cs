﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWord.Infrastructure;

namespace HelloWord.Cryptography
{
    public class AdjustedParity : IBinary
    {
        private readonly byte[] _bites;

        public AdjustedParity(byte[] bites)
        {
            _bites = bites;
        }
        public AdjustedParity(IBinary binary) : this(binary.Bytes()) { }
        public byte[] Bytes()
        {
            return _bites
               .Select(b => new Parity(b).Adjusted().Result())
               .ToArray();
        }
    }
}
