using System;
using UnityEngine;

namespace UI.Generic {
   [Serializable]
   public struct RepeatAnimation {
      [field: SerializeField] public string ID       { get; set; }
      [field: SerializeField] public string USSClass { get; set; }
      [field: SerializeField] public float  Delay    { get; set; }
      [field: SerializeField] public bool   Enabled  { get; set; }



      public RepeatAnimation(string id, string ussClass, float delay = 0f, bool enabled = false) {
         ID       = id;
         USSClass = ussClass;
         Delay    = delay;
         Enabled  = enabled;
      }



      public void SetEnabled(bool enabled) {
         Enabled = enabled;
      }
   }
}