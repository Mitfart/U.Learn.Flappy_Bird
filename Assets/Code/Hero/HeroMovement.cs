using UnityEngine;

namespace Code.Hero {
   public class HeroMovement : MonoBehaviour {
      private const float _GRAVITY = -9.81f;

      public CharacterController cc;

      [Min(0)] public float jumpHeight   = 4;
      [Min(0)] public float gravityScale = 1;
      [Min(0)] public float maxVelocity  = 10f;

      public float Velocity { get; private set; }



      private void FixedUpdate() {
         if (!cc.isGrounded)
            ApplyGravity();

         ClampVelocity();
         MovePlayer();
      }



      public void Jump() => Velocity = Mathf.Sqrt(jumpHeight * -2f * (_GRAVITY * gravityScale));



      private void ApplyGravity()  => Velocity += _GRAVITY * gravityScale * Time.deltaTime;
      private void ClampVelocity() => Velocity = Mathf.Clamp(Velocity, -maxVelocity, maxVelocity);
      private void MovePlayer()    => cc.Move(Vector3.up * (Velocity * Time.deltaTime));
   }
}