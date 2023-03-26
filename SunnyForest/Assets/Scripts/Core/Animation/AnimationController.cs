using Core.Enums;
using UnityEngine;

namespace Core.Animation
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private AnimationType _currentAnimationType;
        
        public void PlayAnimation(AnimationType animationType, bool isActive)
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

        public void UpdateDirection(float value)
        {
            _animator.SetFloat("Direction", value);
        }

    }
}