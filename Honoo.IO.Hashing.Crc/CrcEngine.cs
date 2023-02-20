using System;

namespace Honoo.IO.Hashing
{
    internal abstract class CrcEngine
    {
        #region Properties

        protected readonly int _checksumSize;
        private readonly string _algorithmName;

        internal string AlgorithmName => _algorithmName;

        internal int ChecksumSize => _checksumSize;

        #endregion Properties

        #region Construction

        protected CrcEngine(string algorithmName, int checksumSize)
        {
            _algorithmName = algorithmName ?? throw new ArgumentNullException(nameof(algorithmName));
            _checksumSize = checksumSize;
        }

        #endregion Construction

        internal abstract byte[] DoFinal();

        internal abstract void Reset();

        internal abstract void Update(byte[] buffer, int offset, int length);
    }
}