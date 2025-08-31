using System.Collections;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform leftHinge;
    [SerializeField] private Transform rightHinge;
    [SerializeField] private float angle;
    [SerializeField] private float speed;

    public Transform PlayerPosition => playerPosition;
    
    private Coroutine openCoroutine;
    
    public void Initialize()
    {
        Debug.Log("[Destination] Initialize");
        
        ResetState();
    }
    
    public void Deinitialize()
    {
        Debug.Log("[Destination] Deinitialize");
        
        ResetState();
    }

    private void ResetState()
    {
        if (openCoroutine != null)
        {
            StopCoroutine(openCoroutine);
            openCoroutine = null;
        }
        
        leftHinge.localRotation = Quaternion.identity;
        rightHinge.localRotation = Quaternion.identity;
    }
    
    [ContextMenu("Open")]
    public void Open()
    {
        openCoroutine = StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        var leftTarget = Quaternion.Euler(0, angle, 0);
        var rightTarget = Quaternion.Euler(0, -angle, 0);
        
        while (Quaternion.Angle(leftHinge.localRotation, leftTarget) > 0.1f)
        {
            leftHinge.localRotation = Quaternion.RotateTowards(leftHinge.localRotation, leftTarget, speed * Time.deltaTime);
            rightHinge.localRotation = Quaternion.RotateTowards(rightHinge.localRotation, rightTarget, speed * Time.deltaTime);
            
            yield return null;
        }

        leftHinge.localRotation = leftTarget;
        rightHinge.localRotation = rightTarget;
        
        openCoroutine = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        var projectile = other.gameObject.GetComponent<Projectile>();
        if (!projectile)
            return;
        
        Debug.Log($"[LevelEnd] OnCollisionEnter with projectile");
        
        projectile.OnHit();
    }
}