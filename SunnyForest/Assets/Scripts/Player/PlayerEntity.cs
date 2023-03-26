using Core.Enums;
using Core.Movement.Controller;
using Core.Movement.Data;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private EntityMovementData _movementData;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private EntityMover _entityMover;
        private AnimationType _currentAnimationType;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _entityMover = new EntityMover(_rigidbody, _movementData);
        }

        private void Update()
        {
            UpdateAnimations();
        }

        public void Move(Vector2 direction)
        {
            _entityMover.Move(direction);
        }

        private void UpdateAnimations()
        {
            _animator.SetFloat("Direction", (float)_entityMover.FaceDirection);
            PlayAnimation(AnimationType.Idle, true);
            PlayAnimation(AnimationType.Walk, _entityMover.IsMoving);
        }

        public void StartAttack()
        {
            // TODO: implement attack logic
            return;
        }
        
        private void PlayAnimation(AnimationType animationType, bool isActive)
        {
            if (!isActive)
            {
                if (_currentAnimationType != AnimationType.Idle && _currentAnimationType == animationType)
                    _currentAnimationType = AnimationType.Idle;
            }
            else if (animationType > _currentAnimationType)
            {
                _currentAnimationType = animationType;
            }
            
            PlayAnimation(_currentAnimationType);
        }

        private void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger("AnimationType", (int)animationType);
        }
    }
}
