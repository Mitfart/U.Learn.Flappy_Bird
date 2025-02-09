using System;
using UnityEngine;

namespace Code.Infrastructure {
   public class Score : MonoBehaviour {
      public event Action<int> OnChange;

      private int _score;



      public Score Increase(int amount) {
         if (amount <= 0)
            return this;

         _score += amount;

         OnChange?.Invoke(_score);
         return this;
      }

      public Score Reduce(int amount) {
         if (amount >= 0)
            return this;

         _score += amount; // "+", because amount already negative digit

         OnChange?.Invoke(_score);
         return this;
      }

      public int Get() {
         return _score;
      }
   }
}