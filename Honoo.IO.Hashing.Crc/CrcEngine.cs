using System;

namespace Honoo.IO.Hashing
{
    internal abstract class CrcEngine
    {
        #region Properties

        private readonly string _algorithmName;
        private readonly int _checksumLength;
        private readonly int _checksumSize;
        private readonly bool _withTable;
        internal string AlgorithmName => _algorithmName;
        internal int ChecksumLength => _checksumLength;
        internal int ChecksumSize => _checksumSize;
        internal bool WithTable => _withTable;

        #endregion Properties

        #region Construction

        protected CrcEngine(string algorithmName, int checksumSize, bool useTable)
        {
            _algorithmName = algorithmName ?? throw new ArgumentNullException(nameof(algorithmName));
            _checksumSize = checksumSize;
            _checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            _withTable = useTable;
        }

        #endregion Construction

        internal abstract string DoFinal();

        internal abstract byte[] DoFinal(bool littleEndian);

        internal abstract int DoFinal(bool littleEndian, byte[] output, int offset);

        internal abstract bool DoFinal(out byte checksum);

        internal abstract bool DoFinal(out ushort checksum);

        internal abstract bool DoFinal(out uint checksum);

        internal abstract bool DoFinal(out ulong checksum);

        internal abstract void Reset();

        internal void Update(byte input)
        {
            if (_withTable)
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