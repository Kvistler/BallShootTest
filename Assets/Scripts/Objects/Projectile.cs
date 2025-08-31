using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private BallConfig config;
    
    public float ImpactRadius => transform.localScale.x * config.impactRadiusMultiplier;
    
    public event Action Hit;
    
    private bool isFlying;

    public void Initialize()
    {
        Debug.Log("[Projectile] Initialize");
        
        transform.localScale = Vector3.zero;
    }

    public void Deinitialize()
    {
        Debug.Log("[Projectile] Deinitialize");
        
        if (gameObject)
            Destroy(gameObject);
    }
    
    public void Shoot()
    {
        Debug.Log("[Projectile] Shoot");
        
        transform.SetParent(null);
        isFlying = true;
    }

    private void Update()
    {
        if (!isFlying)
            return;
        
        transform.position += config.projectileSpeed * Time.deltaTime * Vector3.forward;
    }

    public void OnHit()
    {
        Hit?.Invoke();
        Deinitialize();
    }
}