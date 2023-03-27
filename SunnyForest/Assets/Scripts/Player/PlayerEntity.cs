using Core.Animation;
using Core.Movement.Controller;
using Core.Movement.Data;
using StatsSystem;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private EntityMovementData _movementData;
        [SerializeField] private AnimationController _animationController;

        private Rigidbody2D _rigidbody;

        private EntityMover _entityMover;

        public void Initialize(IStatValueGiver statValueGiver)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _entityMover = new EntityMover(_rigidbody, _movementData, statValueGiver);
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
            _animationController.UpdateDirection((float)_movementData.Direction);
            _animationController.PlayAnimation(AnimationType.Idle, true);
            _animationController.PlayAnimation(AnimationType.Walk, _entityMover.IsMoving);
        }

        public void StartAttack()
        {
            if (!_animationController.PlayAnimation(AnimationType.Attack, true))
                return;

            _animationController.ActionRequested += Attack;
            _animationController.AnimationEnded += EndAttack;
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }

        private void EndAttack()
        {
            _animationController.ActionRequested -= Attack;
            _animationController.AnimationEnded -= EndAttack;
            _animationController.PlayAnimation(AnimationType.Attack, false);
        }
    }
}
