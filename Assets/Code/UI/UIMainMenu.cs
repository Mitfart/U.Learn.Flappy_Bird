using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UI {
   public class UIMainMenu : MonoBehaviour {
      public UIDocument document;


      private Button _btnTest;

      private void Awake()     => _btnTest = document.rootVisualElement.Q<Button>("btn_test");
      private void OnEnable()  => _btnTest.RegisterCallback<ClickEvent>(TestClick);
      private void OnDisable() => _btnTest.UnregisterCallback<ClickEvent>(TestClick);

      private void TestClick(ClickEvent evt) {
         Debug.Log("TEST!!");
      }
   }
}