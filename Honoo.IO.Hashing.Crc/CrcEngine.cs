﻿using System;

namespace Honoo.IO.Hashing
{
    internal abstract class CrcEngine
    {
        #region Properties

        protected readonly int _checksumByteLength;
        protected readonly int _checksumHexLength;
        protected readonly bool _refin;
        protected readonly bool _refout;
        protected readonly int _width;
        protected readonly bool _withTable;
        internal int ChecksumLength => _checksumByteLength;
        internal int Width => _width;
        internal bool WithTable => _withTable;

        #endregion Properties

        #region Construction

        protected CrcEngine(int width, bool refin, bool refout, bool useTable)
        {
            _width = width;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _refin = refin;
            _refout = refout;
            _withTable = useTable;
        }

        #endregion Construction

        internal abstract string ComputeFinal();

        internal abstract int ComputeFinal(bool littleEndian, byte[] output, int offset);

        internal abstract bool ComputeFinal(out byte checksum);

        internal abstract bool ComputeFinal(out ushort checksum);

        internal abstract bool ComputeFinal(out uint checksum);

        internal abstract bool ComputeFinal(out ulong checksum);

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