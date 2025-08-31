using UnityEngine;

// Editor script to make the camera look at the ball in the scene view
public class CameraLookAt : MonoBehaviour
{
    [ContextMenu("Look At Ball")]
    public void LookAtBall()
    {
        var ball = FindAnyObjectByType<Ball>();
        transform.LookAt(ball.transform);
    }
}
