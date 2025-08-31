using System;
using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private ParticleSystem popEffect;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material infectedMaterial;
    
    public event Action<Obstacle> Destroyed;
    
    public float Width => collider.radius * 2f;
    public bool IsInfected { get; private set; }

    public void Initialize()
    {
        renderer.material = defaultMaterial;
    }
    
    public void Deinitialize()
    {
        Destroy(gameObject);
    }

    public void Infect()
    {
        IsInfected = true;
        
        renderer.material = infectedMaterial;

        StartCoroutine(PopCoroutine());
        Destroyed?.Invoke(this);
    }

    private IEnumerator PopCoroutine()
    {
        yield return new WaitForSeconds(1f);
        
        Instantiate(popEffect, transform.position, Quaternion.identity);
        
        Deinitialize();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"[Obstacle] OnCollisionEnter with {other.gameObject.name}");
        
        var projectile = other.gameObject.GetComponent<Projectile>();
        if (!projectile)
            return;
        
        GameController.Instance.Obstacles.Infect(new InfectionData
        {
            Position = transform.position,
            Radius = projectile.ImpactRadius,
        });
            
        projectile.OnHit();
    }
}