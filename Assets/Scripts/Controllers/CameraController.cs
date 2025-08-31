using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform initPosition;

    public void Initialize()
    {
        Debug.Log("[CameraController] Initialize");
        
        camera.position = initPosition.position;
    }
    
    public void Deinitialize()
    {
        Debug.Log("[CameraController] Deinitialize");
        
        camera.position = initPosition.position;
    }
    
    public void Move(float position)
    {
        camera.position = new Vector3(camera.position.x, camera.position.y, position);
    }
}
