using UnityEngine;

namespace Code.Environment {
   public class AutoParallax : MonoBehaviour {
      public                         float speed;
      public                         float repeatSize;
      [field: SerializeField] public float ParallaxFactor { get; private set; } = 1f;

      protected virtual float RepeatSize => repeatSize;

      private float _passedSize;


      private void Update() => Move();

      private void Move() {
         _passedSize += !Application.isPlaying ? 0f : speed * ParallaxFactor * Time.deltaTime;
         _passedSize %= RepeatSize;

         Transform t   = transform;
         Vector3   pos = t.position;

         pos.x      = RepeatSize * .5f - _passedSize;
         t.position = pos;
      }
   }
}