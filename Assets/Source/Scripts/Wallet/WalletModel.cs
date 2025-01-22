using System;

public class WalletModel
{
    public int Value { get; private set; }

    public event Action Added;
    public event Action Removed;

    public void Add(int value)
    {
        Value += value;

        Added?.Invoke();
    }

    public bool TryRemove(int value)
    {
        if(Value >= value)
        {
            Value -= value;

            Removed?.Invoke();
            return true;
        }

        return false;
    }
}
