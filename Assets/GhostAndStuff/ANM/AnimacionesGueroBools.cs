using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AnimacionesGueroBools : MonoBehaviour
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(StarterAssetsInputs))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private StarterAssetsInputs _input;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Update()
        {
            if (_input.shoot)
            {
                StartCoroutine(ActivateBool("isShooting", 0.2f));
                _input.shoot = false;
            }

            if (_input.absorb)
            {
                StartCoroutine(ActivateBool("isAbsorbing", 0.3f));
                _input.absorb = false;
            }

            if (_input.dash)
            {
                StartCoroutine(ActivateBool("isDashing", 0.2f));
                _input.dash = false;
            }
        }

        private IEnumerator ActivateBool(string paramName, float duration)
        {
            _animator.SetBool(paramName, true);
            yield return new WaitForSeconds(duration);
            _animator.SetBool(paramName, false);
        }
    }
}

