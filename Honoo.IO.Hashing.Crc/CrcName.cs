using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC name.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:标识符不应包含下划线", Justification = "<挂起>")]
    public sealed class CrcName : IEquatable<CrcName>
    {
        #region Delegate

        internal delegate Crc GetAlgorithmCallback(CrcTableInfo withTable);

        #endregion Delegate

        #region CRC3

        /// <summary></summary>
        public static CrcName CRC3_GSM { get; } = Crc3Gsm.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC3_ROHC { get; } = Crc3Rohc.GetAlgorithmName();

        #endregion CRC3

        #region CRC4

        /// <summary></summary>
        public static CrcName CRC4_G704 { get; } = Crc4Itu.GetAlgorithmName("CRC-4/G-704");

        /// <summary></summary>
        public static CrcName CRC4_INTERLAKEN { get; } = Crc4Interlaken.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC4_ITU { get; } = Crc4Itu.GetAlgorithmName();

        #endregion CRC4

        #region CRC5

        /// <summary></summary>
        public static CrcName CRC5_EPC { get; } = Crc5Epc.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC5_EPC_C1G2 { get; } = Crc5Epc.GetAlgorithmName("CRC-5/EPC-C1G2");

        /// <summary></summary>
        public static CrcName CRC5_G704 { get; } = Crc5Itu.GetAlgorithmName("CRC-5/G-704");

        /// <summary></summary>
        public static CrcName CRC5_ITU { get; } = Crc5Itu.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC5_USB { get; } = Crc5Usb.GetAlgorithmName();

        #endregion CRC5

        #region CRC6

        /// <summary></summary>
        public static CrcName CRC6_CDMA2000A { get; } = Crc6Cdma2000A.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC6_CDMA2000B { get; } = Crc6Cdma2000B.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC6_DARC { get; } = Crc6Darc.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC6_G704 { get; } = Crc6Itu.GetAlgorithmName("CRC-6/G-704");

        /// <summary></summary>
        public static CrcName CRC6_GSM { get; } = Crc6Gsm.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC6_ITU { get; } = Crc6Itu.GetAlgorithmName();

        #endregion CRC6

        #region CRC7

        /// <summary></summary>
        public static CrcName CRC7 { get; } = Crc7.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC7_MMC { get; } = Crc7.GetAlgorithmName("CRC-7/MMC");

        /// <summary></summary>
        public static CrcName CRC7_ROHC { get; } = Crc7Rohc.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC7_UMTS { get; } = Crc7Umts.GetAlgorithmName();

        #endregion CRC7

        #region CRC8

        /// <summary></summary>
        public static CrcName CRC8 { get; } = Crc8.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_AES { get; } = Crc8Ebu.GetAlgorithmName("CRC-8/AES");

        /// <summary></summary>
        public static CrcName CRC8_ATM { get; } = Crc8Itu.GetAlgorithmName("CRC-8/ATM");

        /// <summary></summary>
        public static CrcName CRC8_AUTOSAR { get; } = Crc8Autosar.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_BLUETOOTH { get; } = Crc8Bluetooth.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_CDMA2000 { get; } = Crc8Cdma2000.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_DARC { get; } = Crc8Darc.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_DVB_S2 { get; } = Crc8DvbS2.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_EBU { get; } = Crc8Ebu.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_GSM_A { get; } = Crc8GsmA.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_GSM_B { get; } = Crc8GsmB.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_HITAG { get; } = Crc8Hitag.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_I_432_1 { get; } = Crc8Itu.GetAlgorithmName("CRC-8/I-432-1");

        /// <summary></summary>
        public static CrcName CRC8_I_CODE { get; } = Crc8ICode.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_ITU { get; } = Crc8Itu.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_LTE { get; } = Crc8Lte.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_MAXIM { get; } = Crc8Maxim.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_MAXIM_DOW { get; } = Crc8Maxim.GetAlgorithmName("CRC-8/MAXIM-DOW");

        /// <summary></summary>
        public static CrcName CRC8_MIFARE_MAD { get; } = Crc8MifareMad.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_NRSC_5 { get; } = Crc8Nrsc5.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_OPENSAFETY { get; } = Crc8Opensafety.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_ROHC { get; } = Crc8Rohc.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_SAE_J1850 { get; } = Crc8SaeJ1850.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC8_SMBUS { get; } = Crc8.GetAlgorithmName("CRC-8/SMBUS");

        /// <summary></summary>
        public static CrcName CRC8_TECH_3250 { get; } = Crc8Ebu.GetAlgorithmName("CRC-8/TECH-3250");

        /// <summary></summary>
        public static CrcName CRC8_WCDMA { get; } = Crc8Wcdma.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName DOW_CRC { get; } = Crc8Maxim.GetAlgorithmName("DOW-CRC");

        #endregion CRC8

        #region CRC10

        /// <summary></summary>
        public static CrcName CRC10 { get; } = Crc10.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC10_ATM { get; } = Crc10.GetAlgorithmName("CRC-10/ATM");

        /// <summary></summary>
        public static CrcName CRC10_CDMA2000 { get; } = Crc10Cdma2000.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC10_GSM { get; } = Crc10Gsm.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC10_I_610 { get; } = Crc10.GetAlgorithmName("CRC-10/I-610");

        #endregion CRC10

        #region CRC11

        /// <summary></summary>
        public static CrcName CRC11 { get; } = Crc11.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC11_FLEXRAY { get; } = Crc11.GetAlgorithmName("CRC-11/FLEXRAY");

        /// <summary></summary>
        public static CrcName CRC11_UMTS { get; } = Crc11Umts.GetAlgorithmName();

        #endregion CRC11

        #region CRC12

        /// <summary></summary>
        public static CrcName CRC12_3GPP { get; } = Crc12Umts.GetAlgorithmName("CRC-12/3GPP");

        /// <summary></summary>
        public static CrcName CRC12_CDMA2000 { get; } = Crc12Cdma2000.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC12_DECT { get; } = Crc12Dect.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC12_GSM { get; } = Crc12Gsm.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC12_UMTS { get; } = Crc12Umts.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName XCRC12 { get; } = Crc12Dect.GetAlgorithmName("X-CRC-12");

        #endregion CRC12

        #region CRC13

        /// <summary></summary>
        public static CrcName CRC13_BBC { get; } = Crc13bbc.GetAlgorithmName();

        #endregion CRC13

        #region CRC14

        /// <summary></summary>
        public static CrcName CRC14_DARC { get; } = Crc14Darc.GetAlgorithmName();

        #endregion CRC14

        #region CRC15

        /// <summary></summary>
        public static CrcName CRC14_GSM { get; } = Crc14Gsm.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC15 { get; } = Crc15.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC15_CAN { get; } = Crc15.GetAlgorithmName("CRC-15/CAN");

        /// <summary></summary>
        public static CrcName CRC15_MPT1327 { get; } = Crc15Mpt1327.GetAlgorithmName();

        #endregion CRC15

        #region CRC16

        /// <summary></summary>
        public static CrcName ARC { get; } = Crc16.GetAlgorithmName("ARC");

        /// <summary></summary>
        public static CrcName CRC_B { get; } = Crc16X25.GetAlgorithmName("CRC-B");

        /// <summary></summary>
        public static CrcName CRC_CCITT { get; } = Crc16Ccitt.GetAlgorithmName("CRC-CCITT");

        /// <summary></summary>
        public static CrcName CRC_IBM { get; } = Crc16.GetAlgorithmName("CRC-IBM");

        /// <summary></summary>
        public static CrcName CRC16 { get; } = Crc16.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_ACORN { get; } = Crc16Xmodem.GetAlgorithmName("CRC-16/ACORN");

        /// <summary></summary>
        public static CrcName CRC16_ARC { get; } = Crc16.GetAlgorithmName("CRC-16/ARC");

        /// <summary></summary>
        public static CrcName CRC16_AUG_CCITT { get; } = Crc16SpiFujitsu.GetAlgorithmName("CRC-16/AUG-CCITT");

        /// <summary></summary>
        public static CrcName CRC16_AUTOSAR { get; } = Crc16CcittFalse.GetAlgorithmName("CRC-16/AUTOSAR");

        /// <summary></summary>
        public static CrcName CRC16_BLUETOOTH { get; } = Crc16Ccitt.GetAlgorithmName("CRC-16/BLUETOOTH");

        /// <summary></summary>
        public static CrcName CRC16_BUYPASS { get; } = Crc16Umts.GetAlgorithmName("CRC-16/BUYPASS");

        /// <summary></summary>
        public static CrcName CRC16_CCITT { get; } = Crc16Ccitt.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_CCITT_FALSE { get; } = Crc16CcittFalse.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_CCITT_TRUE { get; } = Crc16Ccitt.GetAlgorithmName("CRC-16/CCITT-TRUE");

        /// <summary></summary>
        public static CrcName CRC16_CDMA2000 { get; } = Crc16Cdma2000.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_CMS { get; } = Crc16Cms.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_DARC { get; } = Crc16Genibus.GetAlgorithmName("CRC-16/DARC");

        /// <summary></summary>
        public static CrcName CRC16_DDS_110 { get; } = Crc16Dds110.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_DECT_R { get; } = Crc16DectR.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_DECT_X { get; } = Crc16DectX.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_DNP { get; } = Crc16Dnp.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_EN13757 { get; } = Crc16En13757.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_EPC { get; } = Crc16Genibus.GetAlgorithmName("CRC-16/EPC");

        /// <summary></summary>
        public static CrcName CRC16_EPC_C1G2 { get; } = Crc16Genibus.GetAlgorithmName("CRC-16/EPC-C1G2");

        /// <summary></summary>
        public static CrcName CRC16_GENIBUS { get; } = Crc16Genibus.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_GSM { get; } = Crc16Gsm.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_I_CODE { get; } = Crc16Genibus.GetAlgorithmName("CRC-16/I-CODE");

        /// <summary></summary>
        public static CrcName CRC16_IBM_3740 { get; } = Crc16CcittFalse.GetAlgorithmName("CRC-16/IBM-3740");

        /// <summary></summary>
        public static CrcName CRC16_IBM_SDLC { get; } = Crc16X25.GetAlgorithmName("CRC-16/IBM-SDLC");

        /// <summary></summary>
        public static CrcName CRC16_IEC_61158_2 { get; } = Crc16Profibus.GetAlgorithmName("CRC-16/IEC-61158-2");

        /// <summary></summary>
        public static CrcName CRC16_ISO_HDLC { get; } = Crc16X25.GetAlgorithmName("CRC-16/ISO-HDLC");

        /// <summary></summary>
        public static CrcName CRC16_ISO_IEC_14443_3_A { get; } = CrcA.GetAlgorithmName("CRC-16/ISO-IEC-14443-3-A");

        /// <summary></summary>
        public static CrcName CRC16_ISO_IEC_14443_3_B { get; } = Crc16X25.GetAlgorithmName("CRC-16/ISO-IEC-14443-3-B");

        /// <summary></summary>
        public static CrcName CRC16_KERMIT { get; } = Crc16Ccitt.GetAlgorithmName("CRC-16/KERMIT");

        /// <summary></summary>
        public static CrcName CRC16_LHA { get; } = Crc16.GetAlgorithmName("CRC-16/LHA");

        /// <summary></summary>
        public static CrcName CRC16_LJ1200 { get; } = Crc16Lj1200.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_LTE { get; } = Crc16Xmodem.GetAlgorithmName("CRC-16/LTE");

        /// <summary></summary>
        public static CrcName CRC16_M17 { get; } = Crc16M17.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_MAXIM { get; } = Crc16Maxim.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_MAXIM_DOW { get; } = Crc16Maxim.GetAlgorithmName("CRC-16/MAXIM-DOW");

        /// <summary></summary>
        public static CrcName CRC16_MCRF4XX { get; } = Crc16Mcrf4XX.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_MODBUS { get; } = Crc16Modbus.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_NRSC_5 { get; } = Crc16Nrsc5.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_OPENSAFETY_A { get; } = Crc16OpensafetyA.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_OPENSAFETY_B { get; } = Crc16OpensafetyB.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_PROFIBUS { get; } = Crc16Profibus.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_RIELLO { get; } = Crc16Riello.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_SPI_FUJITSU { get; } = Crc16SpiFujitsu.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_T10_DIF { get; } = Crc16T10Dif.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_TELEDISK { get; } = Crc16Teledisk.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_TMS37157 { get; } = Crc16Tms37157.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_UMTS { get; } = Crc16Umts.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_USB { get; } = Crc16Usb.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_V_41_LSB { get; } = Crc16Ccitt.GetAlgorithmName("CRC-16/V-41-LSB");

        /// <summary></summary>
        public static CrcName CRC16_V_41_MSB { get; } = Crc16Xmodem.GetAlgorithmName("CRC-16/V-41-MSB");

        /// <summary></summary>
        public static CrcName CRC16_VERIFONE { get; } = Crc16Umts.GetAlgorithmName("CRC-16/VERIFONE");

        /// <summary></summary>
        public static CrcName CRC16_X_25 { get; } = Crc16X25.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_XMODEM { get; } = Crc16Xmodem.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_XMODEM2 { get; } = Crc16Xmodem2.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC16_ZMODEM { get; } = Crc16Xmodem.GetAlgorithmName("CRC-16/ZMODEM");

        /// <summary></summary>
        public static CrcName CRCA { get; } = CrcA.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName KERMIT { get; } = Crc16Ccitt.GetAlgorithmName("KERMIT");

        /// <summary></summary>
        public static CrcName MODBUS { get; } = Crc16Modbus.GetAlgorithmName("MODBUS");

        /// <summary></summary>
        public static CrcName R_CRC16 { get; } = Crc16DectR.GetAlgorithmName("R-CRC-16");

        /// <summary></summary>
        public static CrcName X_25 { get; } = Crc16X25.GetAlgorithmName("X-25");

        /// <summary></summary>
        public static CrcName X_CRC16 { get; } = Crc16DectX.GetAlgorithmName("X-CRC-16");

        /// <summary></summary>
        public static CrcName XMODEM { get; } = Crc16Xmodem.GetAlgorithmName("XMODEM");

        /// <summary></summary>
        public static CrcName ZMODEM { get; } = Crc16Xmodem.GetAlgorithmName("ZMODEM");

        #endregion CRC16

        #region CRC17

        /// <summary></summary>
        public static CrcName CRC17_CAN_FD { get; } = Crc17CanFd.GetAlgorithmName();

        #endregion CRC17

        #region CRC21

        /// <summary></summary>
        public static CrcName CRC21_CAN_FD { get; } = Crc21CanFd.GetAlgorithmName();

        #endregion CRC21

        #region CRC24

        /// <summary></summary>
        public static CrcName CRC24 { get; } = Crc24.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC24_BLE { get; } = Crc24Ble.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC24_FLEXRAY_A { get; } = Crc24FlexrayA.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC24_FLEXRAY_B { get; } = Crc24FlexrayB.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC24_INTERLAKEN { get; } = Crc24Interlaken.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC24_LTE_A { get; } = Crc24LteA.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC24_LTE_B { get; } = Crc24LteB.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC24_OPENPGP { get; } = Crc24.GetAlgorithmName("CRC-24/OPENPGP");

        /// <summary></summary>
        public static CrcName CRC24_OS_9 { get; } = Crc24Os9.GetAlgorithmName();

        #endregion CRC24

        #region CRC30

        /// <summary></summary>
        public static CrcName CRC30_CDMA { get; } = Crc30Cdma.GetAlgorithmName();

        #endregion CRC30

        #region CRC31

        /// <summary></summary>
        public static CrcName CRC31_PHILIPS { get; } = Crc31Philips.GetAlgorithmName();

        #endregion CRC31

        #region CRC32

        /// <summary></summary>
        public static CrcName B_CRC32 { get; } = Crc32Bzip2.GetAlgorithmName("B-CRC-32");

        /// <summary></summary>
        public static CrcName CKSUM { get; } = Crc32Cksum.GetAlgorithmName("CKSUM");

        /// <summary></summary>
        public static CrcName CRC32 { get; } = Crc32.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_AAL5 { get; } = Crc32Bzip2.GetAlgorithmName("CRC-32/AAL5");

        /// <summary></summary>
        public static CrcName CRC32_ADCCP { get; } = Crc32.GetAlgorithmName("CRC-32/ADCCP");

        /// <summary></summary>
        public static CrcName CRC32_AIXM { get; } = Crc32q.GetAlgorithmName("CRC-32/AIXM");

        /// <summary></summary>
        public static CrcName CRC32_AUTOSAR { get; } = Crc32Autosar.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_BASE91_C { get; } = Crc32c.GetAlgorithmName("CRC-32/BASE91-C");

        /// <summary></summary>
        public static CrcName CRC32_BASE91_D { get; } = Crc32d.GetAlgorithmName("CRC-32/BASE91-D");

        /// <summary></summary>
        public static CrcName CRC32_BZIP2 { get; } = Crc32Bzip2.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_CASTAGNOLI { get; } = Crc32c.GetAlgorithmName("CRC-32/CASTAGNOLI");

        /// <summary></summary>
        public static CrcName CRC32_CD_ROM_EDC { get; } = Crc32CdromEdc.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_CKSUM { get; } = Crc32Cksum.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_DECT_B { get; } = Crc32Bzip2.GetAlgorithmName("CRC-32/DECT-B");

        /// <summary></summary>
        public static CrcName CRC32_INTERLAKEN { get; } = Crc32c.GetAlgorithmName("CRC-32/INTERLAKEN");

        /// <summary></summary>
        public static CrcName CRC32_ISCSI { get; } = Crc32c.GetAlgorithmName("CRC-32/ISCSI");

        /// <summary></summary>
        public static CrcName CRC32_ISO_HDLC { get; } = Crc32.GetAlgorithmName("CRC-32/ISO-HDLC");

        /// <summary></summary>
        public static CrcName CRC32_JAMCRC { get; } = Crc32JamCrc.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_KOOPMAN { get; } = Crc32Koopman.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_MEF { get; } = Crc32Mef.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_MPEG_2 { get; } = Crc32Mpeg2.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_POSIX { get; } = Crc32Cksum.GetAlgorithmName("CRC-32/POSIX");

        /// <summary></summary>
        public static CrcName CRC32_SATA { get; } = Crc32Sata.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_V_42 { get; } = Crc32.GetAlgorithmName("CRC-32/V-42");

        /// <summary></summary>
        public static CrcName CRC32_XFER { get; } = Crc32Xfer.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32_XZ { get; } = Crc32.GetAlgorithmName("CRC-32/XZ");

        /// <summary></summary>
        public static CrcName CRC32C { get; } = Crc32c.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32D { get; } = Crc32d.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC32Q { get; } = Crc32q.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName JAMCRC { get; } = Crc32JamCrc.GetAlgorithmName("JAMCRC");

        /// <summary></summary>
        public static CrcName PKZIP { get; } = Crc32.GetAlgorithmName("PKZIP");

        /// <summary></summary>
        public static CrcName XFER { get; } = Crc32Xfer.GetAlgorithmName("XFER");

        #endregion CRC32

        #region CRC40

        /// <summary></summary>
        public static CrcName CRC40_GSM { get; } = Crc40Gsm.GetAlgorithmName();

        #endregion CRC40

        #region CRC64

        /// <summary></summary>
        public static CrcName CRC64 { get; } = Crc64.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC64_ECMA_182 { get; } = Crc64.GetAlgorithmName("CRC-64/ECMA-182");

        /// <summary></summary>
        public static CrcName CRC64_GO_ECMA { get; } = Crc64Xz.GetAlgorithmName("CRC-64/GO-ECMA");

        /// <summary></summary>
        public static CrcName CRC64_GO_ISO { get; } = Crc64GoIso.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC64_MS { get; } = Crc64Ms.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC64_REDIS { get; } = Crc64Redis.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC64_WE { get; } = Crc64We.GetAlgorithmName();

        /// <summary></summary>
        public static CrcName CRC64_XZ { get; } = Crc64Xz.GetAlgorithmName();

        #endregion CRC64

        #region CRC82

        /// <summary></summary>
        public static CrcName CRC82_DARC { get; } = Crc82Darc.GetAlgorithmName();

        #endregion CRC82

        #region Members

        private readonly GetAlgorithmCallback _getAlgorithm;
        private readonly CrcParameter _init;
        private readonly string _name;
        private readonly CrcParameter _poly;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly int _width;
        private readonly CrcParameter _xorout;

        /// <summary>
        /// Gets poly value.
        /// </summary>
        public CrcParameter Init => _init;

        /// <summary>
        /// Gets this algorithm's name.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets poly value.
        /// </summary>
        public CrcParameter Poly => _poly;

        /// <summary>
        /// Gets refin value.
        /// </summary>
        public bool Refin => _refin;

        /// <summary>
        /// Gets refout value.
        /// </summary>
        public bool Refout => _refout;

        /// <summary>
        /// Gets crc width in bits.
        /// </summary>
        public int Width => _width;

        /// <summary>
        /// Gets poly value.
        /// </summary>
        public CrcParameter Xorout => _xorout;

        internal GetAlgorithmCallback GetAlgorithm => _getAlgorithm;

        #endregion Members

        #region Construction

        internal CrcName(string name, int width, bool refin, bool refout, CrcParameter poly, CrcParameter init, CrcParameter xorout, GetAlgorithmCallback getAlgorithm)
        {
            _name = name;
            _width = width;
            _refin = refin;
            _refout = refout;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            _getAlgorithm = getAlgorithm;
        }

        #endregion Construction

        /// <summary>
        /// Gets all algorithm names of the storage.
        /// </summary>
        /// <returns></returns>
        public static CrcName[] GetNames()
        {
            return new CrcName[]
            {
                CRC3_GSM,
                CRC3_ROHC,

                CRC4_G704,
                CRC4_INTERLAKEN,
                CRC4_ITU,

                CRC5_EPC,
                CRC5_EPC_C1G2,
                CRC5_G704,
                CRC5_ITU,
                CRC5_USB,

                CRC6_CDMA2000A,
                CRC6_CDMA2000B,
                CRC6_DARC,
                CRC6_G704,
                CRC6_GSM,
                CRC6_ITU,

                CRC7,
                CRC7_MMC,
                CRC7_ROHC,
                CRC7_UMTS,

                CRC8,
                CRC8_AES,
                CRC8_ATM,
                CRC8_AUTOSAR,
                CRC8_BLUETOOTH,
                CRC8_CDMA2000,
                CRC8_DARC,
                CRC8_DVB_S2,
                CRC8_EBU,
                CRC8_GSM_A,
                CRC8_GSM_B,
                CRC8_HITAG,
                CRC8_I_432_1,
                CRC8_I_CODE,
                CRC8_ITU,
                CRC8_LTE,
                CRC8_MAXIM,
                CRC8_MAXIM_DOW,
                CRC8_MIFARE_MAD,
                CRC8_NRSC_5,
                CRC8_OPENSAFETY,
                CRC8_ROHC,
                CRC8_SAE_J1850,
                CRC8_SMBUS,
                CRC8_TECH_3250,
                CRC8_WCDMA,
                DOW_CRC,

                CRC10,
                CRC10_ATM,
                CRC10_CDMA2000,
                CRC10_GSM,
                CRC10_I_610,

                CRC11,
                CRC11_FLEXRAY,
                CRC11_UMTS,

                CRC12_3GPP,
                CRC12_CDMA2000,
                CRC12_DECT,
                CRC12_GSM,
                CRC12_UMTS,
                XCRC12,

                CRC13_BBC,

                CRC14_DARC,
                CRC14_GSM,

                CRC15,
                CRC15_CAN,
                CRC15_MPT1327,

                ARC,
                CRC_B,
                CRC_CCITT,
                CRC_IBM,
                CRC16,
                CRC16_ACORN,
                CRC16_ARC,
                CRC16_AUG_CCITT,
                CRC16_AUTOSAR,
                CRC16_BLUETOOTH,
                CRC16_BUYPASS,
                CRC16_CCITT,
                CRC16_CCITT_FALSE,
                CRC16_CCITT_TRUE,
                CRC16_CDMA2000,
                CRC16_CMS,
                CRC16_DARC,
                CRC16_DDS_110,
                CRC16_DECT_R,
                CRC16_DECT_X,
                CRC16_DNP,
                CRC16_EN13757,
                CRC16_EPC,
                CRC16_EPC_C1G2,
                CRC16_GENIBUS,
                CRC16_GSM,
                CRC16_I_CODE,
                CRC16_IBM_3740,
                CRC16_IBM_SDLC,
                CRC16_IEC_61158_2,
                CRC16_ISO_HDLC,
                CRC16_ISO_IEC_14443_3_A,
                CRC16_ISO_IEC_14443_3_B,
                CRC16_KERMIT,
                CRC16_LHA,
                CRC16_LJ1200,
                CRC16_LTE,
                CRC16_M17,
                CRC16_MAXIM,
                CRC16_MAXIM_DOW,
                CRC16_MCRF4XX,
                CRC16_MODBUS,
                CRC16_NRSC_5,
                CRC16_OPENSAFETY_A,
                CRC16_OPENSAFETY_B,
                CRC16_PROFIBUS,
                CRC16_RIELLO,
                CRC16_SPI_FUJITSU,
                CRC16_T10_DIF,
                CRC16_TELEDISK,
                CRC16_TMS37157,
                CRC16_UMTS,
                CRC16_USB,
                CRC16_V_41_LSB,
                CRC16_V_41_MSB,
                CRC16_VERIFONE,
                CRC16_X_25,
                CRC16_XMODEM,
                CRC16_XMODEM2,
                CRC16_ZMODEM,
                CRCA,
                KERMIT,
                MODBUS,
                R_CRC16,
                X_25,
                X_CRC16,
                XMODEM,
                ZMODEM,

                CRC17_CAN_FD,

                CRC21_CAN_FD,

                CRC24,
                CRC24_BLE,
                CRC24_FLEXRAY_A,
                CRC24_FLEXRAY_B,
                CRC24_INTERLAKEN,
                CRC24_LTE_A,
                CRC24_LTE_B,
                CRC24_OPENPGP,
                CRC24_OS_9,

                CRC30_CDMA,

                CRC31_PHILIPS,

                B_CRC32,
                CKSUM,
                CRC32,
                CRC32_AAL5,
                CRC32_ADCCP,
                CRC32_AIXM,
                CRC32_AUTOSAR,
                CRC32_BASE91_C,
                CRC32_BASE91_D,
                CRC32_BZIP2,
                CRC32_CASTAGNOLI,
                CRC32_CD_ROM_EDC,
                CRC32_CKSUM,
                CRC32_DECT_B,
                CRC32_INTERLAKEN,
                CRC32_ISCSI,
                CRC32_ISO_HDLC,
                CRC32_JAMCRC,
                CRC32_KOOPMAN,
                CRC32_MEF,
                CRC32_MPEG_2,
                CRC32_POSIX,
                CRC32_SATA,
                CRC32_V_42,
                CRC32_XFER,
                CRC32_XZ,
                CRC32C,
                CRC32D,
                CRC32Q,
                JAMCRC,
                PKZIP,
                XFER,

                CRC40_GSM,

                CRC64,
                CRC64_ECMA_182,
                CRC64_GO_ECMA,
                CRC64_GO_ISO,
                CRC64_MS,
                CRC64_REDIS,
                CRC64_WE,
                CRC64_XZ,

                CRC82_DARC,
            };
        }

        /// <summary>
        /// Try get algorithm name from mechanism.
        /// </summary>
        /// <param name="mechanism">Algorithm mechanism.</param>
        /// <param name="algorithmName">Algorithm name.</param>
        /// <returns></returns>
        public static bool TryGetAlgorithmName(string mechanism, out CrcName algorithmName)
        {
            if (string.IsNullOrWhiteSpace(mechanism))
            {
                algorithmName = null;
                return false;
            }
            mechanism = mechanism.Trim().Replace("-", null).Replace("_", null).Replace("/", null).ToUpperInvariant();
            switch (mechanism)
            {
                case "CRC3GSM": algorithmName = CRC3_GSM; return true;
                case "CRC3ROHC": algorithmName = CRC3_ROHC; return true;

                case "CRC4ITU": algorithmName = CRC4_ITU; return true;
                case "CRC4G704": algorithmName = CRC4_G704; return true;
                case "CRC4INTERLAKEN": algorithmName = CRC4_INTERLAKEN; return true;

                case "CRC5EPC": algorithmName = CRC5_EPC; return true;
                case "CRC5EPCC1G2": algorithmName = CRC5_EPC_C1G2; return true;
                case "CRC5ITU": algorithmName = CRC5_ITU; return true;
                case "CRC5G704": algorithmName = CRC5_G704; return true;
                case "CRC5USB": algorithmName = CRC5_USB; return true;

                case "CRC6CDMA2000A": algorithmName = CRC6_CDMA2000A; return true;
                case "CRC6CDMA2000B": algorithmName = CRC6_CDMA2000B; return true;
                case "CRC6DARC": algorithmName = CRC6_DARC; return true;
                case "CRC6GSM": algorithmName = CRC6_GSM; return true;
                case "CRC6ITU": algorithmName = CRC6_ITU; return true;
                case "CRC6G704": algorithmName = CRC6_G704; return true;

                case "CRC7": algorithmName = CRC7; return true;
                case "CRC7MMC": algorithmName = CRC7_MMC; return true;
                case "CRC7ROHC": algorithmName = CRC7_ROHC; return true;
                case "CRC7UMTS": algorithmName = CRC7_UMTS; return true;

                case "CRC8": algorithmName = CRC8; return true;
                case "CRC8SMBUS": algorithmName = CRC8_SMBUS; return true;
                case "CRC8OPENSAFETY": algorithmName = CRC8_OPENSAFETY; return true;
                case "CRC8SAEJ1850": algorithmName = CRC8_SAE_J1850; return true;
                case "CRC8WCDMA": algorithmName = CRC8_WCDMA; return true;
                case "CRC8BLUETOOTH": algorithmName = CRC8_BLUETOOTH; return true;
                case "CRC8AUTOSAR": algorithmName = CRC8_AUTOSAR; return true;
                case "CRC8CDMA2000": algorithmName = CRC8_CDMA2000; return true;
                case "CRC8NRSC5": algorithmName = CRC8_NRSC_5; return true;
                case "CRC8LTE": algorithmName = CRC8_LTE; return true;
                case "CRC8DARC": algorithmName = CRC8_DARC; return true;
                case "CRC8DVBS2": algorithmName = CRC8_DVB_S2; return true;
                case "CRC8HITAG": algorithmName = CRC8_HITAG; return true;
                case "CRC8GSMA": algorithmName = CRC8_GSM_A; return true;
                case "CRC8GSMB": algorithmName = CRC8_GSM_B; return true;
                case "CRC8EBU": algorithmName = CRC8_EBU; return true;
                case "CRC8TECH3250": algorithmName = CRC8_TECH_3250; return true;
                case "CRC8AES": algorithmName = CRC8_AES; return true;
                case "CRC8ICODE": algorithmName = CRC8_I_CODE; return true;
                case "CRC8MIFAREMAD": algorithmName = CRC8_MIFARE_MAD; return true;
                case "CRC8ITU": algorithmName = CRC8_ITU; return true;
                case "CRC8I4321": algorithmName = CRC8_I_432_1; return true;
                case "CRC8ATM": algorithmName = CRC8_ATM; return true;
                case "CRC8MAXIM": algorithmName = CRC8_MAXIM; return true;
                case "CRC8MAXIMDOW": algorithmName = CRC8_MAXIM_DOW; return true;
                case "DOWCRC": algorithmName = DOW_CRC; return true;
                case "CRC8ROHC": algorithmName = CRC8_ROHC; return true;

                case "CRC10": algorithmName = CRC10; return true;
                case "CRC10ATM": algorithmName = CRC10_ATM; return true;
                case "CRC10I610": algorithmName = CRC10_I_610; return true;
                case "CRC10CDMA2000": algorithmName = CRC10_CDMA2000; return true;
                case "CRC10GSM": algorithmName = CRC10_GSM; return true;

                case "CRC11": algorithmName = CRC11; return true;
                case "CRC11FLEXRAY": algorithmName = CRC11_FLEXRAY; return true;
                case "CRC11UMTS": algorithmName = CRC11_UMTS; return true;

                case "CRC12CDMA2000": algorithmName = CRC12_CDMA2000; return true;
                case "CRC12DECT": algorithmName = CRC12_DECT; return true;
                case "XCRC12": algorithmName = XCRC12; return true;
                case "CRC12GSM": algorithmName = CRC12_GSM; return true;
                case "CRC12UMTS": algorithmName = CRC12_UMTS; return true;
                case "CRC123GPP": algorithmName = CRC12_3GPP; return true;

                case "CRC13BBC": algorithmName = CRC13_BBC; return true;

                case "CRC14DARC": algorithmName = CRC14_DARC; return true;
                case "CRC14GSM": algorithmName = CRC14_GSM; return true;

                case "CRC15": algorithmName = CRC15; return true;
                case "CRC15CAN": algorithmName = CRC15_CAN; return true;
                case "CRC15MPT1327": algorithmName = CRC15_MPT1327; return true;

                case "CRC16": algorithmName = CRC16; return true;
                case "CRC16ARC": algorithmName = CRC16_ARC; return true;
                case "ARC": algorithmName = ARC; return true;
                case "CRC16LHA": algorithmName = CRC16_LHA; return true;
                case "CRCIBM": algorithmName = CRC_IBM; return true;
                case "CRCA": algorithmName = CRCA; return true;
                case "CRC16CMS": algorithmName = CRC16_CMS; return true;
                case "CRC16ISOIEC144433A": algorithmName = CRC16_ISO_IEC_14443_3_A; return true;
                case "CRC16SPIFUJITSU": algorithmName = CRC16_SPI_FUJITSU; return true;
                case "CRC16AUGCCITT": algorithmName = CRC16_AUG_CCITT; return true;
                case "CRC16UMTS": algorithmName = CRC16_UMTS; return true;
                case "CRC16BUYPASS": algorithmName = CRC16_BUYPASS; return true;
                case "CRC16VERIFONE": algorithmName = CRC16_VERIFONE; return true;
                case "CRC16CCITT": algorithmName = CRC16_CCITT; return true;
                case "CRCCCITT": algorithmName = CRC_CCITT; return true;
                case "CRC16CCITTTRUE": algorithmName = CRC16_CCITT_TRUE; return true;
                case "CRC16KERMIT": algorithmName = CRC16_KERMIT; return true;
                case "KERMIT": algorithmName = KERMIT; return true;
                case "CRC16BLUETOOTH": algorithmName = CRC16_BLUETOOTH; return true;
                case "CRC16V41LSB": algorithmName = CRC16_V_41_LSB; return true;
                case "CRC16CCITTFALSE": algorithmName = CRC16_CCITT_FALSE; return true;
                case "CRC16IBM3740": algorithmName = CRC16_IBM_3740; return true;
                case "CRC16AUTOSAR": algorithmName = CRC16_AUTOSAR; return true;
                case "CRC16CDMA2000": algorithmName = CRC16_CDMA2000; return true;
                case "CRC16DECTR": algorithmName = CRC16_DECT_R; return true;
                case "RCRC16": algorithmName = R_CRC16; return true;
                case "CRC16DECTX": algorithmName = CRC16_DECT_X; return true;
                case "XCRC16": algorithmName = X_CRC16; return true;
                case "CRC16DNP": algorithmName = CRC16_DNP; return true;
                case "CRC16DDS110": algorithmName = CRC16_DDS_110; return true;
                case "CRC16EN13757": algorithmName = CRC16_EN13757; return true;
                case "CRC16GENIBUS": algorithmName = CRC16_GENIBUS; return true;
                case "CRC16DARC": algorithmName = CRC16_DARC; return true;
                case "CRC16EPC": algorithmName = CRC16_EPC; return true;
                case "CRC16EPCC1G2": algorithmName = CRC16_EPC_C1G2; return true;
                case "CRC16ICODE": algorithmName = CRC16_I_CODE; return true;
                case "CRC16M17": algorithmName = CRC16_M17; return true;
                case "CRC16GSM": algorithmName = CRC16_GSM; return true;
                case "CRC16LJ1200": algorithmName = CRC16_LJ1200; return true;
                case "CRC16MAXIM": algorithmName = CRC16_MAXIM; return true;
                case "CRC16MAXIMDOW": algorithmName = CRC16_MAXIM_DOW; return true;
                case "CRC16MCRF4XX": algorithmName = CRC16_MCRF4XX; return true;
                case "CRC16MODBUS": algorithmName = CRC16_MODBUS; return true;
                case "MODBUS": algorithmName = MODBUS; return true;
                case "CRC16PROFIBUS": algorithmName = CRC16_PROFIBUS; return true;
                case "CRC16IEC611582": algorithmName = CRC16_IEC_61158_2; return true;
                case "CRC16NRSC5": algorithmName = CRC16_NRSC_5; return true;
                case "CRC16OPENSAFETYA": algorithmName = CRC16_OPENSAFETY_A; return true;
                case "CRC16OPENSAFETYB": algorithmName = CRC16_OPENSAFETY_B; return true;
                case "CRC16RIELLO": algorithmName = CRC16_RIELLO; return true;
                case "CRC16T10DIF": algorithmName = CRC16_T10_DIF; return true;
                case "CRC16TELEDISK": algorithmName = CRC16_TELEDISK; return true;
                case "CRC16TMS37157": algorithmName = CRC16_TMS37157; return true;
                case "CRC16USB": algorithmName = CRC16_USB; return true;
                case "CRC16X25": algorithmName = CRC16_X_25; return true;
                case "X25": algorithmName = X_25; return true;
                case "CRC16IBMSDLC": algorithmName = CRC16_IBM_SDLC; return true;
                case "CRC16ISOHDLC": algorithmName = CRC16_ISO_HDLC; return true;
                case "CRC16ISOIEC144433B": algorithmName = CRC16_ISO_IEC_14443_3_B; return true;
                case "CRCB": algorithmName = CRC_B; return true;
                case "CRC16XMODEM": algorithmName = CRC16_XMODEM; return true;
                case "XMODEM": algorithmName = XMODEM; return true;
                case "CRC16ZMODEM": algorithmName = CRC16_ZMODEM; return true;
                case "ZMODEM": algorithmName = ZMODEM; return true;
                case "CRC16ACORN": algorithmName = CRC16_ACORN; return true;
                case "CRC16LTE": algorithmName = CRC16_LTE; return true;
                case "CRC16V41MSB": algorithmName = CRC16_V_41_MSB; return true;
                case "CRC16XMODEM2": algorithmName = CRC16_XMODEM2; return true;

                case "CRC17CANFD": algorithmName = CRC17_CAN_FD; return true;

                case "CRC21CANFD": algorithmName = CRC21_CAN_FD; return true;

                case "CRC24": algorithmName = CRC24; return true;
                case "CRC24OPENPGP": algorithmName = CRC24_OPENPGP; return true;
                case "CRC24BLE": algorithmName = CRC24_BLE; return true;
                case "CRC24INTERLAKEN": algorithmName = CRC24_INTERLAKEN; return true;
                case "CRC24FLEXRAYA": algorithmName = CRC24_FLEXRAY_A; return true;
                case "CRC24FLEXRAYB": algorithmName = CRC24_FLEXRAY_B; return true;
                case "CRC24LTEA": algorithmName = CRC24_LTE_A; return true;
                case "CRC24LTEB": algorithmName = CRC24_LTE_B; return true;
                case "CRC24OS9": algorithmName = CRC24_OS_9; return true;

                case "CRC30CDMA": algorithmName = CRC30_CDMA; return true;

                case "CRC31PHILIPS": algorithmName = CRC31_PHILIPS; return true;

                case "CRC32": algorithmName = CRC32; return true;
                case "CRC32ISOHDLC": algorithmName = CRC32_ISO_HDLC; return true;
                case "CRC32V42": algorithmName = CRC32_V_42; return true;
                case "CRC32XZ": algorithmName = CRC32_XZ; return true;
                case "PKZIP": algorithmName = PKZIP; return true;
                case "CRC32ADCCP": algorithmName = CRC32_ADCCP; return true;
                case "CRC32AUTOSAR": algorithmName = CRC32_AUTOSAR; return true;
                case "CRC32C": algorithmName = CRC32C; return true;
                case "CRC32ISCSI": algorithmName = CRC32_ISCSI; return true;
                case "CRC32BASE91C": algorithmName = CRC32_BASE91_C; return true;
                case "CRC32CASTAGNOLI": algorithmName = CRC32_CASTAGNOLI; return true;
                case "CRC32INTERLAKEN": algorithmName = CRC32_INTERLAKEN; return true;
                case "CRC32CDROMEDC": algorithmName = CRC32_CD_ROM_EDC; return true;
                case "CRC32D": algorithmName = CRC32D; return true;
                case "CRC32BASE91D": algorithmName = CRC32_BASE91_D; return true;
                case "CRC32BZIP2": algorithmName = CRC32_BZIP2; return true;
                case "CRC32AAL5": algorithmName = CRC32_AAL5; return true;
                case "CRC32DECTB": algorithmName = CRC32_DECT_B; return true;
                case "BCRC32": algorithmName = B_CRC32; return true;
                case "CRC32JAMCRC": algorithmName = CRC32_JAMCRC; return true;
                case "JAMCRC": algorithmName = JAMCRC; return true;
                case "CRC32KOOPMAN": algorithmName = CRC32_KOOPMAN; return true;
                case "CRC32MEF": algorithmName = CRC32_MEF; return true;
                case "CRC32MPEG2": algorithmName = CRC32_MPEG_2; return true;
                case "CRC32CKSUM": algorithmName = CRC32_CKSUM; return true;
                case "CKSUM": algorithmName = CKSUM; return true;
                case "CRC32POSIX": algorithmName = CRC32_POSIX; return true;
                case "CRC32Q": algorithmName = CRC32Q; return true;
                case "CRC32AIXM": algorithmName = CRC32_AIXM; return true;
                case "CRC32SATA": algorithmName = CRC32_SATA; return true;
                case "CRC32XFER": algorithmName = CRC32_XFER; return true;
                case "XFER": algorithmName = XFER; return true;

                case "CRC40GSM": algorithmName = CRC40_GSM; return true;

                case "CRC64": algorithmName = CRC64; return true;
                case "CRC64ECMA182": algorithmName = CRC64_ECMA_182; return true;
                case "CRC64GOISO": algorithmName = CRC64_GO_ISO; return true;
                case "CRC64MS": algorithmName = CRC64_MS; return true;
                case "CRC64REDIS": algorithmName = CRC64_REDIS; return true;
                case "CRC64WE": algorithmName = CRC64_WE; return true;
                case "CRC64XZ": algorithmName = CRC64_XZ; return true;
                case "CRC64GOECMA": algorithmName = CRC64_GO_ECMA; return true;

                case "CRC82DARC": algorithmName = CRC82_DARC; return true;
                default: algorithmName = null; return false;
            }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CrcName other)
        {
            return other != null && string.Equals(_name, other._name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is CrcName other && Equals(other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Return algorithm name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }
    }
}