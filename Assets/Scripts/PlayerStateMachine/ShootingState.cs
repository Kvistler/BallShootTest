public class ShootingState : PlayerState
{
    private Ball Ball => GameController.Instance.Player.Ball;
    
    public override void Initialize()
    {
        base.Initialize();
        
        Ball.Shoot();
        Ball.Projectile.Hit += OnHit;
    }
    
    public override void Deinitialize()
    {
        base.Deinitialize();

        if (Ball.Projectile)
        {
            Ball.Projectile.Hit -= OnHit;
        }
    }

    private void OnHit()
    {
        StateMachine.ChangeState(new MoveState());
    }
}