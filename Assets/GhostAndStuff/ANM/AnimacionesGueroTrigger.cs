using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AnimacionesGueroTrigger : MonoBehaviour
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
            Debug.Log("Entro");
            if (_input.shoot)
            {
                _animator.SetTrigger("Shoot");
                _input.shoot = false;
            }

            if (_input.absorb)
            {
                _animator.SetTrigger("Absorb");
                _input.absorb = false;
            }

            if (_input.dash)
            {
                _animator.SetTrigger("Dash");
                _input.dash = true;
                
            }
        }
    }
}