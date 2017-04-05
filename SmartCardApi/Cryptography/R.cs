﻿using SmartCardApi.Infrastructure;
using SmartCardApi.MRZ;
using SmartCardApi.SmartCard;

namespace SmartCardApi.Cryptography
{
    public class R : IBinary
    {
        private readonly IBinary _externalAuthRespData;
        private readonly MRZInfo _mrzInformation;

        public R(
                IBinary externalAuthRespData,
                MRZInfo mrzInformation
            )
        {
            _externalAuthRespData = externalAuthRespData;
            _mrzInformation = mrzInformation;
        }
        public byte[] Bytes()
        {
            return new DecryptedExternalAuthenticateResponseData(
                    _externalAuthRespData,
                    _mrzInformation
                ).Bytes();
        }
    }
}