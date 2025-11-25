using System;

public class MoneyEvents
{
    public event Action<int> onMoneyGained;
    public void MoneyGained(int Money) 
    {
        if (onMoneyGained != null) 
        {
            onMoneyGained(Money);
        }
    }

    public event Action<int> onMoneyChange;
    public void MoneyChange(int Money) 
    {
        if (onMoneyChange != null) 
        {
            onMoneyChange(Money);
        }
    }
}