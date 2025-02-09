using Code.Infrastructure;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Environment {
   [RequireComponent(typeof(BoxCollider))]
   public class ObstaclePassChecker : MonoBehaviour {
      private BoxCollider _collider;
      private Transform   _body;
      private Score       _score;
      private int         _reward;



      private void Awake() {
         _body               = transform;
         _collider           = GetComponent<BoxCollider>();
         _collider.isTrigger = true;
      }



      public ObstaclePassChecker SetSize(float height, float width = 1f, float deapth = 1f) {
         _body.localPosition = Vector3.zero;
         _body.rotation      = quaternion.identity;

         _collider.center = Vector3.up * (height * .5f);
         _collider.size   = new Vector3(width, height, deapth);
         return this;
      }

      private void OnTriggerExit(Collider other) {
         if (!other.TryGetComponent(out Hero.Hero _))
            return;

         _score.Increase(_reward);
      }

      public void SetReward(Score score, int reward = 1) {
         _reward = reward;
         _score  = score;
      }
   }
}