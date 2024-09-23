namespace Honoo.IO.Hashing
{
    internal abstract class CrcEngine
    {
        #region Members

        internal abstract int ChecksumByteLength { get; }
        internal abstract CrcCore Core { get; }
        internal abstract int Width { get; }
        internal abstract bool WithTable { get; }

        #endregion Members

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcEngine class.
        /// </summary>
        protected CrcEngine()
        {
        }

        #endregion Construction

        internal abstract object CloneTable();

        internal abstract string ComputeFinal(CrcStringFormat outputFormat);

        internal abstract int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset);

        internal abstract bool ComputeFinal(out byte checksum);

        internal abstract bool ComputeFinal(out ushort checksum);

        internal abstract bool ComputeFinal(out uint checksum);

        internal abstract bool ComputeFinal(out ulong checksum);

        internal abstract void Reset();

        internal abstract void Update(byte input);

        internal abstract void Update(byte[] inputBuffer, int offset, int length);
    }
}