using IFramework.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IFramework.Test.InputSystem
{
    public class Player : MonoBehaviour
    {
        private MyInput myInput;

        private void Awake()
        {
            myInput = new MyInput();
        }

        private void OnEnable()
        {
            myInput.Enable();
            myInput.GamePlay.Jump.performed += JumpOnperformed;
            myInput.GamePlay.Move.performed += MoveOnperformed;
        }

        private void MoveOnperformed(InputAction.CallbackContext obj)
        {
            obj.ReadValue<Vector2>().LogInfo();
        }

        private void JumpOnperformed(InputAction.CallbackContext obj)
        {
            gameObject.transform.Translate(Vector3.up);
        }

        private void OnDisable()
        {
            myInput.GamePlay.Jump.performed -= JumpOnperformed;
            myInput.GamePlay.Move.performed -= MoveOnperformed;
            myInput.Disable();
        }

        // Update is called once per frame
        private void Update() { }
    }
}
