using UnityEngine;

namespace Code.Hero {
   public class HeroAnimator : MonoBehaviour {
      public HeroMovement heroMovement;
      public Transform    view;
      public Animator     animations;

      [Min(0)] public float followFactor;
      [Min(0)] public float followAngle;
      [Min(0)] public float followSpeedFactor;

      private float _angle;


      private void Update() {
         float targetAngle = Mathf.Lerp(-followAngle, followAngle, heroMovement.Velocity * followFactor);
         _angle           = Mathf.Lerp(_angle, targetAngle, followSpeedFactor * Time.deltaTime);
         view.eulerAngles = Vector3.forward * _angle;
      }
   }
}