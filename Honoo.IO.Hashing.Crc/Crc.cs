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
        /// Gets output checksum byte length.
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
            if (algorithmName == null)
            {
                throw new ArgumentNullException(nameof(algorithmName));
            }
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
        public static Crc CreateBy(int width, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable = true)
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
        public static Crc CreateBy(int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable = true)
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
        public static Crc CreateBy(int width, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable = true)
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
        public static Crc CreateBy(int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable = true)
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
        public static Crc CreateBy(int width, bool refin, bool refout, string polyHex, string initHex, string xoroutHex, CrcCore core = CrcCore.Auto)
        {
            return new CrcCustom(width, refin, refout, polyHex, initHex, xoroutHex, core);
        }

        /// <summary>
        /// Computes checksum and reset the calculator. The return value is "Hex String".
        /// </summary>
        /// <returns></returns>
        public string ComputeFinal()
        {
            return _engine.ComputeFinal();
        }

        /// <summary>
        /// Computes checksum and reset the calculator. The return value is "Hex String".
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns></returns>
        public string ComputeFinal(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
            return ComputeFinal();
        }

        /// <summary>
        /// Computes checksum and reset the calculator. The return value is "Hex String".
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="inputOffset">Read start offset from buffer.</param>
        /// <param name="inputLength">Read length from buffer.</param>
        /// <returns></returns>
        public string ComputeFinal(byte[] inputBuffer, int inputOffset, int inputLength)
        {
            Update(inputBuffer, inputOffset, inputLength);
            return ComputeFinal();
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        public byte[] ComputeFinal(Endian outputEndian)
        {
            byte[] result = new byte[_engine.ChecksumLength];
            ComputeFinal(outputEndian, result, 0);
            return result;
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        public byte[] ComputeFinal(byte[] input, Endian outputEndian)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
            return ComputeFinal(outputEndian);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="inputOffset">Read start offset from buffer.</param>
        /// <param name="inputLength">Read length from buffer.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        public byte[] ComputeFinal(byte[] inputBuffer, int inputOffset, int inputLength, Endian outputEndian)
        {
            Update(inputBuffer, inputOffset, inputLength);
            return ComputeFinal(outputEndian);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            return _engine.ComputeFinal(outputEndian, outputBuffer, outputOffset);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int ComputeFinal(byte[] input, Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
            return ComputeFinal(outputEndian, outputBuffer, outputOffset);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="inputOffset">Read start offset from buffer.</param>
        /// <param name="inputLength">Read length from buffer.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int ComputeFinal(byte[] inputBuffer, int inputOffset, int inputLength, Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Update(inputBuffer, inputOffset, inputLength);
            return ComputeFinal(outputEndian, outputBuffer, outputOffset);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(out byte checksum)
        {
            return _engine.ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] input, out byte checksum)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
            return ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="inputOffset">Read start offset from buffer.</param>
        /// <param name="inputLength">Read length from buffer.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] inputBuffer, int inputOffset, int inputLength, out byte checksum)
        {
            Update(inputBuffer, inputOffset, inputLength);
            return ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(out ushort checksum)
        {
            return _engine.ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] input, out ushort checksum)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
            return ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="inputOffset">Read start offset from buffer.</param>
        /// <param name="inputLength">Read length from buffer.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] inputBuffer, int inputOffset, int inputLength, out ushort checksum)
        {
            Update(inputBuffer, inputOffset, inputLength);
            return ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(out uint checksum)
        {
            return _engine.ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] input, out uint checksum)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
            return ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="inputOffset">Read start offset from buffer.</param>
        /// <param name="inputLength">Read length from buffer.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] inputBuffer, int inputOffset, int inputLength, out uint checksum)
        {
            Update(inputBuffer, inputOffset, inputLength);
            return ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(out ulong checksum)
        {
            return _engine.ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] input, out ulong checksum)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
            return ComputeFinal(out checksum);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a <see cref="bool"/> indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="inputOffset">Read start offset from buffer.</param>
        /// <param name="inputLength">Read length from buffer.</param>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(byte[] inputBuffer, int inputOffset, int inputLength, out ulong checksum)
        {
            Update(inputBuffer, inputOffset, inputLength);
            return ComputeFinal(out checksum);
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
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Update(input, 0, input.Length);
        }

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="offset">Read start offset from buffer.</param>
        /// <param name="length">Read length from buffer.</param>
        /// <exception cref="Exception"></exception>
        public void Update(byte[] inputBuffer, int offset, int length)
        {
            if (inputBuffer == null)
            {
                throw new ArgumentNullException(nameof(inputBuffer));
            }
            for (int i = offset; i < offset + length; i++)
            {
                Update(inputBuffer[i]);
            }
        }
    }
}