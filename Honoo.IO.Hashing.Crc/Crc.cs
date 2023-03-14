using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Represents the abstract base class from which all implementations of crc algorithms must inherit.
    /// <br/>Catalogue of parametrised CRC algorithms: https://reveng.sourceforge.io/crc-catalogue/all.htm .
    /// </summary>
    public abstract class Crc
    {
        #region Properties

        private readonly CrcEngine _engine;
        private readonly string _name;

        /// <summary>
        /// Gets iyrput checksum byte length.
        /// </summary>
        public int ChecksumLength => _engine.ChecksumLength;

        /// <summary>
        /// Gets algorithm name.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets algorithm width bits.
        /// </summary>
        public int Width => _engine.Width;
        /// <summary>
        /// Gets a value indicating whether the calculations with the table.
        /// </summary>
        public bool WithTable => _engine.WithTable;

        #endregion Properties

        #region Construction

        internal Crc(string name, CrcEngine engine)
        {
            _name = name;
            _engine = engine;
        }

        #endregion Construction

        /// <summary>
        /// Creates an instance of the algorithm by algorithm name.
        /// </summary>
        /// <param name="algorithmName">Crc algorithm name.</param>
        /// <returns></returns>
        public static Crc Create(CrcName algorithmName)
        {
            return algorithmName.GetAlgorithm();
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int width, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable = true)
        {
            return new CrcCustom(width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable = true)
        {
            return new CrcCustom(width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int width, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable = true)
        {
            return new CrcCustom(width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable = true)
        {
            return new CrcCustom(width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="polyHex">Polynomials value hex string.</param>
        /// <param name="initHex">Initialization value hex string.</param>
        /// <param name="xoroutHex">Output xor value hex string.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int width, bool refin, bool refout, string polyHex, string initHex, string xoroutHex, CrcCore core = CrcCore.Auto)
        {
            return new CrcCustom(width, refin, refout, polyHex, initHex, xoroutHex, core);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator. The return value is "Hex String".
        /// </summary>
        /// <returns></returns>
        public string DoFinal()
        {
            return _engine.DoFinal();
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// </summary>
        /// <param name="littleEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        public byte[] DoFinal(bool littleEndian)
        {
            return _engine.DoFinal(littleEndian);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="littleEndian">Specifies the type of endian for output.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="offset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int DoFinal(bool littleEndian, byte[] output, int offset)
        {
            return _engine.DoFinal(littleEndian, output, offset);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out byte checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out ushort checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out uint checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out ulong checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Reset calculator.
        /// </summary>
        public void Reset()
        {
            _engine.Reset();
        }

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <exception cref="Exception"></exception>
        public void Update(byte input)
        {
            _engine.Update(input);
        }

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <exception cref="Exception"></exception>
        public void Update(byte[] input)
        {
            Update(input, 0, input.Length);
        }

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="buffer">Input buffer.</param>
        /// <param name="offset">Read start offset from buffer.</param>
        /// <param name="length">Read length from buffer.</param>
        /// <exception cref="Exception"></exception>
        public void Update(byte[] buffer, int offset, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            for (int i = offset; i < offset + length; i++)
            {
                Update(buffer[i]);
            }
        }
    }
}