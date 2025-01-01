namespace Pi5Pixel.WS2812;

public class Strip
{
    public int LedCount { get; }

    public Color[] Pixels { get; }
    
    private readonly WS2812Driver _driver;

    public Strip(WS2812Driver driver)
    {
        _driver = driver;
        LedCount = _driver.GetLedCount();
        Pixels = new Color[LedCount].Select(_ => new Color()).ToArray();
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
        _driver.Write(buffer.ToArray());
    }
    
    public void Clear() => _driver.Clear();
}