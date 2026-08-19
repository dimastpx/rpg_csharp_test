namespace Test.Items;

public class Item(string name, ItemType type, Effect[]? effects = null)
{
    public string Name{get; } = name;
    public Effect[]? Effects{get; } = effects;
    public ItemType Type{get; } = type;
}