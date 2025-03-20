using Unity.VisualScripting;
using UnityEngine;

namespace Code.Environment {
   [RequireComponent(typeof(BoxCollider))]
   public class Wall : MonoBehaviour {
      [SerializeField] private SpriteRenderer fill;
      [SerializeField] public  Vector2        refFillSize;
      [SerializeField] private SpriteRenderer surface;
      [SerializeField] public  Vector2        refSurfaceSize;
      [SerializeField] private BoxCollider    deathZone;

      public AutoParallax parallax;

      private float _size;



      private void OnValidate() {
         deathZone = deathZone.IsUnityNull()
            ? GetComponent<BoxCollider>()
            : deathZone;
      }



      public void SetSize(float width) {
         _size = width * 2f;
         Redraw();
      }



      private void Redraw() {
         Vector2 s;

         s         = refFillSize;
         s.x       = _size;
         fill.size = s;

         s            = refSurfaceSize;
         s.x          = _size;
         surface.size = s;

         Vector3 cs = deathZone.size;
         cs.x           = _size;
         deathZone.size = cs;
      }
   }
}