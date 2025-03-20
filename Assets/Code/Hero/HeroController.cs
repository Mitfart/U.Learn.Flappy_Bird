using System.Linq;
using UnityEngine;

namespace Code.Hero {
   public class HeroController : MonoBehaviour {
      public HeroMovement movement;
      public KeyCode[]    jumpKeys = { KeyCode.Space, KeyCode.Z, KeyCode.KeypadEnter, KeyCode.Mouse0 };

      private void Update() {
         if (AnyJumpKeyDown())
            movement.Jump();
      }

      private bool AnyJumpKeyDown() => jumpKeys.Any(Input.GetKeyDown);
   }
}