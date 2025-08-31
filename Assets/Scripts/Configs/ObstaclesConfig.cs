using UnityEngine;

[CreateAssetMenu(fileName = "ObstaclesConfig", menuName = "Configs/ObstaclesConfig")]
public class ObstaclesConfig : ScriptableObject
{
    [Min(0)]
    public int count;
}