using System;
using System.Linq;
using Code.Environment;
using UI.EndGameManu;
using UI.Loading;
using UI.MainMenu;
using UI.Score;
using UnityEngine;

namespace Code.Infrastructure {
   public class Bootstrap : MonoBehaviour {
      public                                         Render       render;
      public                                         SpeedManager speedManager;
      public                                         Score        score;
      [Range(                        0f, 1f)] public float        speedByScoreScale;
      [field: SerializeField] [Range(0f, 1f)] public float        HeroSpawnPoint { get; private set; } = .31f;

      public Hero.Hero        heroPrefab;
      public Level            levelPrefab;
      public ObstaclesSpawner obstaclesSpawnerPrefab;

      public UILoadingCtr     uiLoadingCtrPrefab;
      public UIMainMenuCtr    uiMainMenuCtrPrefab;
      public UIEndGameMenuCtr uiEndGameMenuCtrPrefab;
      public UIScoreCtr       uiScorePrefabPrefab;

      private GameManager _gameManager;

      public Level            Level            { get; private set; }
      public ObstaclesSpawner ObstaclesSpawner { get; private set; }
      public Hero.Hero        Hero             { get; private set; }

      public UILoadingCtr     UILoadingCtr     { get; private set; }
      public UIMainMenuCtr    UIMainMenuCtr    { get; private set; }
      public UIEndGameMenuCtr UIEndGameMenuCtr { get; private set; }
      public UIScoreCtr       UIScore          { get; private set; }



      private async void Awake() {
         _gameManager = new GameManager(this);
         UILoadingCtr = Instantiate(uiLoadingCtrPrefab);
         
         
         await UILoadingCtr.Show();

         Level            = (await InstantiateAsync(levelPrefab)).First().Construct(render, speedManager);
         ObstaclesSpawner = (await InstantiateAsync(obstaclesSpawnerPrefab)).First().Construct(Level, speedManager, score);
         
         UIMainMenuCtr    = (await InstantiateAsync(uiMainMenuCtrPrefab)).First().Construct(_gameManager);
         UIEndGameMenuCtr = (await InstantiateAsync(uiEndGameMenuCtrPrefab)).First().Construct(_gameManager);
         UIScore          = (await InstantiateAsync(uiScorePrefabPrefab)).First().Construct(score);
         
         Hero = (await InstantiateAsync(heroPrefab)).First().Init(_gameManager, speedManager);
         Hero.transform.SetPositionX(Level.Width * (HeroSpawnPoint - .5f));
         
         score.OnChange += s => speedManager.ScaleSpeed(1f + s * speedByScoreScale);
         speedManager.ScaleSpeed(1f + score.Get() * speedByScoreScale);
         
         _gameManager.BootGame();
         
         await UILoadingCtr.Hide();
      }
   }
}