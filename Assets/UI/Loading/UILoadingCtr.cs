using System.Collections.Generic;
using System.Threading.Tasks;
using Code;
using UI.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Loading {
   public class UILoadingCtr : UICtr {
      private const string _ID        = "loading";
      private const string _HIDDEN_CL = "loading_hidden";

      public List<RepeatAnimation> repeatableAnimations;

      protected override IEnumerable<RepeatAnimation> RepeatableAnimations => repeatableAnimations;

      private VisualElement _main;

      

      protected override void InitUI() => _main = Root.Q(_ID);

      public async Task Show() {
         _main.RemoveFromClassList(_HIDDEN_CL);
         await Awaitable.WaitForSecondsAsync(_main.GetTransitionDurationInSeconds());
      }

      public async Task Hide() {
         _main.AddToClassList(_HIDDEN_CL);
         await Awaitable.WaitForSecondsAsync(_main.GetTransitionDurationInSeconds());
      }
   }
}