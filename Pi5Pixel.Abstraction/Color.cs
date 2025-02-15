using System.ComponentModel.DataAnnotations;

namespace Pi5Pixel.Abstraction;

public class Color(byte red, byte green, byte blue)
{
    [Range(0, 255)] public byte Red { get; } = red;
    [Range(0, 255)] public byte Green { get; } = green;
    [Range(0, 255)] public byte Blue { get; } = blue;

    public Color() : this(0, 0, 0)
    {
    }
}