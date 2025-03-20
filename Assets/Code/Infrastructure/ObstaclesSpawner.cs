using System.Collections.Generic;
using Code.Environment;
using UnityEngine;

namespace Code.Infrastructure {
   public class ObstaclesSpawner : MonoBehaviour {
      public                 Obstacle[] prefabs;
      public                 Vector2    scaleRatio         = new(1f, 2f);
      [Min(0f)]       public float      tiltRatio          = 15f;
      [Min(0.02f)]    public float      frequency          = .5f;
      [Range(0f, 1f)] public float      topObstaclesChance = .5f;
      [Min(1)]        public int        reward             = 1;

      private readonly List<Obstacle> _spawnedObstacles = new();

      private Level        _level;
      private SpeedManager _speedManager;
      private Score        _score;

      private float _lastSpawnTime;



      public ObstaclesSpawner Construct(Level level, SpeedManager speedManager, Score scoreRecorder) {
         _level        = level;
         _speedManager = speedManager;
         _score        = scoreRecorder;
         return this;
      }

      private void Update() => MoveObstacles();

      private void FixedUpdate() {
         float time = Time.time;

         if ((time - _lastSpawnTime) * _speedManager.SpeedScale < frequency)
            return;

         SpawnRandomObstacle();
         _lastSpawnTime = time;
      }



      public void Enable()  => enabled = true;
      public void Disable() => enabled = false;



      private void MoveObstacles() {
         for (var i = 0; i < _spawnedObstacles.Count; i++) {
            Obstacle obstacle = _spawnedObstacles[i];
            obstacle.Move(_speedManager.Get());

            if (!BeyondLevel(obstacle))
               continue;

            _spawnedObstacles.Remove(obstacle);
            obstacle.DestroySelf();
            i--;
         }
      }


      private bool BeyondLevel(Obstacle obstacle) => obstacle.transform.position.x < -_level.Width;



      private void SpawnRandomObstacle() {
         Obstacle obstaclePrefab = RandomObstacle();


         Obstacle obstacle = Instantiate(
            obstaclePrefab,
            new Vector3(_level.Width, -_level.HalfHeight),
            Quaternion.identity
         );

         _spawnedObstacles.Add(obstacle);

         obstacle.checker
                 .SetSize(_level.Height)
                 .SetReward(_score, reward);

         ScaleRandomly(obstacle);
         TiltRandomly(obstacle);

         bool top = Random.Range(0f, 1f) <= topObstaclesChance;
         if (top)
            MirrorHorizontally(obstacle);
      }


      private static void MirrorHorizontally(Obstacle obstacle) {
         Transform obstacleT = obstacle.transform;

         Vector3 s = obstacleT.localScale;
         s.y                  *= -1;
         obstacleT.localScale =  s;

         obstacleT.SetPositionY(obstacleT.position.y * -1);
      }

      private void TiltRandomly(Obstacle  obstacle) => obstacle.transform.rotation = Quaternion.Euler(x: 0f, y: 0f, Random.Range(-tiltRatio, tiltRatio));
      private void ScaleRandomly(Obstacle obstacle) => obstacle.transform.localScale = Vector3.one * Random.Range(scaleRatio.x, scaleRatio.y);

      private Obstacle RandomObstacle() => prefabs[Random.Range(minInclusive: 0, prefabs.Length)];
   }
}