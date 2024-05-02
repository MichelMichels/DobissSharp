namespace MichelMichels.DobissSharp;

public class DobissGroup
{
    public DobissGroup(string name, List<DobissElement> elements)
    {
        Name = name;
        Elements = elements;
    }

    public string Name { get; }
    public List<DobissElement> Elements { get; }
}