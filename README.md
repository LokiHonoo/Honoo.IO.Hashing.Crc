# Honoo.IO.Hashing.Crc

- [Honoo.IO.Hashing.Crc](#honooiohashingcrc)
  - [Introduction](#introduction)
  - [Usage](#usage)
    - [Namespace](#namespace)
    - [Demo](#demo)
  - [License](#license)

## Introduction

Crc Crc4 Crc5 Crc6 Crc7 Crc8 Crc16 Crc32 Crc64.

## Usage

### Namespace

```c#

using Honoo.IO.Hashing;

```

### Demo

```c#

private static void Demo1()
{
    var crc = Crc.Create("crc32");
    crc.Update(inputBytes);
    byte[] checksum = crc.DoFinal();
}

```

```c#

private static void Demo2()
{
    var crc = new Crc16Modbus();
    byte[] checksum = crc.DoFinal(inputBytes);
}

```

## License

The development and release of this project is based on MIT licence.
