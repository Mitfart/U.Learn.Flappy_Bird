using UnityEngine;

namespace Code.Environment {
   public class Coin : MonoBehaviour {
      private void OnTriggerEnter(Collider other) {
         if (!other.TryGetComponent(out Hero.Hero _))
            return;

         Destroy(gameObject);
      }

      public void Move(float speed) => transform.Translate(Vector2.left * speed * Time.deltaTime);
   }
}