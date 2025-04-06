using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Represents the abstract base class from which all implementations of CRC algorithms must inherit.
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
        /// Gets a value indicating whether the calculate with table.
        /// </summary>
        public CrcTableInfo TableInfo => _engine.TableInfo;

        /// <summary>
        /// Gets crc width in bits.
        /// </summary>
        public int Width => _engine.Width;

        #endregion Members

        #region Construction

        internal Crc(string name, CrcEngine engine)
        {
            _name = name;
            _engine = engine;
        }

        #endregion Construction

        #region Create

        /// <summary>
        /// Creates an instance of the algorithm by algorithm name.
        /// </summary>
        /// <param name="algorithmName">Crc algorithm name.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Crc Create(CrcName algorithmName, CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            if (algorithmName == null)
            {
                throw new ArgumentNullException(nameof(algorithmName));
            }
            return algorithmName.GetAlgorithm(withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by algorithm name.
        /// </summary>
        /// <param name="mechanism">Crc algorithm name.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc Create(string mechanism, CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            if (CrcName.TryGetAlgorithmName(mechanism, out CrcName algorithmName))
            {
                return algorithmName.GetAlgorithm(withTable);
            }
            return null;
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 8.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, byte poly, byte init, byte xorout, bool refin, bool refout, CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 8.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, byte poly, byte init, byte xorout, bool refin, bool refout, CrcTable table)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, table);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 16.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, ushort poly, ushort init, ushort xorout, bool refin, bool refout, CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 16.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, ushort poly, ushort init, ushort xorout, bool refin, bool refout, CrcTable table)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, table);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 32.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, uint poly, uint init, uint xorout, bool refin, bool refout, CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 32.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, uint poly, uint init, uint xorout, bool refin, bool refout, CrcTable table)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, table);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 64.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, ulong poly, ulong init, ulong xorout, bool refin, bool refout, CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 64.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, ulong poly, ulong init, ulong xorout, bool refin, bool refout, CrcTable table)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, table);
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, withTable);
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc CreateBy(string name, int width, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, CrcTable table)
        {
            return new CrcCustom(name, width, poly, init, xorout, refin, refout, table);
        }

        #endregion Create

        #region Table

        /// <summary>
        /// Clone calculation table if exists.
        /// </summary>
        public CrcTable CloneTable()
        {
            return _engine.CloneTable();
        }

        #endregion Table

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// </summary>
        /// <returns></returns>
        public CrcValue ComputeFinal()
        {
            return _engine.ComputeFinal();
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CrcValue ComputeFinal(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            return ComputeFinal(input, 0, input.Length);
        }

        /// <summary>
        /// Computes checksum and reset the calculator.
        /// </summary>
        /// <param name="inputBuffer">Input buffer.</param>
        /// <param name="offset">Read start offset from buffer.</param>
        /// <param name="length">Read length.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CrcValue ComputeFinal(byte[] inputBuffer, int offset, int length)
        {
            _engine.Update(inputBuffer, offset, length);
            return ComputeFinal();
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