using System;
using UnityEngine;

namespace Code.Infrastructure {
   [Serializable]
   public class Score {
      public event Action<int> OnChange;

      [SerializeField] private int score;



      public Score Increase(int amount) {
         if (amount <= 0)
            return this;

         score += amount;

         OnChange?.Invoke(score);
         return this;
      }

      public Score Reduce(int amount) {
         if (amount >= 0)
            return this;

         score += amount; // "+", because amount already negative digit

         OnChange?.Invoke(score);
         return this;
      }

      public int Get() => score;
   }
}