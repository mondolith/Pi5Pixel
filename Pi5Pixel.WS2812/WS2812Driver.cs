using System.Collections;
using System.Device.Spi;

namespace Pi5Pixel.WS2812;

public class WS2812Driver : IDisposable
{
    private const byte LedZero = 0b1100_0000;
    private const byte LedOne = 0b1111_1100;

    private readonly int _ledCount;

    private readonly SpiDevice _device;

    private readonly byte[] _clearBuffer;

    public WS2812Driver(int spiBus, int spiDevice, int ledCount)
    {
        var settings = new SpiConnectionSettings(spiBus, spiDevice)
        {
            ClockFrequency = 6_500_000,
            Mode = SpiMode.Mode0,
            DataFlow = DataFlow.MsbFirst
        };

        _device = SpiDevice.Create(settings);
        
        _ledCount = ledCount;

        _clearBuffer = new byte[_ledCount * 24].Select(_ => LedZero).ToArray();
    }

    public void Write(byte[] colorBytes)
    {
        var colorBits = new BitArray(colorBytes);

        var spiBits = new byte[colorBits.Length].Select((_, index) => colorBits[index] ? LedOne : LedZero).ToArray();

        _device.Write(spiBits);
    }

    public void Clear() => _device.Write(_clearBuffer);

    public int GetLedCount() => _ledCount;

    public Strip GetStrip() => new(this);

    public void Dispose() => _device.Dispose();
}