namespace Pi5Pixel.Abstraction;

public interface ILedStrip : IDisposable
{
    public Color[] Pixels { get; }
    
    public int LedCount { get; }
    
    void ChangeLedCount(int ledCount, bool updateStrip = false);
    
    void SetPixelColor(int index, byte red, byte green, byte blue);

    void SetPixelColor(int index, Color color);

    void SetAllPixels(byte red, byte green, byte blue);

    void Show();

    void Clear();
}