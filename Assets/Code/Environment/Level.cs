using Code.Infrastructure;
using UnityEngine;

namespace Code.Environment {
   public class Level : MonoBehaviour {
      public float          width  = 20;
      public float          height = 10;
      public Wall           wallBot;
      public Wall           wallTop;
      public SpriteRenderer background;
      public float          speed = 1f;

      private Render _render;



      public Level Construct(Render render) {
         _render = render;
         return this;
      }

      private void Update() {
         MoveWalls();
      }

      private void OnValidate() => SetSize(width, height);



      private void SetSize(float w, float h) {
         if (float.IsNaN(w)
          || float.IsNaN(h)
          || w <= 0f
          || h <= 0f)
            return;

         width  = w;
         height = h;

         SyncWalls();
         SyncBg();
         SyncCamera();
      }



      private void MoveWalls() {
         float time       = !Application.isPlaying ? 0f : Time.time * speed;
         float halfHeight = height * .5f;

         wallBot.transform.position = halfHeight * Vector3.down + Vector3.right * (wallBot.RepeatSize * .5f - time % wallBot.RepeatSize);
         wallTop.transform.position = halfHeight * Vector3.up   + Vector3.right * (wallBot.RepeatSize * .5f - time % wallTop.RepeatSize);
      }



      private void SyncWalls() {
         wallBot.SetSize(width + wallBot.RepeatSize);
         wallTop.SetSize(width + wallTop.RepeatSize);

         MoveWalls();
      }

      private void SyncBg() {
         Vector2 bgSize = background.size;

         float scale = height / bgSize.y;
         background.transform.localScale = scale * Vector3.one;

         bgSize.x        = width / scale;
         background.size = bgSize;
      }

      private void SyncCamera() {
         if (!_render || !_render.cam)
            return;

         _render.cam.orthographicSize = height * .5f;
      }
   }
}