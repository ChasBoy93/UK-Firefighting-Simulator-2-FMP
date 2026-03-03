using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.Events;

namespace Artemis
{
    [RequireComponent(typeof(CharacterController))]
    public class FPController : MonoBehaviour
    {
        [Header("Movement Perameters")]
        public float maxSpeed => sprintInput ? sprintSpeed : walkSpeed;
        public float acceleration = 15f;

        [SerializeField] float walkSpeed = 3.5f;
        [SerializeField] float sprintSpeed = 8f;

        [Space(15)]
        [Tooltip("This is how high the character can jump.")]
        [SerializeField] float jumpHeight = 2f;

        private int timesJumped = 0;

        [SerializeField] bool CanDoubleJump = true;
        public bool sprinting
        {
            get
            {
                return sprintInput && currentSpeed > 0.1f;
            }
        }

        [Header("Looking Perameters")]
        public Vector2 lookSensitivity = new Vector2(0.1f, 0.1f);

        public float PitchLimit = 85f;

        [SerializeField] float currentPitch = 0f;

        public float CurrentPitch
        {
            get => currentPitch;

            set
            {
                currentPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
            }
        }

        [Header("Camera Perameters")]
        [SerializeField] float cameraNormalFOV = 60f;
        [SerializeField] float cameraSprintFOV = 80f;
        [SerializeField] float cameraFOVSmoothing = 1f;

        float targetCameraFOV
        {
            get
            {
                return sprinting ? cameraSprintFOV : cameraNormalFOV;
            }
        }

        [Header("Physics Parameters")]
        [SerializeField] float GravityScale = 3f;

        public float verticalVelocity = 0f;

        public Vector3 currentVelocity { get; private set; }
        public float currentSpeed { get; private set; }

        private bool wasGrounded = false;
        public bool IsGrounded => characterController.isGrounded;

        [Header("Input")]
        public Vector2 moveInput;
        public Vector2 lookInput;
        public bool sprintInput;

        [Header("Components")]
        [SerializeField] CinemachineCamera fpCamera;
        [SerializeField] CharacterController characterController;

        [Header("Events")]
        public UnityEvent landed;

        #region Unity Methods

        void OnValidate()
        {
           if(characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
        }

        void Update()
        {
            MoveUpdate();
            LookUpdate();
            CameraUpadate();

            if(!wasGrounded && IsGrounded)
            {
                timesJumped = 0;
                landed?.Invoke();
            }

            wasGrounded = IsGrounded;
        }

        #endregion

        #region Controller Methods

        public void TryJump()
        {
            if (IsGrounded == false)
            {
                if (CanDoubleJump && timesJumped < 2 && verticalVelocity > 0.01f)
                {
                    return;
                }
            }

            if(!CanDoubleJump || timesJumped >= 2)
            {
                return;
            }


            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * GravityScale);

            timesJumped++;
        }

        void MoveUpdate()
        {
            Vector3 motion = transform.forward * moveInput.y + transform.right * moveInput.x;
            motion.y = 0f;
            motion.Normalize();

            if (motion.sqrMagnitude >= 0.01f)
            {
                currentVelocity = Vector3.MoveTowards(currentVelocity, motion * maxSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, acceleration * Time.deltaTime);
            }

            if(IsGrounded && verticalVelocity <= 0.01f)
            {
                verticalVelocity = -3f;
            }
            else
            {
                verticalVelocity += Physics.gravity.y * GravityScale * Time.deltaTime;
            }

            Vector3 fullVelocity = new Vector3(currentVelocity.x, verticalVelocity, currentVelocity.z);

            CollisionFlags flags =  characterController.Move(fullVelocity * Time.deltaTime);

            if ((flags & CollisionFlags.Above) != 0 && verticalVelocity > 0.01f)
            {

                verticalVelocity = 0f;
            }

            //Updating Speed
            currentSpeed = currentVelocity.magnitude;
        }

        void LookUpdate()
        {
            Vector2 input = new Vector2(lookInput.x * lookSensitivity.x, lookInput.y * lookSensitivity.y);

            //Looking Up and Down
            CurrentPitch -= input.y;

            fpCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);


            //Looking Left and Right
            transform.Rotate(Vector3.up * input.x);
        }

        void CameraUpadate()
        {
            float targetFOV = cameraNormalFOV;

            if (sprinting)
            {
                float speedRatio = currentSpeed / sprintSpeed;

                targetFOV = Mathf.Lerp(cameraNormalFOV, cameraSprintFOV, speedRatio);
            }

            fpCamera.Lens.FieldOfView = Mathf.Lerp(fpCamera.Lens.FieldOfView, targetFOV, cameraFOVSmoothing *  Time.deltaTime);
        }

        #endregion
    }
}
