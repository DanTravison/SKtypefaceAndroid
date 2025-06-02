namespace Sample;

/// <summary>
/// Provides a strongly typed named value.
/// </summary>
public class NamedValue
{
    /// <summary>
    /// Initializes a new instance of this class
    /// </summary>
    /// <param name="name">The <see cref="Name"/></param>
    /// <param name="value">The <see cref="Value"/>.</param>
    public NamedValue(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name
    {
        get;
    }

    public string Value
    {
        get;
    }
}