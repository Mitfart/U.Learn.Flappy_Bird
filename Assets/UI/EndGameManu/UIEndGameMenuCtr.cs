using System.Collections.Generic;
using Code;
using Code.Infrastructure;
using UI.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.EndGameManu {
   public class UIEndGameMenuCtr : UICtr {
      private const string _BTN_RESTART_ID = "end__btn_restart";
      private const string _HIDDEN_CL      = "end__btn_hidden";

      [SerializeField] private List<RepeatAnimation> repeatableAnimations;

      private Button _restart;
      private Button _exit;

      private GameManager _gameManager;


      protected override IEnumerable<RepeatAnimation> RepeatableAnimations => repeatableAnimations;



      protected override void InitUI() {
         _restart = Root.Q<Button>(_BTN_RESTART_ID);

         _restart.AddToClassList(_HIDDEN_CL);
      }

      public UIEndGameMenuCtr Construct(GameManager gameManager) {
         _gameManager = gameManager;
         return this;
      }



      public async void Show() {
         _restart.RemoveFromClassList(_HIDDEN_CL);
         await Awaitable.WaitForSecondsAsync(_restart.GetTransitionDurationInSeconds());
         
         _restart.clicked += RestartGame;
      }

      public async void Hide() {
         _restart.AddToClassList(_HIDDEN_CL);
         await Awaitable.WaitForSecondsAsync(_restart.GetTransitionDurationInSeconds());
         
         _restart.clicked -= RestartGame;
      }



      private void RestartGame() {
         _gameManager.RestartGame();
      }
   }
}