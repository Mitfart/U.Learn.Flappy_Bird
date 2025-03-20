using UnityEngine;

namespace Code.Environment {
   public class Background : AutoParallax {
      [SerializeField] public  SpriteRenderer image;
      [SerializeField] public  Vector2        refSize;
      [SerializeField] private float          width;
      [SerializeField] private float          height;

      private float _scale = 1f;

      protected override float RepeatSize => base.RepeatSize * _scale;



      public void SetSize(float w, float h) {
         height = h;
         width  = w * 2f;

         Redraw();
      }



      private void Redraw() {
         Vector2 s = refSize;

         _scale = height / s.y;
         s.x    = width  / _scale;

         transform.localScale = _scale * Vector3.one;
         image.size           = s;
      }
   }
}