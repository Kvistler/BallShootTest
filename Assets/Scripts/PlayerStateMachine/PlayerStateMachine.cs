using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    
    public void Initialize()
    {
        Debug.Log("[PlayerStateMachine] Initialize");
        
        ChangeState(new IdleState());
    }
    
    public void Deinitialize()
    {
        Debug.Log("[PlayerStateMachine] Deinitialize");
        CurrentState?.Deinitialize();
        CurrentState = null;
    }
    
    public void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(PlayerState newState)
    {
        Debug.Log($"[PlayerStateMachine] ChangeState from {CurrentState?.GetType().Name} to {newState?.GetType().Name}");
        
        CurrentState?.Deinitialize();
        
        CurrentState = newState;
        CurrentState?.SetDependencies(this);
        CurrentState?.Initialize();
    }
}

public abstract class PlayerState
{
    protected PlayerStateMachine StateMachine { get; private set; }

    public void SetDependencies(PlayerStateMachine playerStateMachine)
    {
        StateMachine = playerStateMachine;
    }
    
    public virtual void Initialize() { }
    public virtual void Deinitialize() { }
    public virtual void Update() { }
}
