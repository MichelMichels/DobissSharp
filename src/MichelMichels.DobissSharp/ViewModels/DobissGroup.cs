namespace MichelMichels.DobissSharp.ViewModels
{
    public class DobissGroup
    {
        public DobissGroup(string name, List<DobissNXTElement> elements)
        {
            Name = name;
            Elements = elements;
        }
        
        public string Name { get; }
        public List<DobissNXTElement> Elements { get; }
    }
}