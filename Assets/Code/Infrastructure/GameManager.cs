using Code.Environment;
using Code.UI;
using UnityEngine;

namespace Code.Infrastructure {
   public class GameManager : MonoBehaviour {
      public Render render;
      public Score  score;

      public Hero.Hero        heroPrefab;
      public Level            levelPrefab;
      public ObstaclesSpawner obstaclesSpawner;
      public UIScore          uiScorePrefab;

      private Hero.Hero        _hero;
      private Level            _level;
      private ObstaclesSpawner _obstaclesSpawner;
      private UIScore          _uiScore;



      private void Start() {
         _level            = Instantiate(levelPrefab).Construct(render);
         _obstaclesSpawner = Instantiate(obstaclesSpawner).Construct(_level, score);
         _hero             = Instantiate(heroPrefab);
         _uiScore          = Instantiate(uiScorePrefab).Construct(score);
      }
   }
}