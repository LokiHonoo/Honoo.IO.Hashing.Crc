namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC parameters interface.
    /// </summary>
    public interface ICrcParameters
    {
        /// <summary>
        /// Gets checksum size bits.
        /// </summary>
        int ChecksumSize { get; }

        /// <summary>
        /// Initialization value hex string.
        /// </summary>
        string InitHex { get; }

        /// <summary>
        /// Polynomials value hex string.
        /// </summary>
        string PolyHex { get; }

        /// <summary>
        /// Reflects input value.
        /// </summary>
        bool Refin { get; }

        /// <summary>
        /// Reflects output value.
        /// </summary>
        bool Refout { get; }

        /// <summary>
        /// Output xor value hex string.
        /// </summary>
        string XoroutHex { get; }
    }
}