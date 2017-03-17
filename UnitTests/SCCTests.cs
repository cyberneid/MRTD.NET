﻿using System;
using HelloWord.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SCCTests
    {
        [TestMethod]
        [DataRow("887022120C06C226", "4608F91988702212", "781723860C06C226")]
        public void Calculate_SendSequenceCounter_from_RNDic_and_RNDifd(string act, string rndIc, string rndIfd)
        {
            Assert.AreEqual(
                    act,
                    new Hex(
                        new SSC(
                            new BinaryHex(rndIc),
                            new BinaryHex(rndIfd)
                        )
                    ).ToString()
                );
        }
    }
}
