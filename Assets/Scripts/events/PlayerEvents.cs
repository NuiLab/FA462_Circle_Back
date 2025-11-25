using System;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null) 
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null) 
        {
            onEnablePlayerMovement();
        }
    }

    public event Action<int> onSeniorityGained;
    public void SeniorityGained(int seniority) 
    {
        if (onSeniorityGained != null) 
        {
            onSeniorityGained(seniority);
        }
    }

    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level) 
    {
        if (onPlayerLevelChange != null) 
        {
            onPlayerLevelChange(level);
        }
    }

    public event Action<int> onPlayerSeniorityChange;
    public void PlayerSeniorityChange(int seniority) 
    {
        if (onPlayerSeniorityChange != null) 
        {
            onPlayerSeniorityChange(seniority);
        }
    }
}