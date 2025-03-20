using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure {
   [Serializable]
   public class GameManager {
      private const string _BOOT = "Boot";

      private Bootstrap _bootstrap;



      public GameManager(Bootstrap bootstrap) {
         _bootstrap = bootstrap;
      }



      public void BootGame() {
         _bootstrap.UIEndGameMenuCtr.Hide();

         _bootstrap.Hero.Disable();
         _bootstrap.ObstaclesSpawner.Disable();
      }

      public void StartGame() {
         _bootstrap.Hero.Enable();
         _bootstrap.ObstaclesSpawner.Enable();
      }

      public void EndGame() {
         _bootstrap.ObstaclesSpawner.Disable();

         _bootstrap.speedManager.ScaleSpeed(0);
         _bootstrap.UIEndGameMenuCtr.Show();
      }

      public async void RestartGame() {
         await _bootstrap.UILoadingCtr.Show();
         await SceneManager.LoadSceneAsync(_BOOT);
      }
   }
}