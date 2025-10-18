using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface ISeguimiento
{
    void SeguirPlayer();
    
}
public interface IInteractuable
{
    public void Interact(GameObject observer);
}
public interface IDetection
{
    void PlayerDetected();
}

public interface IStatus
{
    void UpdateStatus();
    
        
}

