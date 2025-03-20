using UnityEngine;

namespace Code.Hero {
   public class HeroSmokeController : MonoBehaviour {
      public                 HeroMovement   movement;
      public                 ParticleSystem particle;
      [Range(0f, 2f)] public float          extraSpeedScale = 1.5f;

      private float _speed;

      public float Speed {
         get => _speed;
         set {
            _speed = value;

            ParticleSystem.VelocityOverLifetimeModule main = particle.velocityOverLifetime;
            main.x = -_speed * extraSpeedScale;
         }
      }



      private void OnEnable()  => movement.OnJump += particle.Play;
      private void OnDisable() => movement.OnJump -= particle.Play;



      public void EnableLoop() {
         ParticleSystem.MainModule particleMain = particle.main;
         particleMain.loop = true;
         particle.Play();
      }

      public void DisableLoop() {
         ParticleSystem.MainModule particleMain = particle.main;
         particleMain.loop = false;
         particle.Stop();
      }
   }
}