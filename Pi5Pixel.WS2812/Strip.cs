namespace Pi5Pixel.WS2812;

public class Strip
{
    public int LedCount { get; }

    private readonly Color[] _pixels;
    
    private readonly WS2812Driver _driver;

    public Strip(WS2812Driver driver)
    {
        _driver = driver;
        LedCount = _driver.GetLedCount();
        _pixels = new Color[LedCount].Select(_ => new Color()).ToArray();
    }

    public void SetPixelColor(int index, byte red, byte green, byte blue) => _pixels[index] = new Color(red, green, blue);
    
    public void SetPixelColor(int index, Color color) => _pixels[index] = color;
    
    public void SetAllPixels(byte red, byte green, byte blue) => SetAllPixels(new Color(red, green, blue));
    
    public void SetAllPixels(Color color)
    {
        for (var i = 0; i < _pixels.Length; i++) _pixels[i] = color;
    }

    public void Show()
    {
        var buffer = new List<byte>(_pixels.Length * 3);
        foreach (var pixel in _pixels)
        {
            buffer.Add(pixel.Green);
            buffer.Add(pixel.Red);
            buffer.Add(pixel.Blue);
        }
        _driver.Write(buffer.ToArray());
    }
    
    public void Clear() => _driver.Clear();
}