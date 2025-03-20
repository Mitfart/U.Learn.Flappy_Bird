using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Generic {
   public abstract class UICtr : MonoBehaviour {
      public UIDocument view;

      protected          VisualElement         Root;
      protected abstract IEnumerable<RepeatAnimation> RepeatableAnimations { get; }



      private void OnEnable() {
         Root = view.rootVisualElement;

         InitUI();
         RegEvents();

         RepeatableAnimations.RegisterFor(Root);
      }

      private void OnDisable() {
         UnregEvents();

         RepeatableAnimations.UnregisterAll();
      }



      protected virtual void InitUI()      { }
      protected virtual void RegEvents()   { }
      protected virtual void UnregEvents() { }
   }
}