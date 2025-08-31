public class IdleState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();

        GameController.Instance.Input.HoldStarted += OnHoldStart;
    }

    public override void Deinitialize()
    {
        base.Deinitialize();
        
        GameController.Instance.Input.HoldStarted -= OnHoldStart;
    }

    private void OnHoldStart()
    {
        StateMachine.ChangeState(new ChargingState());
    }
}