using System.Collections;
using System.Device.Spi;

namespace Pi5Pixel.WS2812;

public class LedStrip : IDisposable
{
    public Color[] Pixels { get; private set; }

    public int LedCount { get; private set; }
    
    private readonly SpiDevice _device;
    
    private const byte LedZero = 0b1100_0000;
    private const byte LedOne = 0b1111_1100;

    public LedStrip(int spiBus, int spiDevice, int ledCount = 0)
    {
        var settings = new SpiConnectionSettings(spiBus, spiDevice)
        {
            ClockFrequency = 6_500_000,
            Mode = SpiMode.Mode0,
            DataFlow = DataFlow.MsbFirst
        };

        _device = SpiDevice.Create(settings);
        
        LedCount = ledCount;
        
        Pixels = GenerateEmptyColors();
    }
    
    private Color[] GenerateEmptyColors() => new Color[LedCount].Select(_ => new Color()).ToArray();

    public void ChangeLedCount(int ledCount, bool callShow = false)
    {
        LedCount = ledCount;
        Pixels = GenerateEmptyColors();
        if (callShow) Show();
    }

    public void SetPixelColor(int index, byte red, byte green, byte blue) => Pixels[index] = new Color(red, green, blue);
    
    public void SetPixelColor(int index, Color color) => Pixels[index] = color;
    
    public void SetAllPixels(byte red, byte green, byte blue) => SetAllPixels(new Color(red, green, blue));
    
    public void SetAllPixels(Color color)
    {
        for (var i = 0; i < Pixels.Length; i++) Pixels[i] = color;
    }

    public void Show()
    {
        var buffer = new List<byte>(Pixels.Length * 3);
        foreach (var pixel in Pixels)
        {
            buffer.Add(pixel.Green);
            buffer.Add(pixel.Red);
            buffer.Add(pixel.Blue);
        }
        Write(buffer.ToArray());
    }

    public void Clear() => _device.Write(new byte[LedCount * 24].Select(_ => LedZero).ToArray());

    private void Write(byte[] colorBytes)
    {
        var colorBits = new BitArray(colorBytes);

        var spiBits = new byte[colorBits.Length].Select((_, index) => colorBits[index] ? LedOne : LedZero).ToArray();

        _device.Write(spiBits);
    }

    public void Dispose()
    {
        Clear();
        _device.Dispose();
    }
}