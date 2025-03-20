using System;
using UnityEngine;

namespace Code.Hero {
   public class HeroAnimator : MonoBehaviour {
      public HeroMovement heroMovement;
      public Transform    view;
      public Animator     Animator;

      [Min(0)] public float followFactor;
      [Min(0)] public float followAngle;
      [Min(0)] public float followSpeedFactor;

      [Range(0f, 1f)] public float idleHeight;
      [Min(.000_1f)]  public float idleSpeed;

      private Anim  _anim = Anim.Idle;
      private float _angle;



      private void Update() {
         switch (_anim) {
            case Anim.Idle:
               IdleAnim();
               break;
            case Anim.Fly:
               RotateTowardMoveDir();
               break;
            case Anim.None:
            case Anim.Break:
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }
      }



      public void SetAnim(Anim anim) {
         _anim = anim;

         if (_anim == Anim.Break)
            Animator.enabled = false;
      }



      private void IdleAnim() {
         float t = Time.time * idleSpeed % 1f * 360f;
         transform.SetPositionY(Mathf.Sin(t)  * idleHeight);
      }

      private void RotateTowardMoveDir() {
         float targetAngle = Mathf.Lerp(-followAngle, followAngle, heroMovement.Velocity * followFactor);
         _angle           = Mathf.Lerp(_angle, targetAngle, followSpeedFactor * Time.deltaTime);
         view.eulerAngles = Vector3.forward * _angle;
      }



      public enum Anim {
         None,
         Idle,
         Fly,
         Break
      }
   }
}