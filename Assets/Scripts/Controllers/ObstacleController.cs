#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
   [SerializeField] private ObstaclesConfig config;
   [SerializeField] private Bounds bounds;
   [SerializeField] private Obstacle obstaclePrefab;
   [SerializeField] private Transform obstacleParent;
   
   private List<Obstacle> obstacles;

   public void Initialize()
   {
      Debug.Log("[ObstacleController] Initialize");
      
      SpawnObstacles();
   }

   public void Deinitialize()
   {
      Debug.Log("[ObstacleController] Deinitialize");
      
      DestroyObstacles();
   }
   
   private void SpawnObstacles()
   {
      Debug.Log($"[ObstacleController] SpawnObstacles count: {config.count}");
      
      obstacles = new List<Obstacle>(config.count);
      
      // UnityRandom produces the same sequence on each run, so use System.Random
      var rand = new System.Random();
      
      for (var i = 0; i < config.count; i++)
      {
         var position = new Vector3(
            (float)(rand.NextDouble() * (bounds.extents.x * 2) - bounds.extents.x),
            0,
            (float)(rand.NextDouble() * (bounds.extents.z * 2) - bounds.extents.z)
         ) + bounds.center + transform.position;
         
         var obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, obstacleParent);
         obstacle.Initialize();
         obstacle.Destroyed += RemoveObstacle;
         
         obstacles.Add(obstacle);
      }
   }

   private void DestroyObstacles()
   {
      if (obstacles == null)
         return;

      obstacles.ForEach(o => o.Deinitialize());
      obstacles.Clear();
      obstacles = null;
   }
   
   [ContextMenu("Respawn Obstacles")]
   public void RespawnObstacles()
   {
      DestroyObstacles();
      SpawnObstacles();
   }
   
   public void Infect(InfectionData data)
   {
      // consider ignoring y-axis if obstacles can be at different heights
      obstacles.Where(obstacle => obstacle.transform.position.Distance2D(data.Position) <= data.Radius)
               .ToList().ForEach(obstacle => obstacle.Infect());
   }
   
   public Obstacle GetClosestObstacle()
   {
      // assuming all obstacles have the same width
      var laneWidth = GameController.Instance.Player.Ball.Power + obstaclePrefab.Width;
      var playerPosition = GameController.Instance.Player.Ball.transform.position;
      
      var obstacle = obstacles
         .Where(o => !o.IsInfected && Mathf.Abs(o.transform.position.x - playerPosition.x) < laneWidth * 0.5f)
         .OrderBy(o => Vector3.Distance(o.transform.position, playerPosition))
         .FirstOrDefault();

      Debug.Log($"[ObstacleController] GetClosestPossiblePlayerPosition: closest obstacle: {obstacle?.name}", obstacle);
      return obstacle;
   }

   private void RemoveObstacle(Obstacle obstacle)
   {
      obstacles.Remove(obstacle);
   }
   
   private void OnDrawGizmosSelected()
   {
      var center = transform.position + bounds.center;
      var size = bounds.size;

      var p1 = center + new Vector3(-size.x * 0.5f, 0, -size.z * 0.5f);
      var p2 = center + new Vector3(-size.x * 0.5f, 0, size.z * 0.5f);
      var p3 = center + new Vector3(size.x * 0.5f, 0, size.z * 0.5f);
      var p4 = center + new Vector3(size.x * 0.5f, 0, -size.z * 0.5f);

      /*Gizmos.color = Color.yellow;
      Gizmos.DrawLine(p1, p2);
      Gizmos.DrawLine(p2, p3);
      Gizmos.DrawLine(p3, p4);
      Gizmos.DrawLine(p4, p1);*/
      
      // Gizmos lines are too thin, so use Handles instead
#if UNITY_EDITOR
      Handles.color = Color.yellow;
      Handles.DrawAAPolyLine(4f, p1, p2, p3, p4, p1);
#endif
   }
}

public struct InfectionData
{
   public Vector3 Position;
   public float Radius;
}
