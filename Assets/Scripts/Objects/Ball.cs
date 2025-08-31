using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private BallConfig config;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform model;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private Projectile projectilePrefab;
    
    public Projectile Projectile { get; private set; }

    public float Power => model.localScale.x;
    public bool HasEnoughPower => Power > config.minimumSize;
    
    public void Initialize()
    {
        Debug.Log("[Ball] Initialize");
        
        model.localScale = Vector3.one * config.initialSize;
        
        UpdateLine();
    }

    public void Deinitialize()
    {
        Debug.Log("[Ball] Deinitialize");

        if (Projectile)
            Projectile.Deinitialize();
    }
    
    public void PrepareProjectile()
    {
        if (Projectile)
            Projectile.Deinitialize();
        
        Projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity, projectileSpawn);
        Projectile.Initialize();
    }

    public void Charge()
    {
        var scaleTransfer = config.chargingSpeed * Time.deltaTime;
        
        model.localScale -= scaleTransfer * Vector3.one;
        Projectile.transform.localScale += scaleTransfer * Vector3.one;

        UpdateLine();
    }

    public void Move()
    {
        transform.Translate(config.moveSpeed * Time.deltaTime * Vector3.forward);
        UpdateLine();
    }

    private void UpdateLine()
    {
        line.widthCurve = new AnimationCurve(new Keyframe(0f, Power), new Keyframe(1f, Power));
        
        line.positionCount = 2;
        
        const float heightOffset = 0.01f;
        var position = transform.position;
        position.y = heightOffset;
        var destination = GameController.Instance.LevelEnd.transform.position;
        destination.y = heightOffset;
        
        line.SetPositions(new[]
        {
            position,
            destination,
        });
    }

    public void Shoot()
    {
        Projectile.Shoot();
    }
}