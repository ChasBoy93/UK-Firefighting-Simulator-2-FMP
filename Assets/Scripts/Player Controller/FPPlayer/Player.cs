using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Artemis
{
    [RequireComponent(typeof(FPController))]
    public class Player : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] FPController FPController;

        #region Input Handling

        void OnMove(InputValue value)
        {
            FPController.moveInput = value.Get<Vector2>();
        }

        void OnLook(InputValue value)
        {
            FPController.lookInput = value.Get<Vector2>();
        }

        void OnSprint(InputValue value)
        {
            FPController.sprintInput = value.isPressed;
        }

        void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                FPController.TryJump();
            }
        }

        #endregion

        #region Unity Methods
        void OnValidate()
        {
           if (FPController == null)
            {
                FPController = GetComponent<FPController>();
            }
        }

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion
    }
}
