using System;
using Core.Enums;
using UnityEngine;

namespace Core.Animation
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private AnimationType _currentAnimationType;

        public event Action ActionRequested;
        public event Action AnimationEnded;
        
        public bool PlayAnimation(AnimationType animationType, bool isActive)
        {
            if (!isActive)
            {
                if (_currentAnimationType != AnimationType.Idle && _currentAnimationType == animationType)
                {
                    _currentAnimationType = AnimationType.Idle;
                    PlayAnimation(_currentAnimationType);
                }

                return false;
            }

            if (animationType <= _currentAnimationType)
                return false;
            
            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
            return true;
        }
        
        private void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger("AnimationType", (int)animationType);
        }

        public void UpdateDirection(float value)
        {
            _animator.SetFloat("Direction", value);
        }

        protected void OnActionRequested() => ActionRequested?.Invoke();

        protected void OnAnimationEnded() => AnimationEnded?.Invoke();

    }
}