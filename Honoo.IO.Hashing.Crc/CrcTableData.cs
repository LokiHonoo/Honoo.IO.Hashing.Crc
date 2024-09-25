namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC Table data.
    /// </summary>
    public sealed class CrcTableData
    {
        internal CrcTableData(CrcCore core, CrcTableInfo info, object table)
        {
            this.Core = core;
            this.Info = info;
            this.Table = table;
        }

        /// <summary>
        /// Gets table using by core.
        /// </summary>
        public CrcCore Core { get; }

        /// <summary>
        /// Gets table info.
        /// </summary>
        public CrcTableInfo Info { get; }

        /// <summary>
        /// Gets calculation table.
        /// </summary>
        public object Table { get; }
    }
}