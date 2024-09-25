using System;

namespace Honoo.IO.Hashing
{
    internal abstract class CrcEngine : IDisposable
    {
        #region Members

        internal abstract int ChecksumByteLength { get; }
        internal abstract CrcCore Core { get; }
        internal abstract int Width { get; }
        internal abstract CrcTable WithTable { get; }

        #endregion Members

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcEngine class.
        /// </summary>
        protected CrcEngine()
        {
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        ~CrcEngine()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);

        #endregion Construction

        internal abstract object CloneTable();

        internal abstract string ComputeFinal(CrcStringFormat outputFormat);

        internal abstract int ComputeFinal(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset);

        internal abstract bool ComputeFinal(out byte checksum);

        internal abstract bool ComputeFinal(out ushort checksum);

        internal abstract bool ComputeFinal(out uint checksum);

        internal abstract bool ComputeFinal(out ulong checksum);

        internal abstract void Reset();

        internal abstract void Update(byte input);

        internal abstract void Update(byte[] inputBuffer, int offset, int length);
    }
}