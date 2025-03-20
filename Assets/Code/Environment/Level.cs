using Code.Infrastructure;
using UnityEngine;

namespace Code.Environment {
   public class Level : MonoBehaviour {
      [field: SerializeField] public float Height { get; private set; } = 10;

      [SerializeField] private Wall       wallBot;
      [SerializeField] private Wall       wallTop;
      [SerializeField] private Background background;

      private Render       _render;
      private SpeedManager _speed;

      public float Width      { get; private set; }
      public float HalfHeight => Height * .5f;



      public Level Construct(Render render, SpeedManager speedManager) {
         _render = render;
         _speed  = speedManager;

         SyncScene();

         _speed.OnChangeSpeed += SetSpeed;
         return this;
      }

      private void OnValidate() {
         if (float.IsNaN(Height)
          || Height  <= 0f
          || _render == null
          || !_render.cam)
            return;

         SyncScene();
      }



      private void SyncScene() {
         SyncCamera();
         SyncBackground();
         SyncWalls();
         SetSpeed(_speed.Get());
      }


      private void SyncCamera() {
         _render.cam.orthographicSize = Height * .5f;

         Width = _render.cam.aspect * Height;
      }


      private void SyncBackground() {
         background.SetSize(Width, Height);
      }


      private void SyncWalls() {
         wallBot.SetSize(Width);
         wallTop.SetSize(Width);

         SyncWallsPos();
      }

      private void SyncWallsPos() {
         wallTop.transform.SetPositionY(+HalfHeight);
         wallBot.transform.SetPositionY(-HalfHeight);
      }


      private void SetSpeed(float speed) {
         background.speed       = speed;
         wallBot.parallax.speed = speed;
         wallTop.parallax.speed = speed;
      }
   }
}