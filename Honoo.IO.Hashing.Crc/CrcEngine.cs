namespace Honoo.IO.Hashing
{
    internal abstract class CrcEngine
    {
        #region Members

        internal abstract int ChecksumByteLength { get; }
        internal abstract CrcCore Core { get; }
        internal abstract CrcTableInfo TableInfo { get; }
        internal abstract int Width { get; }

        #endregion Members

        #region Construction

        internal CrcEngine()
        {
        }

        #endregion Construction

        internal abstract CrcTable CloneTable();

        internal abstract CrcValue ComputeFinal();

        internal abstract void Reset();

        internal abstract void Update(byte input);

        internal abstract void Update(byte[] inputBuffer, int offset, int length);
    }
}