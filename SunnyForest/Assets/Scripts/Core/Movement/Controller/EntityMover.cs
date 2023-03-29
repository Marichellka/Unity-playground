using System;
using Core.Enums;
using Core.Movement.Data;
using StatsSystem;
using StatsSystem.Enums;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class EntityMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly EntityMovementData _movementData;
        private readonly IStatValueGiver _statValueGiver;

        private Vector2 _movement;
        public bool IsMoving => _movement.magnitude > 0;

        public EntityMover(Rigidbody2D rigidbody2D, EntityMovementData movementData, IStatValueGiver statValueGiver)
        {
            _rigidbody = rigidbody2D;
            _movementData = movementData;
            _statValueGiver = statValueGiver;
        }
        
        public void Move(Vector2 direction)
        {
            _movement = direction;
            SetDirection(direction);
            _rigidbody.MovePosition(_rigidbody.position +
                                    direction * _statValueGiver.GetStatValue(StatType.Speed) * Time.fixedDeltaTime);
        }

        private void SetDirection(Vector2 direction)
        {
            if (Math.Abs(direction.y) > Math.Abs(direction.x))
            {
                if (direction.y > 0)
                    _movementData.Direction = Direction.Up;
                else if (direction.y < 0)
                    _movementData.Direction = Direction.Down; 
            }
            else
            {
                if (direction.x > 0)
                    _movementData.Direction = Direction.Right;
                else if (direction.x < 0)
                    _movementData.Direction = Direction.Left;
            }
        }
    }
}