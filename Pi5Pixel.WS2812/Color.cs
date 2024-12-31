namespace Pi5Pixel.WS2812;

public class Color
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    
    public Color()
    {
    }

    public Color(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }
}