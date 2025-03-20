using System;
using UnityEngine;

namespace Code.Infrastructure {
   [Serializable]
   public class SpeedManager {
      public event Action<float> OnChangeSpeed;

      [field: SerializeField] public float BaseSpeed  { get; private set; } = 1f;
      [field: SerializeField] public float SpeedScale { get; private set; } = 1f;

      

      public void ScaleSpeed(float newScale) {
         SpeedScale = Mathf.Max(0f, newScale);

         OnChangeSpeed?.Invoke(Get());
      }

      public float Get() => BaseSpeed * SpeedScale;
   }
}