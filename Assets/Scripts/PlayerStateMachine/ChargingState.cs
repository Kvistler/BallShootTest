public class ChargingState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();

        GameController.Instance.Player.Ball.PrepareProjectile();
        GameController.Instance.Input.HoldEnded += OnHoldEnd;
    }

    public override void Deinitialize()
    {
        base.Deinitialize();
        
        GameController.Instance.Input.HoldEnded -= OnHoldEnd;
    }

    private void OnHoldEnd()
    {
        StateMachine.ChangeState(new ShootingState());
    }

    public override void Update()
    {
        base.Update();

        var ball = GameController.Instance.Player.Ball;
        
        ball.Charge();

        if (!ball.HasEnoughPower)
        {
            StateMachine.ChangeState(null);
            GameController.Instance.OnDeath();
        }
    }
}