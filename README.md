# WS2812 LED Strip Controller for Raspberry Pi 5
Since I did not find any library or NuGet package to control the WS2812 with C# and .NET, I made this.

## Acknowledgements
This project was inspired by [niklasr22](https://github.com/niklasr22) and his work on [rpi5-ws2812](https://github.com/niklasr22/rpi5-ws2812). Thanks for providing the foundational code that made this C# adaptation possible.

## Wiring
**Important:** While it's possible to power the LED strip directly from the Raspberry Pi, it's not recommended for longer strips or higher brightness settings. Using an external power supply ensures stable operation and prevents potential damage to your Pi or your LED strip.

### Required Components
- Raspberry Pi 5
- WS2812 LED Strip
- Jumper Wires or Soldering Iron
- Optional: External 5V Power Supply

### Connection Instructions
- Power Connections
  - Positive (5V)
    - Raspberry Pi Pin 2 or Pin 4
    - Note: For longer strips, consider using an external 5V power supply connected to the additional power wires on the LED strip.
  - Ground (GND)
    - Raspberry Pi Pin 6, 9, 14, 25, 30, 34, or 39
  - Data Connection
    - Data In
      - Connect to Raspberry Pi MOSI (Pin 19 / GPIO 10)
  - LED Strip Wires
    - Outer Wires
      - Red: 5V Power
      - White: Ground
    - Middle Wire
      - Green: Data

## Installation
Enable SPI in the Raspberry Pi config with `sudo raspi-config`

Use `git clone` and reference the project in your solution

## Usage
```CSharp
var driver = new WS2812Driver(bus, device, ledCount); // Initialize with bus = 0 and device = 0

var strip = driver.GetStrip();

strip.SetAllPixels(255, 255, 255); // Perform your effect

strip.Show(); // Changes are only visible after calling strip.Show();
```

## Caution
**Disclaimer:** I am not an expert in electronics, Raspberry Pi hardware, SPI or WS2812 hardware and protocol. While I have taken care to ensure the accuracy of the information and instructions provided in this repository, mistakes can still occur. Proceed with caution and at your own risk.
