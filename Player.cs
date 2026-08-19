namespace Test;
public class Player()
    :Entity("Игрок", 100, 20, 0)
{
    public int Level{get; private set; } = 1;
    public int Xp{get; private set; } = 0;

    public void AddXp(int count = 20)
    {
        Xp += count;
        if(Xp >= 100)
        {
            Xp = 0;
            SetLevel(Level + 1);
        }
    }

    private void SetLevel(int value)
    {
        if(value < 0)
        {
            Level = 0;
        }
        else if(value > 5)
        {
            Level = 5;
        }
        else
        {
            Level = value;
        }
    }
}