using Code.Infrastructure;
using UnityEngine;

namespace Code.Hero {
   public class Hero : MonoBehaviour {
      public HeroController      controller;
      public HeroMovement        movement;
      public HeroAnimator        animator;
      public HeroSmokeController smoke;

      private GameManager  _gameManager;
      private SpeedManager _speed;



      public Hero Init(GameManager gameManager, SpeedManager speedManager) {
         _speed       = speedManager;
         _gameManager = gameManager;

         smoke.Speed = _speed.Get();
         return this;
      }



      public void Enable() {
         controller.enabled = true;
         movement.enabled   = true;

         smoke.DisableLoop();

         animator.SetAnim(HeroAnimator.Anim.Fly);
      }

      public void Disable() {
         movement.enabled = false;

         smoke.EnableLoop();

         animator.SetAnim(HeroAnimator.Anim.Idle);
      }

      public void Die() {
         controller.enabled = false;
         movement.enabled   = false;

         smoke.EnableLoop();
         animator.SetAnim(HeroAnimator.Anim.Break);

         _gameManager.EndGame();
      }
   }
}