using System;

namespace Honoo.IO.Hashing
{
    internal abstract class CrcEngine
    {
        #region Properties

        protected readonly int _checksumByteLength;
        protected readonly int _checksumHexLength;
        protected readonly int _checksumSize;
        protected readonly bool _refin;
        protected readonly bool _refout;
        protected readonly bool _withTable;
        private readonly string _algorithmName;
        internal string AlgorithmName => _algorithmName;
        internal int ChecksumLength => _checksumByteLength;
        internal int ChecksumSize => _checksumSize;
        internal abstract string InitHex { get; }
        internal abstract string PolyHex { get; }
        internal bool Refin => _refin;
        internal bool Refout => _refout;
        internal bool WithTable => _withTable;
        internal abstract string XoroutHex { get; }

        #endregion Properties

        #region Construction

        protected CrcEngine(string algorithmName, int checksumSize, bool refin, bool refout, bool useTable)
        {
            _algorithmName = algorithmName ?? throw new ArgumentNullException(nameof(algorithmName));
            _checksumSize = checksumSize;
            _checksumByteLength = (int)Math.Ceiling(checksumSize / 8d);
            _checksumHexLength = (int)Math.Ceiling(checksumSize / 4d);
            _refin = refin;
            _refout = refout;
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