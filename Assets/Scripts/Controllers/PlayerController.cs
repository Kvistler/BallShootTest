using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Transform initPosition;

    public event Action<float> Moved;
    
    public Ball Ball => ball;
    
    private PlayerStateMachine stateMachine;
    
    public void Initialize()
    {
        Debug.Log("[PlayerController] Initialize");
        
        ball.Initialize();
        ball.transform.position = initPosition.position;

        stateMachine = new PlayerStateMachine();
        stateMachine.Initialize();
    }

    public void Deinitialize()
    {
        Debug.Log("[PlayerController] Deinitialize");
        
        ball.Deinitialize();
        ball.transform.position = initPosition.position;
        
        stateMachine.Deinitialize();
    }
    
    private void Update()
    {
        stateMachine?.Update();
    }

    public void Move()
    {
        ball.Move();
        Moved?.Invoke(ball.transform.position.z);
    }
}
