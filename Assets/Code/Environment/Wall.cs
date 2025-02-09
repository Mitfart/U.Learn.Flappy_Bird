using Unity.VisualScripting;
using UnityEngine;

namespace Code.Environment {
   [RequireComponent(typeof(BoxCollider))]
   public class Wall : MonoBehaviour {
      [SerializeField] private float          size;
      [SerializeField] private SpriteRenderer surface;
      [SerializeField] private SpriteRenderer fill;
      [SerializeField] private BoxCollider    surfaceCollider;

      [field: SerializeField] public float RepeatSize { get; private set; }



      private void OnValidate() {
         surfaceCollider = surfaceCollider.IsUnityNull() ? GetComponent<BoxCollider>() : surfaceCollider;
         
         Redraw();
      }



      public void SetSize(float width) {
         size = width;
         Redraw();
      }



      private void Redraw() {
         Vector2 wallSize;

         wallSize     = surface.size;
         wallSize.x   = size;
         surface.size = wallSize;

         wallSize   = fill.size;
         wallSize.x = size;
         fill.size  = wallSize;

         surfaceCollider.size = wallSize;
      }
   }
}