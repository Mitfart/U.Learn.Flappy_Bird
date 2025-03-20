using System;
using System.Collections.Generic;
using System.Linq;
using UI.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code {
   public static class Extentions {
      public static Vector3 SetPositionX(this Transform t, float value) {
         Vector3 pos = t.position;
         pos.x      = value;
         t.position = pos;
         return pos;
      }

      public static Vector3 SetPositionY(this Transform t, float value) {
         Vector3 pos = t.position;
         pos.y      = value;
         t.position = pos;
         return pos;
      }

      public static Vector3 SetPositionZ(this Transform t, float value) {
         Vector3 pos = t.position;
         pos.z      = value;
         t.position = pos;
         return pos;
      }



      public static float GetTransitionDurationInSeconds(this VisualElement element) => element.resolvedStyle.transitionDuration.GetDurationInSeconds();

      public static float GetDurationInSeconds(this IEnumerable<TimeValue> transitions) {
         TimeValue transition = transitions.First();

         return transition.unit switch {
            TimeUnit.Second      => transition.value,
            TimeUnit.Millisecond => transition.value * .000_1f,
            _                    => throw new ArgumentOutOfRangeException()
         };
      }



      public static void RegRepeatAnim(this VisualElement el, RepeatAnimation anim) {
         float duration;

         anim.SetEnabled(true);

         RegAnim();
         return;

         async void RegAnim() {
            if (el.enabledInHierarchy && anim.Enabled) {
               duration = el.resolvedStyle.transitionDuration.GetDurationInSeconds() + Mathf.Max(0f, anim.Delay);
               el.ToggleInClassList(anim.USSClass);

               await Awaitable.WaitForSecondsAsync(duration);
               RegAnim();
            }
         }
      }

      public static void RegisterFor(this IEnumerable<RepeatAnimation> anims, VisualElement root) {
         foreach (RepeatAnimation anim in anims)
            root.Q(anim.ID).RegRepeatAnim(anim);
      }

      public static void UnregisterAll(this IEnumerable<RepeatAnimation> anims) {
         foreach (RepeatAnimation el in anims)
            el.SetEnabled(false);
      }
   }
}