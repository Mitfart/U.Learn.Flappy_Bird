using Code.Infrastructure;
using TMPro;
using UnityEngine;

namespace Code.UI {
   public class UIScore : MonoBehaviour {
      public TextMeshProUGUI text;

      private Score _score;



      public UIScore Construct(Score score) {
         _score = score;

         Redraw(_score.Get());
         _score.OnChange += Redraw;
         return this;
      }

      private void OnDestroy() {
         _score.OnChange -= Redraw;
      }


      private void Redraw(int newScore) {
         text.text = newScore.ToString();
      }
   }
}