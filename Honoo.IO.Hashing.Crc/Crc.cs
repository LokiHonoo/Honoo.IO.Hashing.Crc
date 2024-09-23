using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Represents the abstract base class from which all implementations of crc algorithms must inherit.
    /// <br/>Catalogue of parametrised CRC algorithms: <see href="https://reveng.sourceforge.io/crc-catalogue/all.htm"/>.
    /// </summary>
    public abstract class Crc
    {
        #region Members

        private readonly CrcEngine _engine;
        private readonly string _name;

        /// <summary>
        /// Gets output checksum bytes length.
        /// </summary>
        public int ChecksumByteLength => _engine.ChecksumByteLength;

        /// <summary>
        /// Gets a value indicating whether the calculate core.
        /// </summary>
        public CrcCore Core => _engine.Core;

        /// <summary>
        /// Gets algorithm name.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets crc width in bits.
        /// </summary>
        public int Width => _engine.Width;

        /// <summary>
        /// Gets a value indicating whether the calculate with table.
        /// </summary>
        public bool WithTable => _engine.WithTable;

        #endregion Members

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
        /// <exception cref="Exception"></exception>
        public static Crc Create(CrcName algorithmName)
        {
            if (algorithmName == null)
            {
                throw new ArgumentNullException(nameof(algorithmName));
            }
            return algorithmName.GetAlgorithm();
        }

        /// <summary>
        /// Creates an instance of the algorithm by algorithm name.
        /// </summary>
        /// <param name="mechanism">Crc algorithm name.</param>
        /// <returns></returns>
        public static Crc Create(string mechanism)
        {
            if (CrcName.TryGetAlgorithmName(mechanism, out CrcName algorithmName))
            {
                return algorithmName.GetAlgorithm();
            }
            return null;
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable = true)
        {
            return new CrcCustom(name, width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable = true)
        {
            return new CrcCustom(name, width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable = true)
        {
            return new CrcCustom(name, width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable = true)
        {
            return new CrcCustom(name, width, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, bool refin, bool refout, CrcParameter poly, CrcParameter init, CrcParameter xorout, bool withTable = true, CrcCore core = CrcCore.Auto)
        {
            return new CrcCustom(name, width, refin, refout, poly, init, xorout, withTable, core);
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        public object CloneTable()
        {
            return _engine.CloneTable();
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out byte[] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case byte[] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(byte[])}\".");
            }
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out ushort[] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case ushort[] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(ushort[])}\".");
            }
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out uint[] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case uint[] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(uint[])}\".");
            }
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out ulong[] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case ulong[] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(ulong[])}\".");
            }
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out byte[][] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case byte[][] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(byte[][])}\".");
            }
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out ushort[][] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case ushort[][] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(ushort[][])}\".");
            }
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out uint[][] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case uint[][] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(uint[][])}\".");
            }
        }

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        /// <param name="table">a cloned table.</param>
        /// <exception cref="Exception"></exception>
        public void CloneTable(out ulong[][] table)
        {
            object obj = _engine.CloneTable();
            switch (obj)
            {
                case ulong[][] t: table = t; break;
                case null: table = null; break;
                default: throw new InvalidCastException($"Cannot convert type \"{obj.GetType()}\" to type \"{typeof(ulong[][])}\".");
            }
        }

        /// <summary>
        /// Computes checksum and reset the calculator. The return value is "Binary String" or "Hex String".
        /// </summary>
        /// <param name="outputFormat">Specifies the type of format for output.</param>
        /// <returns></returns>
        public string ComputeFinal(CrcStringFormat outputFormat)
        {
            return _engine.ComputeFinal(outputFormat);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        public byte[] ComputeFinal(Endian outputEndian)
        {
            byte[] result = new byte[_engine.ChecksumByteLength];
            ComputeFinal(outputEndian, result, 0);
            return result;
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
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool ComputeFinal(out ulong checksum)
        {
            return _engine.ComputeFinal(out checksum);
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
            _engine.Update(inputBuffer, offset, length);
        }
    }
}