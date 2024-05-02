namespace MichelMichels.DobissSharp;

public class DobissGroup(string name)
{
    public string Name { get; } = name;
    public List<DobissElement> Elements { get; } = [];
}