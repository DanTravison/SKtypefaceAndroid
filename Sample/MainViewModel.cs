using System;

namespace Sample;

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using SkiaSharp;

public sealed class MainViewModel : INotifyPropertyChanged
{
    string _selectedFontFamily = string.Empty;

    public MainViewModel()
    {
        FontFamilies = [.. SKFontManager.Default.GetFontFamilies()];
    }

    public List<string> FontFamilies
    {
        get;
    }

    public string SelectedFontFamily
    {
        get => _selectedFontFamily;
        set
        {
            if (_selectedFontFamily != value)
            {
                _selectedFontFamily = value;
                PropertyChanged?.Invoke(this, new(nameof(SelectedFontFamily)));
                if (!string.IsNullOrEmpty(value))
                {
                    Properties = CreateProperties(value);
                }
                else
                {
                    Properties = [];
                }
                PropertyChanged?.Invoke(this, new(nameof(Properties)));
            }
        }
    }

    public List<NamedValue> Properties
    {
        get;
        private set;
    } = [];

    static List<NamedValue> CreateProperties(string familyName)
    {
        SKTypeface typeface = SKTypeface.FromFamilyName(familyName, SKFontStyle.Normal);
        if (typeface is not null && typeface.FamilyName != familyName)
        {
            typeface = SKTypeface.FromFamilyName(familyName);
        }
        if (typeface is null)
        {
            Trace.WriteLine($"{familyName} could not be loaded");
            return [];
        }

        Trace.WriteLine($"{familyName} resolved to {typeface.FamilyName}");
        using (SKFont font = new(typeface, 12))
        {
            font.GetFontMetrics(out SKFontMetrics metrics);

            return
            [
                new("FamilyName (Expected)", familyName),
                new("FamilyName (Actual)", typeface.FamilyName),
                new(nameof(typeface.FontWidth), typeface.FontWidth.ToString()),
                new(nameof(typeface.FontSlant), typeface.FontWeight.ToString()),
                new(nameof(typeface.FontWeight), typeface.FontWeight.ToString()),
                new(nameof(metrics.Ascent), metrics.Ascent.ToString()),
                new(nameof(metrics.Descent), metrics.Descent.ToString()),
                new(nameof(metrics.Top), metrics.Top.ToString()),
                new(nameof(metrics.Bottom), metrics.Bottom.ToString()),
                new(nameof(metrics.Leading), metrics.Leading.ToString()),
            ];
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;
}
