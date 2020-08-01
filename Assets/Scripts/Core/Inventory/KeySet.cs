public class KeySet
{
    public string Color;
    public int Total;
    public bool CanBeRemoved;

    public KeySet(string color, int total = 0, bool canBeRemoved = true)
    {
        Color = color;
        Total = total;
        CanBeRemoved = canBeRemoved;
    }

    // Supports being able to remove key if used
    // CanBeRemoved is used for Level 1 Green Key
    public void RemoveKey()
    {
        if (CanBeRemoved)
        {
            Total -= 1;
        }
    }

    public void AddKey()
    {
        Total += 1;
    }
}
