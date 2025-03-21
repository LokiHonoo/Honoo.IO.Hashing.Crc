# Honoo.IO.Hashing.Crc

<!-- @import "[TOC]" {cmd="toc" depthFrom=1 depthTo=6 orderedList=false} -->

<!-- code_chunk_output -->

- [Honoo.IO.Hashing.Crc](#honooiohashingcrc)
  - [INTRODUCTION](#introduction)
  - [GUIDE](#guide)
    - [NuGet](#nuget)
  - [USAGE](#usage)
  - [SPEED](#speed)
  - [LICENSE](#license)

<!-- /code_chunk_output -->

## INTRODUCTION

CRC implemenations.

CRC name of catalogue(112) are supported.

Custom width and parameters are supported.

Catalogue of parametrised CRC algorithms: <https://reveng.sourceforge.io/crc-catalogue/all.htm> .

Catalogue of supported CRC algorithms:

CRC-3/GSM,
CRC-3/ROHC,
CRC-4/G-704, CRC-4/ITU,
CRC-4/INTERLAKEN,
CRC-5/EPC-C1G2, CRC-5/EPC,
CRC-5/G-704, CRC-5/ITU,
CRC-5/USB,
CRC-6/CDMA2000-A,
CRC-6/CDMA2000-B,
CRC-6/DARC,
CRC-6/G-704, CRC-6/ITU,
CRC-6/GSM,
CRC-7/MMC, CRC-7,
CRC-7/ROHC,
CRC-7/UMTS,
CRC-8/AUTOSAR,
CRC-8/BLUETOOTH,
CRC-8/CDMA2000,
CRC-8/DARC,
CRC-8/DVB-S2,
CRC-8/GSM-A,
CRC-8/GSM-B,
CRC-8/HITAG,
CRC-8/I-432-1, CRC-8/ITU,
CRC-8/I-CODE,
CRC-8/LTE,
CRC-8/MAXIM-DOW, CRC-8/MAXIM, DOW-CRC,
CRC-8/MIFARE-MAD,
CRC-8/NRSC-5,
CRC-8/OPENSAFETY,
CRC-8/ROHC,
CRC-8/SAE-J1850,
CRC-8/SMBUS, CRC-8,
CRC-8/TECH-3250, CRC-8/AES, CRC-8/EBU,
CRC-8/WCDMA,
CRC-10/ATM, CRC-10, CRC-10/I-610,
CRC-10/CDMA2000,
CRC-10/GSM,
CRC-11/FLEXRAY, CRC-11,
CRC-11/UMTS,
CRC-12/CDMA2000,
CRC-12/DECT, X-CRC-12,
CRC-12/GSM,
CRC-12/UMTS, CRC-12/3GPP,
CRC-13/BBC,
CRC-14/DARC,
CRC-14/GSM,
CRC-15/CAN, CRC-15,
CRC-15/MPT1327,
CRC-16/ARC, ARC, CRC-16, CRC-16/LHA, CRC-IBM,
CRC-16/CDMA2000,
CRC-16/CMS,
CRC-16/DDS-110,
CRC-16/DECT-R, R-CRC-16,
CRC-16/DECT-X, X-CRC-16,
CRC-16/DNP,
CRC-16/EN-13757,
CRC-16/GENIBUS, CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE,
CRC-16/GSM,
CRC-16/IBM-3740, CRC-16/AUTOSAR, CRC-16/CCITT-FALSE,
CRC-16/IBM-SDLC, CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25,
CRC-16/ISO-IEC-14443-3-A, CRC-A,
CRC-16/KERMIT, CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT,
CRC-16/LJ1200,
CRC-16/M17,
CRC-16/MAXIM-DOW, CRC-16/MAXIM,
CRC-16/MCRF4XX,
CRC-16/MODBUS, MODBUS,
CRC-16/NRSC-5,
CRC-16/OPENSAFETY-A,
CRC-16/OPENSAFETY-B,
CRC-16/PROFIBUS, CRC-16/IEC-61158-2,
CRC-16/RIELLO,
CRC-16/SPI-FUJITSU, CRC-16/AUG-CCITT,
CRC-16/T10-DIF,
CRC-16/TELEDISK,
CRC-16/TMS37157,
CRC-16/UMTS, CRC-16/BUYPASS, CRC-16/VERIFONE,
CRC-16/USB,
CRC-16/XMODEM, CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM,
CRC-17/CAN-FD,
CRC-21/CAN-FD,
CRC-24/BLE,
CRC-24/FLEXRAY-A,
CRC-24/FLEXRAY-B,
CRC-24/INTERLAKEN,
CRC-24/LTE-A,
CRC-24/LTE-B,
CRC-24/OPENPGP, CRC-24,
CRC-24/OS-9,
CRC-30/CDMA,
CRC-31/PHILIPS,
CRC-32/AIXM, CRC-32Q,
CRC-32/AUTOSAR,
CRC-32/BASE91-D, CRC-32D,
CRC-32/BZIP2, CRC-32/AAL5, CRC-32/DECT-B, B-CRC-32,
CRC-32/CD-ROM-EDC,
CRC-32/CKSUM, CKSUM, CRC-32/POSIX,
CRC-32/ISCSI, CRC-32/BASE91-C, CRC-32/CASTAGNOLI, CRC-32/INTERLAKEN, CRC-32C,
CRC-32/ISO-HDLC, CRC-32, CRC-32/ADCCP, CRC-32/V-42, CRC-32/XZ, PKZIP,
CRC-32/JAMCRC, JAMCRC,
CRC-32/MEF,
CRC-32/MPEG-2,
CRC-32/XFER, XFER,
CRC-40/GSM,
CRC-64/ECMA-182, CRC-64,
CRC-64/GO-ISO,
CRC-64/MS,
CRC-64/REDIS,
CRC-64/WE,
CRC-64/XZ, CRC-64/GO-ECMA,
CRC-82/DARC,

## GUIDE

### NuGet

<https://www.nuget.org/packages/Honoo.IO.Hashing.Crc/>

## USAGE

```c#

using Honoo.IO.Hashing;

```

```c#

private static void Demo1()
{
    var crc = Crc.Create("crc32");
    // Update input data.
    crc.Update(inputBytes);
    // The return value is "Hex String".
    string checksum = crc.ComputeFinal().ToHex(CrcCaseSensitivity.Upper);
}

private static void Demo2()
{
    var crc = Crc.Create(CrcName.CRC40_GSM, CrcTableInfo.Standard);
    CrcValue checksum = crc.ComputeFinal(inputBytes);
    // Width is 40 bits, ulong is 64 bits, The value is not truncated.
    ulong value = checksum.ToUInt64();
    bool truncated = checksum.ToUInt64(out ulong value);
    // Width is 40 bits, uint is 32 bits, The value is truncated.
    uint value = checksum.ToUInt32();
    bool truncated = checksum.ToUInt32(out uint value);
}

private static void Demo3()
{
    var crc = new Crc32();
    crc.Update(inputBytes);
    // The return value length is crc.ChecksumByteLength.
    byte[] checksum = crc.ComputeFinal().ToBytes(CrcEndian.BigEndian);
}

private static void Demo4()
{
    // Custom width and parameters are supported.
    var table = new CrcTable(CrcTableInfo.Standard, width, poly, refin, core);
    var crc = Crc.CreateBy("CRC-217/CUSTOM", 217, poly, init, xorout, true, true, table);
    crc.Update(inputBytes);
    byte[] checksum = new byte[crc.ChecksumByteLength];
    int length = crc.ComputeFinal().ToBytes(CrcEndian.BigEndian, checksum, 0);
}

```

## SPEED

AMD5700X DDR4-3600

|algorithm|core|table|table overhead|speed|
|:-------:|:--:|:---:|:------------:|----:|
|CRC-32|UInt32|None||34 MiB/s|
|CRC-32|UInt32|Standard|1 KiB|504 MiB/s|
|CRC-32|UInt32|M16x|16 KiB|3255 MiB/s|
|-|-|-|-|-|
|CRC-7|UInt8|Standard|256 B|341 MiB/s|
|CRC-7|UInt16|Standard|512 B|258 MiB/s|
|CRC-7|UInt32|Standard|1 KiB|551 MiB/s|
|CRC-7|UInt64|Standard|2 KiB|551 MiB/s|
|CRC-7|Sharding8|Standard|256 B|269 MiB/s|
|CRC-7|Sharding16|Standard|512 B|240 MiB/s|
|CRC-7|Sharding32|Standard|1 KiB|242 MiB/s|
|CRC-7|Sharding64|Standard|2 KiB|239 MiB/s|
|-|-|-|-|-|
|CRC-5/ITU|UInt8|M16x|4 KiB|3581 MiB/s|
|CRC-13/BBC|UInt16|M16x|8 KiB|2984 MiB/s|
|CRC-24/BLE|UInt32|M16x|16 KiB|3581 MiB/s|
|CRC-40/GSM|UInt64|M16x|32 KiB|2754 MiB/s|
|-|-|-|-|-|
|SHA1|system|||814 MiB/s|
|SHA256|system|||2106 MiB/s|

## LICENSE

[MIT](LICENSE) license.
