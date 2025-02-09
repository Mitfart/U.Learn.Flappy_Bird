using System.Collections.Generic;
using Code.Environment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Infrastructure {
   public class ObstaclesSpawner : MonoBehaviour {
      public                 Obstacle[] prefabs;
      public                 Vector2    scaleRatio = new(1f, 2f);
      [Min(0f)]       public float      tiltRatio  = 15f;
      [Min(0.02f)]    public float      frequancy  = .5f;
      [Range(0f, 1f)] public float      topObstaclesChance;

      private readonly List<Obstacle> _spawnedObstacles = new();

      private float _lastSpawnTime;
      private Level _level;
      private Score _score;



      public ObstaclesSpawner Construct(Level level, Score scoreRecorder) {
         _level = level;
         _score = scoreRecorder;
         return this;
      }

      private void Update() => MoveObstacles();

      private void FixedUpdate() {
         float time = Time.time;

         if (time - _lastSpawnTime < frequancy)
            return;

         SpawnRandomObstacle();
         _lastSpawnTime = time;
      }



      private void MoveObstacles() {
         for (var i = 0; i < _spawnedObstacles.Count; i++) {
            Obstacle obstacle = _spawnedObstacles[i];
            obstacle.Move(_level.speed);

            if (!BeyondLevel(obstacle))
               continue;

            _spawnedObstacles.Remove(obstacle);
            obstacle.DestroySelf();
            i--;
         }
      }



      private bool BeyondLevel(Obstacle obstacle) => obstacle.transform.position.x < -_level.width;



      private void SpawnRandomObstacle() {
         Obstacle prefab = RandomObstacle();

         bool  top  = Random.Range(0f, 1f) <= topObstaclesChance;
         float yPos = (top ? _level.height : -_level.height) * .5f;
         float xPos = _level.width                           * .5f;

         Obstacle obstacle = Instantiate(
            prefab,
            new Vector3(xPos, yPos),
            Quaternion.Euler(0f, 0f, Random.Range(-tiltRatio, tiltRatio))
         );
         ScaleRandomly(obstacle);

         if (top) {
            Transform obstacleT = obstacle.transform;
            Vector3   s         = obstacleT.localScale;
            s.y                  *= -1;
            obstacleT.localScale =  s;
         }

         _spawnedObstacles.Add(obstacle);

         obstacle.checker
                 .SetSize(_level.height)
                 .SetReward(_score);
      }


      private void ScaleRandomly(Component obstacle) => obstacle.transform.localScale = Vector3.one * Random.Range(scaleRatio.x, scaleRatio.y);

      private Obstacle RandomObstacle() => prefabs[Random.Range(0, prefabs.Length)];
   }
}