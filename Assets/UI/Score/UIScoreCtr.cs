using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Score {
   public class UIScoreCtr : MonoBehaviour {
      public UIDocument view;

      private VisualElement _root;
      private UIScore       _scoreUI;

      [SerializeField] private Code.Infrastructure.Score score;



      private void OnEnable() {
         _root    = view.rootVisualElement;
         _scoreUI = _root.Q<UIScore>();

         _scoreUI.dataSource = score;
      }

      public UIScoreCtr Construct(Code.Infrastructure.Score score) {
         this.score          = score;
         _scoreUI.dataSource = score;
         return this;
      }
   }
}