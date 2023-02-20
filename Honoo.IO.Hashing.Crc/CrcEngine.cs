using System;

namespace Honoo.IO.HashingOld
{
    internal abstract class CrcEngine
    {
        #region Properties

        private readonly string _algorithmName;
        private readonly int _checksumSize;
        private readonly bool _useTable;
        internal string AlgorithmName => _algorithmName;
        internal int ChecksumSize => _checksumSize;
        internal bool UseTable => _useTable;

        #endregion Properties

        #region Construction

        protected CrcEngine(string algorithmName, int checksumSize, bool useTable)
        {
            _algorithmName = algorithmName ?? throw new ArgumentNullException(nameof(algorithmName));
            _checksumSize = checksumSize;
            _useTable = useTable;
        }

        #endregion Construction

        internal abstract object DoFinal();

        internal abstract byte[] DoFinal(bool littleEndian);

        internal abstract void Reset();

        internal void Update(byte input)
        {
            if (_useTable)
            {
                UpdateWithTable(input);
            }
            else
            {
                UpdateWithoutTable(input);
            }
        }

        protected abstract void UpdateWithoutTable(byte input);

        protected abstract void UpdateWithTable(byte input);
    }
}