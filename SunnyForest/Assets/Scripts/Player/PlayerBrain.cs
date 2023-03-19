using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerBrain
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;

        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            _playerEntity.Move(GetDirection());
            if (IsAttack)
                _playerEntity.StartAttack();
            
            foreach (var source in _inputSources)
                source.ResetOneTimeActions();
        }

        private Vector2 GetDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if (inputSource.Direction.magnitude != 0)
                    return inputSource.Direction;
            }

            return new Vector2();
        }

        private bool IsAttack => _inputSources.Any(source => source.Attack);
    }
}