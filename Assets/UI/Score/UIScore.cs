using Unity.Properties;
using UnityEngine.UIElements;

namespace UI.Score {
   [UxmlElement]
   public partial class UIScore : VisualElement {
      [DontCreateProperty] private int _score;

      private readonly Label _scoreTxt;

      [UxmlAttribute, CreateProperty]
      public int Score {
         get => _score;
         set {
            _score         = value;
            _scoreTxt.text = value.ToString();

            MarkDirtyRepaint();
         }
      }



      public UIScore() {
         AddToClassList("score");

         _scoreTxt = new Label(_score.ToString());
         _scoreTxt.AddToClassList("score__txt");
         Add(_scoreTxt);
      }
   }
}