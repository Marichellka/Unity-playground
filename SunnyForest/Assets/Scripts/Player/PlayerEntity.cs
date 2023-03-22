using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector2 direction)
        {
            UpdateAnimation(direction);
            _rigidbody.MovePosition(_rigidbody.position + direction * _moveSpeed * Time.fixedDeltaTime);
        }

        private void UpdateAnimation(Vector2 direction)
        {
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
            _animator.SetFloat("Speed", direction.sqrMagnitude);
            
            if (Math.Abs(direction.x) > 0.99 || Math.Abs(direction.y) > 0.99)
            {
                _animator.SetFloat("FaceHorizontal", direction.x);
                _animator.SetFloat("FaceVertical", direction.y);
            }
        }

        public void StartAttack()
        {
            _animator.SetBool("IsAttack", true);
        }

        public void EndAttack()
        {
            _animator.SetBool("IsAttack", false);
        }
    }
}
