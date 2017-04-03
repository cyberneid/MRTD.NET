﻿using System.Linq;
using HelloWord.Cryptography;
using HelloWord.Cryptography.RandomKeys;
using HelloWord.Infrastructure;

namespace HelloWord.SmartCard
{
    public class ExternalAuthenticateCommandData : IBinary
    {
        private readonly string _mrzInformation;
        private readonly IBinary _rndIc;
        private readonly IBinary _rndIfd;
        private readonly IBinary _kIfd;

        public ExternalAuthenticateCommandData(
            string mrzInformation, 
            IBinary rndIc, 
            IBinary rndIfd, 
            IBinary kIfd)
        {
            _mrzInformation = mrzInformation;
            _rndIc = rndIc;
            _rndIfd = rndIfd;
            _kIfd = kIfd;
        }
   

        public byte[] Bytes()
        {
            var kSeed = new Kseed(
                            new SHA1(
                                new UTF8String(_mrzInformation)
                            )
                        );
            var eIfd = new Eifd(
                    new S(
                        _rndIfd,
                        _rndIc,
                        _kIfd
                    ),
                    new Kenc(kSeed)
                );
            var mIfd = new Mifd(
                    eIfd,
                    new Kmac(kSeed)
                );
            return new CmdData(
                    eIfd,
                    mIfd
                ).Bytes();
        }


        private class CmdData : IBinary
        {
            private readonly IBinary _eIfd;
            private readonly IBinary _mIfd;
            public CmdData(IBinary eIfd, IBinary mIfd)
            {
                _eIfd = eIfd;
                _mIfd = mIfd;
            }

            public byte[] Bytes()
            {
                return new CombinedBinaries(
                        _eIfd,
                        _mIfd
                    ).Bytes();
            }
        }
    }
}
