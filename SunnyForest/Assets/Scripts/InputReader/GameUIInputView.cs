using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Core.InputReader
{
    public class GameUIInputView : MonoBehaviour, IEntityInputSource
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _attackButton;
        
        public Vector2 Direction => new Vector2(_joystick.Horizontal, _joystick.Vertical);
        public bool Attack { get; private set; }

        private void Awake()
        {
            _attackButton.onClick.AddListener(() => Attack = true);
        }

        private void OnDestroy()
        {
            _attackButton.onClick.RemoveAllListeners();
        }
        
        public void ResetOneTimeActions()
        {
            Attack = false;
        }
    }
}