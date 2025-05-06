using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.Windows;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")] 
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool aim;
        public bool shoot;
        public bool absorb;
        public bool dash;
        public bool actionEncoger;

        private Animator _animator;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [Header("Audio Sources")] // Aqui se declaran los audios
        public AudioSource jumpAudio;
        public AudioSource shootAudio;
        public AudioSource absorbAudio;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            bool isJumping = value.isPressed;
            JumpInput(isJumping);

            if (isJumping && jumpAudio != null && !jumpAudio.isPlaying)
            {
                jumpAudio.Play();
            }
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnAim(InputValue value)
        {
            AimInput(value.isPressed);

            Debug.Log("Aim");
            _animator.SetBool("Aim", true);
            StartCoroutine(ResetBool("Aim"));
        }

        public void OnShoot(InputValue value)
        {
            bool isShooting = value.isPressed;
            ShootInput(isShooting);

            Debug.Log("Shoot");
            _animator.SetBool("Shoot", true);
            StartCoroutine(ResetBool("Shoot"));

            if (isShooting && shootAudio != null)
            {
                shootAudio.Play();
            }
        }

        public void OnAbsorb(InputValue value)
        {
            bool isAbsorbing = value.isPressed;
            AbsorbInput(isAbsorbing);

            Debug.Log("Absorb");
            _animator.SetBool("Absorb", true);
            StartCoroutine(ResetBool("Absorb"));

            if (isAbsorbing && absorbAudio != null)
            {
                absorbAudio.Play();
            }
        }

        public void OnDash(InputValue value)
        {
            DashInput(value.isPressed);

            Debug.Log("Dash");
            _animator.SetBool("Dash", true);
            StartCoroutine(ResetBool("Dash"));
        }

        public void OnEncoger(InputValue value)
        {
            EncogerInput(value.isPressed);
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void AimInput(bool newAimState)
        {
            aim = newAimState;
        }

        public void AbsorbInput(bool newAbsorbState)
        {
            absorb = newAbsorbState;
        }

        public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }

        public void DashInput(bool newdashState)
        {
            dash = newdashState;
        }

        public void EncogerInput(bool newEncogerState)
        {
            actionEncoger = newEncogerState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private System.Collections.IEnumerator ResetBool(string boolName)
        {
            yield return new WaitForSeconds(0.1f);
            _animator.SetBool(boolName, false);
        }
    }
}

