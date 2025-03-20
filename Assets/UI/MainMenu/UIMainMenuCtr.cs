using System.Collections.Generic;
using Code.Infrastructure;
using UI.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu {
   public class UIMainMenuCtr : UICtr {
      private const string _BTN_ID = "tap__btn";

      [SerializeField] private List<RepeatAnimation> repeatableAnimations = new() { new RepeatAnimation("tap__hand", "tap__hand_anim", .5f) };

      private Button      _start;
      private GameManager _gameManager;



      protected override IEnumerable<RepeatAnimation> RepeatableAnimations => repeatableAnimations;



      public UIMainMenuCtr Construct(GameManager gameManager) {
         _gameManager = gameManager;
         return this;
      }


      private void StartGame() {
         _gameManager.StartGame();

         view.enabled = false;
      }

      protected override void InitUI()      => _start = Root.Q<Button>(_BTN_ID);
      protected override void RegEvents()   => _start.clicked += StartGame;
      protected override void UnregEvents() => _start.clicked -= StartGame;
   }
}