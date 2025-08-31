using UnityEngine;

[CreateAssetMenu(fileName = "BallConfig", menuName = "Configs/BallConfig")]
public class BallConfig : ScriptableObject
{
    [Min(0f)]
    public float initialSize;
    [Min(0f)]
    public float minimumSize;
    [Min(0f)]
    public float chargingSpeed;
    [Min(0f)]
    public float moveSpeed;
    [Min(0f)]
    public float projectileSpeed;
    [Min(0f)]
    public float impactRadiusMultiplier;
}