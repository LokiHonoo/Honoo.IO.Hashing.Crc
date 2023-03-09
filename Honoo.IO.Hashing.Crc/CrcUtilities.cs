using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Crc utilities.
    /// </summary>
    public sealed class CrcUtilities
    {
        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of checksum buffer.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="checksumSize">Checksum size bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static byte ToByte(bool littleEndian, byte[] buffer, int startIndex, int checksumSize)
        {
            if (littleEndian)
            {
                return buffer[startIndex];
            }
            else
            {
                int checksumLength = (int)Math.Ceiling(checksumSize / 8d);
                return buffer[checksumLength - 1 + startIndex];
            }
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of checksum buffer.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="checksumSize">Checksum size bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static string ToHexString(bool littleEndian, byte[] buffer, int startIndex, int checksumSize)
        {
            int checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            int stringLength = (int)Math.Ceiling(checksumSize / 4d);
            int length = Math.Min(checksumLength, buffer.Length - startIndex);
            StringBuilder result = new StringBuilder();
            if (littleEndian)
            {
                for (int i = 0; i < length; i++)
                {
                    result.Append(Convert.ToString(buffer[length - 1 - i + startIndex], 16).PadLeft(2, '0'));
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result.Append(Convert.ToString(buffer[i + startIndex], 16).PadLeft(2, '0'));
                }
            }
            if (result.Length > stringLength)
            {
                return result.ToString(result.Length - stringLength, stringLength).ToUpperInvariant();
            }
            else
            {
                return result.ToString().ToUpperInvariant();
            }
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of checksum buffer.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="checksumSize">Checksum size bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static ushort ToUInt16(bool littleEndian, byte[] buffer, int startIndex, int checksumSize)
        {
            int checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            ushort result;
            if (littleEndian)
            {
                result = buffer[startIndex];
                if (checksumLength > 1) result |= (ushort)((buffer[startIndex + 1] & 0xFF) << 8);
            }
            else
            {
                result = buffer[checksumLength - 1 + startIndex];
                if (checksumLength > 1) result |= (ushort)((buffer[checksumLength - 1 + startIndex - 1] & 0xFF) << 8);
            }
            return result;
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of checksum buffer.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="checksumSize">Checksum size bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static uint ToUInt32(bool littleEndian, byte[] buffer, int startIndex, int checksumSize)
        {
            int checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            uint result;
            if (littleEndian)
            {
                result = buffer[startIndex];
                if (checksumLength > 1) result |= (buffer[startIndex + 1] & 0xFFU) << 8;
                if (checksumLength > 2) result |= (buffer[startIndex + 2] & 0xFFU) << 16;
                if (checksumLength > 3) result |= (buffer[startIndex + 3] & 0xFFU) << 24;
            }
            else
            {
                result = buffer[checksumLength - 1 + startIndex];
                if (checksumLength > 1) result |= (buffer[checksumLength - 1 + startIndex - 1] & 0xFFU) << 8;
                if (checksumLength > 2) result |= (buffer[checksumLength - 1 + startIndex - 2] & 0xFFU) << 16;
                if (checksumLength > 3) result |= (buffer[checksumLength - 1 + startIndex - 3] & 0xFFU) << 24;
            }
            return result;
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of checksum buffer.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="checksumSize">Checksum size bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static ulong ToUInt64(bool littleEndian, byte[] buffer, int startIndex, int checksumSize)
        {
            int checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            ulong result;
            if (littleEndian)
            {
                result = buffer[startIndex];
                if (checksumLength > 1) result |= (buffer[startIndex + 1] & 0xFFUL) << 8;
                if (checksumLength > 2) result |= (buffer[startIndex + 2] & 0xFFUL) << 16;
                if (checksumLength > 3) result |= (buffer[startIndex + 3] & 0xFFUL) << 24;
                if (checksumLength > 4) result |= (buffer[startIndex + 4] & 0xFFUL) << 32;
                if (checksumLength > 5) result |= (buffer[startIndex + 5] & 0xFFUL) << 40;
                if (checksumLength > 6) result |= (buffer[startIndex + 6] & 0xFFUL) << 48;
                if (checksumLength > 7) result |= (buffer[startIndex + 7] & 0xFFUL) << 56;
            }
            else
            {
                result = buffer[checksumLength - 1 + startIndex];
                if (checksumLength > 1) result |= (buffer[checksumLength - 1 + startIndex - 1] & 0xFFUL) << 8;
                if (checksumLength > 2) result |= (buffer[checksumLength - 1 + startIndex - 2] & 0xFFUL) << 16;
                if (checksumLength > 3) result |= (buffer[checksumLength - 1 + startIndex - 3] & 0xFFUL) << 24;
                if (checksumLength > 4) result |= (buffer[checksumLength - 1 + startIndex - 4] & 0xFFUL) << 32;
                if (checksumLength > 5) result |= (buffer[checksumLength - 1 + startIndex - 5] & 0xFFUL) << 40;
                if (checksumLength > 6) result |= (buffer[checksumLength - 1 + startIndex - 6] & 0xFFUL) << 48;
                if (checksumLength > 7) result |= (buffer[checksumLength - 1 + startIndex - 7] & 0xFFUL) << 56;
            }
            return result;
        }

        /// <summary>
        /// Try get CRC algorithm from mechanism.
        /// </summary>
        /// <param name="mechanism">CRC algorithm mechanism.</param>
        /// <param name="algorithm">CRC algorithm.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <returns></returns>
        public static bool TryGetAlgorithm(string mechanism, out Crc algorithm, bool withTable = true)
        {
            if (string.IsNullOrWhiteSpace(mechanism))
            {
                algorithm = null;
                return false;
            }
            mechanism = mechanism.Replace("_", null).Replace("-", null).Replace("/", null).Replace("\\", null).Trim().ToUpperInvariant();
            switch (mechanism)
            {
                case "CRC3GSM": algorithm = new Crc3Gsm(withTable); return true;
                case "CRC3ROHC": algorithm = new Crc3Rohc(withTable); return true;
                case "CRC4ITU": algorithm = new Crc4Itu(withTable); return true;
                case "CRC4G704": algorithm = new Crc4Itu("CRC-4/G-704", withTable); return true;
                case "CRC4INTERLAKEN": algorithm = new Crc4Interlaken(withTable); return true;
                case "CRC5EPC": algorithm = new Crc5Epc(withTable); return true;
                case "CRC5EPCC1G2": algorithm = new Crc5Epc("CRC-5/EPC-C1G2", withTable); return true;
                case "CRC5ITU": algorithm = new Crc5Itu(withTable); return true;
                case "CRC5G704": algorithm = new Crc5Itu("CRC-5/G-704", withTable); return true;
                case "CRC5USB": algorithm = new Crc5Usb(withTable); return true;
                case "CRC6CDMA2000A": algorithm = new Crc6Cdma2000A(withTable); return true;
                case "CRC6CDMA2000B": algorithm = new Crc6Cdma2000B(withTable); return true;
                case "CRC6DARC": algorithm = new Crc6Darc(withTable); return true;
                case "CRC6GSM": algorithm = new Crc6Gsm(withTable); return true;
                case "CRC6ITU": algorithm = new Crc6Itu(withTable); return true;
                case "CRC6G704": algorithm = new Crc6Itu("CRC-6/G-704", withTable); return true;
                case "CRC7": algorithm = new Crc7(withTable); return true;
                case "CRC7MMC": algorithm = new Crc7("CRC-7/MMC", withTable); return true;
                case "CRC7ROHC": algorithm = new Crc7Rohc(withTable); return true;
                case "CRC7UMTS": algorithm = new Crc7Umts(withTable); return true;
                case "CRC8": algorithm = new Crc8(withTable); return true;
                case "CRC8SMBUS": algorithm = new Crc8("CRC-8/SMBUS", withTable); return true;
                case "CRC8OPENSAFETY": algorithm = new Crc8Opensafety(withTable); return true;
                case "CRC8SAEJ1850": algorithm = new Crc8SaeJ1850(withTable); return true;
                case "CRC8WCDMA": algorithm = new Crc8Wcdma(withTable); return true;
                case "CRC8BLUETOOTH": algorithm = new Crc8Bluetooth(withTable); return true;
                case "CRC8AUTOSAR": algorithm = new Crc8Autosar(withTable); return true;
                case "CRC8CDMA2000": algorithm = new Crc8Cdma2000(withTable); return true;
                case "CRC8NRSC5": algorithm = new Crc8Nrsc5(withTable); return true;
                case "CRC8LTE": algorithm = new Crc8Lte(withTable); return true;
                case "CRC8DARC": algorithm = new Crc8Darc(withTable); return true;
                case "CRC8DVBS2": algorithm = new Crc8DvbS2(withTable); return true;
                case "CRC8HITAG": algorithm = new Crc8Hitag(withTable); return true;
                case "CRC8GSMA": algorithm = new Crc8GsmA(withTable); return true;
                case "CRC8GSMB": algorithm = new Crc8GsmB(withTable); return true;
                case "CRC8EBU": algorithm = new Crc8Ebu(withTable); return true;
                case "CRC8TECH3250": algorithm = new Crc8Ebu("CRC-8/TECH-3250", withTable); return true;
                case "CRC8AES": algorithm = new Crc8Ebu("CRC-8/AES", withTable); return true;
                case "CRC8ICODE": algorithm = new Crc8ICode(withTable); return true;
                case "CRC8MIFAREMAD": algorithm = new Crc8MifareMad(withTable); return true;
                case "CRC8ITU": algorithm = new Crc8Itu(withTable); return true;
                case "CRC8I4321": algorithm = new Crc8Itu("CRC-8/I-432-1", withTable); return true;
                case "CRC8ATM": algorithm = new Crc8Itu("CRC-8/ATM", withTable); return true;
                case "CRC8MAXIM": algorithm = new Crc8Maxim(withTable); return true;
                case "CRC8MAXIMDOW": algorithm = new Crc8Maxim("CRC-8/MAXIM-DOW", withTable); return true;
                case "DOWCRC": algorithm = new Crc8Maxim("DOW-CRC", withTable); return true;
                case "CRC8ROHC": algorithm = new Crc8Rohc(withTable); return true;
                case "CRC10": algorithm = new Crc10(withTable); return true;
                case "CRC10ATM": algorithm = new Crc10("CRC-10/ATM", withTable); return true;
                case "CRC10I610": algorithm = new Crc10("CRC-10/I-610", withTable); return true;
                case "CRC10CDMA2000": algorithm = new Crc10Cdma2000(withTable); return true;
                case "CRC10GSM": algorithm = new Crc10Gsm(withTable); return true;
                case "CRC11": algorithm = new Crc11(withTable); return true;
                case "CRC11FLEXRAY": algorithm = new Crc11("CRC-11/FLEXRAY", withTable); return true;
                case "CRC11UMTS": algorithm = new Crc11Umts(withTable); return true;
                case "CRC12CDMA2000": algorithm = new Crc12Cdma2000(withTable); return true;
                case "CRC12DECT": algorithm = new Crc12Dect(withTable); return true;
                case "XCRC12": algorithm = new Crc12Dect("X-CRC-12", withTable); return true;
                case "CRC12GSM": algorithm = new Crc12Gsm(withTable); return true;
                case "CRC12UMTS": algorithm = new Crc12Umts(withTable); return true;
                case "CRC123GPP": algorithm = new Crc12Umts("CRC-12/3GPP", withTable); return true;
                case "CRC13BBC": algorithm = new Crc13bbc(withTable); return true;
                case "CRC14DARC": algorithm = new Crc14Darc(withTable); return true;
                case "CRC14GSM": algorithm = new Crc14Gsm(withTable); return true;
                case "CRC15": algorithm = new Crc15(withTable); return true;
                case "CRC15CAN": algorithm = new Crc15("CRC-15/CAN", withTable); return true;
                case "CRC15MPT1327": algorithm = new Crc15Mpt1327(withTable); return true;
                case "CRC16": algorithm = new Crc16(withTable); return true;
                case "CRC16ARC": algorithm = new Crc16("CRC-16/ARC", withTable); return true;
                case "ARC": algorithm = new Crc16("ARC", withTable); return true;
                case "CRC16LHA": algorithm = new Crc16("CRC-16/LHA", withTable); return true;
                case "CRCIBM": algorithm = new Crc16("CRC-IBM", withTable); return true;
                case "CRCA": algorithm = new CrcA(withTable); return true;
                case "CRC16CMS": algorithm = new Crc16Cms(withTable); return true;
                case "CRC16ISOIEC144433A": algorithm = new CrcA("CRC-16/ISO-IEC-14443-3-A", withTable); return true;
                case "CRC16SPIFUJITSU": algorithm = new Crc16SpiFujitsu(withTable); return true;
                case "CRC16AUGCCITT": algorithm = new Crc16SpiFujitsu("CRC-16/AUG-CCITT", withTable); return true;
                case "CRC16UMTS": algorithm = new Crc16Umts(withTable); return true;
                case "CRC16BUYPASS": algorithm = new Crc16Umts("CRC-16/BUYPASS", withTable); return true;
                case "CRC16VERIFONE": algorithm = new Crc16Umts("CRC-16/VERIFONE", withTable); return true;
                case "CRC16CCITT": algorithm = new Crc16Ccitt(withTable); return true;
                case "CRCCCITT": algorithm = new Crc16Ccitt("CRC-CCITT", withTable); return true;
                case "CRC16CCITTTRUE": algorithm = new Crc16Ccitt("CRC-16/CCITT-TRUE", withTable); return true;
                case "CRC16KERMIT": algorithm = new Crc16Ccitt("CRC-16/KERMIT", withTable); return true;
                case "KERMIT": algorithm = new Crc16Ccitt("KERMIT", withTable); return true;
                case "CRC16BLUETOOTH": algorithm = new Crc16Ccitt("CRC-16/BLUETOOTH", withTable); return true;
                case "CRC16V41LSB": algorithm = new Crc16Ccitt("CRC-16/V-41-LSB", withTable); return true;
                case "CRC16CCITTFALSE": algorithm = new Crc16CcittFalse(withTable); return true;
                case "CRC16IBM3740": algorithm = new Crc16CcittFalse("CRC-16/IBM-3740", withTable); return true;
                case "CRC16AUTOSAR": algorithm = new Crc16CcittFalse("CRC-16/AUTOSAR", withTable); return true;
                case "CRC16CDMA2000": algorithm = new Crc16Cdma2000(withTable); return true;
                case "CRC16DECTR": algorithm = new Crc16DectR(withTable); return true;
                case "RCRC16": algorithm = new Crc16DectR("R-CRC-16", withTable); return true;
                case "CRC16DECTX": algorithm = new Crc16DectX(withTable); return true;
                case "XCRC16": algorithm = new Crc16DectX("X-CRC-16", withTable); return true;
                case "CRC16DNP": algorithm = new Crc16Dnp(withTable); return true;
                case "CRC16DDS110": algorithm = new Crc16Dds110(withTable); return true;
                case "CRC16EN13757": algorithm = new Crc16En13757(withTable); return true;
                case "CRC16GENIBUS": algorithm = new Crc16Genibus(withTable); return true;
                case "CRC16DARC": algorithm = new Crc16Genibus("CRC-16/DARC", withTable); return true;
                case "CRC16EPC": algorithm = new Crc16Genibus("CRC-16/EPC", withTable); return true;
                case "CRC16EPCC1G2": algorithm = new Crc16Genibus("CRC-16/EPC-C1G2", withTable); return true;
                case "CRC16ICODE": algorithm = new Crc16Genibus("CRC-16/I-CODE", withTable); return true;
                case "CRC16M17": algorithm = new Crc16M17(withTable); return true;
                case "CRC16GSM": algorithm = new Crc16Gsm(withTable); return true;
                case "CRC16LJ1200": algorithm = new Crc16Lj1200(withTable); return true;
                case "CRC16MAXIM": algorithm = new Crc16Maxim(withTable); return true;
                case "CRC16MAXIMDOW": algorithm = new Crc16Maxim("CRC-16/MAXIM-DOW", withTable); return true;
                case "CRC16MCRF4XX": algorithm = new Crc16Mcrf4XX(withTable); return true;
                case "CRC16MODBUS": algorithm = new Crc16Modbus(withTable); return true;
                case "MODBUS": algorithm = new Crc16Modbus("MODBUS", withTable); return true;
                case "CRC16PROFIBUS": algorithm = new Crc16Profibus(withTable); return true;
                case "CRC16IEC611582": algorithm = new Crc16Profibus("CRC-16/IEC-61158-2", withTable); return true;
                case "CRC16NRSC5": algorithm = new Crc16Nrsc5(withTable); return true;
                case "CRC16OPENSAFETYA": algorithm = new Crc16OpensafetyA(withTable); return true;
                case "CRC16OPENSAFETYB": algorithm = new Crc16OpensafetyB(withTable); return true;
                case "CRC16RIELLO": algorithm = new Crc16Riello(withTable); return true;
                case "CRC16T10DIF": algorithm = new Crc16T10Dif(withTable); return true;
                case "CRC16TELEDISK": algorithm = new Crc16Teledisk(withTable); return true;
                case "CRC16TMS37157": algorithm = new Crc16Tms37157(withTable); return true;
                case "CRC16USB": algorithm = new Crc16Usb(withTable); return true;
                case "CRC16X25": algorithm = new Crc16X25(withTable); return true;
                case "X25": algorithm = new Crc16X25("X-25", withTable); return true;
                case "CRC16IBMSDLC": algorithm = new Crc16X25("CRC-16/IBM-SDLC", withTable); return true;
                case "CRC16ISOHDLC": algorithm = new Crc16X25("CRC-16/ISO-HDLC", withTable); return true;
                case "CRC16ISOIEC144433B": algorithm = new Crc16X25("CRC-16/ISO-IEC-14443-3-B", withTable); return true;
                case "CRCB": algorithm = new Crc16X25("CRC-B", withTable); return true;
                case "CRC16XMODEM": algorithm = new Crc16Xmodem(withTable); return true;
                case "XMODEM": algorithm = new Crc16Xmodem("XMODEM", withTable); return true;
                case "CRC16ZMODEM": algorithm = new Crc16Xmodem("CRC-16/ZMODEM", withTable); return true;
                case "ZMODEM": algorithm = new Crc16Xmodem("ZMODEM", withTable); return true;
                case "CRC16ACORN": algorithm = new Crc16Xmodem("CRC-16/ACORN", withTable); return true;
                case "CRC16LTE": algorithm = new Crc16Xmodem("CRC-16/LTE", withTable); return true;
                case "CRC16V41MSB": algorithm = new Crc16Xmodem("CRC-16/V-41-MSB", withTable); return true;
                case "CRC16XMODEM2": algorithm = new Crc16Xmodem2(withTable); return true;
                case "CRC17CANFD": algorithm = new Crc17CanFd(withTable); return true;
                case "CRC21CANFD": algorithm = new Crc21CanFd(withTable); return true;
                case "CRC24": algorithm = new Crc24(withTable); return true;
                case "CRC24OPENPGP": algorithm = new Crc24("CRC-24/OPENPGP", withTable); return true;
                case "CRC24BLE": algorithm = new Crc24Ble(withTable); return true;
                case "CRC24INTERLAKEN": algorithm = new Crc24Interlaken(withTable); return true;
                case "CRC24FLEXRAYA": algorithm = new Crc24FlexrayA(withTable); return true;
                case "CRC24FLEXRAYB": algorithm = new Crc24FlexrayB(withTable); return true;
                case "CRC24LTEA": algorithm = new Crc24LteA(withTable); return true;
                case "CRC24LTEB": algorithm = new Crc24LteB(withTable); return true;
                case "CRC24OS9": algorithm = new Crc24Os9(withTable); return true;
                case "CRC30CDMA": algorithm = new Crc30Cdma(withTable); return true;
                case "CRC31PHILIPS": algorithm = new Crc31Philips(withTable); return true;
                case "CRC32": algorithm = new Crc32(withTable); return true;
                case "CRC32ISOHDLC": algorithm = new Crc32("CRC-32/ISO-HDLC", withTable); return true;
                case "CRC32V42": algorithm = new Crc32("CRC-32/V-42", withTable); return true;
                case "CRC32XZ": algorithm = new Crc32("CRC-32/XZ", withTable); return true;
                case "PKZIP": algorithm = new Crc32("PKZIP", withTable); return true;
                case "CRC32ADCCP": algorithm = new Crc32("CRC-32/ADCCP", withTable); return true;
                case "CRC32AUTOSAR": algorithm = new Crc32Autosar(withTable); return true;
                case "CRC32C": algorithm = new Crc32c(withTable); return true;
                case "CRC32ISCSI": algorithm = new Crc32c("CRC-32/ISCSI", withTable); return true;
                case "CRC32BASE91C": algorithm = new Crc32c("CRC-32/BASE91-C", withTable); return true;
                case "CRC32CASTAGNOLI": algorithm = new Crc32c("CRC-32/CASTAGNOLI", withTable); return true;
                case "CRC32INTERLAKEN": algorithm = new Crc32c("CRC-32/INTERLAKEN", withTable); return true;
                case "CRC32CDROMEDC": algorithm = new Crc32CdromEdc(withTable); return true;
                case "CRC32D": algorithm = new Crc32d(withTable); return true;
                case "CRC32BASE91D": algorithm = new Crc32d("CRC-32/BASE91-D", withTable); return true;
                case "CRC32BZIP2": algorithm = new Crc32Bzip2(withTable); return true;
                case "CRC32AAL5": algorithm = new Crc32Bzip2("CRC-32/AAL5", withTable); return true;
                case "CRC32DECTB": algorithm = new Crc32Bzip2("CRC-32/DECT-B", withTable); return true;
                case "BCRC32": algorithm = new Crc32Bzip2("B-CRC-32", withTable); return true;
                case "CRC32JAMCRC": algorithm = new Crc32JamCrc(withTable); return true;
                case "JAMCRC": algorithm = new Crc32JamCrc("JAMCRC", withTable); return true;
                case "CRC32KOOPMAN": algorithm = new Crc32Koopman(withTable); return true;
                case "CRC32MEF": algorithm = new Crc32Mef(withTable); return true;
                case "CRC32MPEG2": algorithm = new Crc32Mpeg2(withTable); return true;
                case "CRC32CKSUM": algorithm = new Crc32Cksum(withTable); return true;
                case "CKSUM": algorithm = new Crc32Cksum("CKSUM", withTable); return true;
                case "CRC32POSIX": algorithm = new Crc32Cksum("CRC-32/POSIX", withTable); return true;
                case "CRC32Q": algorithm = new Crc32q(withTable); return true;
                case "CRC32AIXM": algorithm = new Crc32q("CRC-32/AIXM", withTable); return true;
                case "CRC32SATA": algorithm = new Crc32Sata(withTable); return true;
                case "CRC32XFER": algorithm = new Crc32Xfer(withTable); return true;
                case "XFER": algorithm = new Crc32Xfer("XFER", withTable); return true;
                case "CRC40GSM": algorithm = new Crc40Gsm(withTable); return true;
                case "CRC64": algorithm = new Crc64(withTable); return true;
                case "CRC64ECMA182": algorithm = new Crc64("CRC-64/ECMA-182", withTable); return true;
                case "CRC64GOISO": algorithm = new Crc64GoIso(withTable); return true;
                case "CRC64MS": algorithm = new Crc64Ms(withTable); return true;
                case "CRC64REDIS": algorithm = new Crc64Redis(withTable); return true;
                case "CRC64WE": algorithm = new Crc64We(withTable); return true;
                case "CRC64XZ": algorithm = new Crc64Xz(withTable); return true;
                case "CRC64GOECMA": algorithm = new Crc64Xz("CRC-64/GO-ECMA", withTable); return true;
                case "CRC82DARC": algorithm = new Crc82Darc(withTable); return true;
                default: algorithm = null; return false;
            }
        }
    }
}