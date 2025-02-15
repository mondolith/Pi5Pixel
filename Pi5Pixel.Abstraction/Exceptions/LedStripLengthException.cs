namespace Pi5Pixel.Abstraction.Exceptions;

public class LedStripLengthException(int ledCount)
    : Exception($"Led count can't be zero or less. Led count: {ledCount}")
{
}