namespace Test;
public class Player()
    :Entity("Игрок", 100, 20)
{
    public int Level{get; private set; } = 1;
}