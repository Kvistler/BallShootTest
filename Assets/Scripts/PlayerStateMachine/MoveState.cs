public class MoveState : PlayerState
{
    private float targetZ;
    private bool isWin;
    
    public override void Initialize()
    {
        base.Initialize();
        
        var playerPosition = GameController.Instance.Player.Ball.transform.position;

        const float clearance = 3f;
        var obstacle = GameController.Instance.Obstacles.GetClosestObstacle();
        isWin = !obstacle;
        targetZ = !isWin ?
            obstacle.transform.position.z - clearance :
            GameController.Instance.LevelEnd.PlayerPosition.position.z;

        if (playerPosition.z >= targetZ)
            StateMachine.ChangeState(new IdleState());
    }

    public override void Update()
    {
        base.Update();

        var player = GameController.Instance.Player;
        
        if (player.Ball.transform.position.z <= targetZ)
        {
            player.Move();
        }
        else if (!isWin)
        {
            StateMachine.ChangeState(new IdleState());
        }
        else
        {
            GameController.Instance.OnWin();
        }
    }
}