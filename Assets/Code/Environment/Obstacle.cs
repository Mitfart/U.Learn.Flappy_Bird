using UnityEngine;

namespace Code.Environment {
   public class Obstacle : MonoBehaviour {
      public ObstaclePassChecker checker;



      private void OnTriggerEnter(Collider other) {
         if (other.TryGetComponent(out Hero.Hero hero))
            hero.Die();
      }



      public void Move(float speed) => transform.position += Vector3.left * (speed * Time.deltaTime);

      public void DestroySelf() => Destroy(gameObject);
   }
}