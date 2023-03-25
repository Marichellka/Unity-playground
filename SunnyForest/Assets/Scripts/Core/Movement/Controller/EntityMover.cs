using Core.Enums;
using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class EntityMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly EntityMovementData _movementData;

        private Vector2 _movement;
        
        public Direction FaceDirection { get; private set; }
        public bool IsMoving => _movement.magnitude > 0;

        public EntityMover(Rigidbody2D rigidbody2D, EntityMovementData movementData)
        {
            _rigidbody = rigidbody2D;
            _movementData = movementData;
        }
        
        public void Move(Vector2 direction)
        {
            _movement = direction;
            SetDirection(direction);
            _rigidbody.MovePosition(_rigidbody.position + direction * _movementData.MoveSpeed * Time.fixedDeltaTime);
        }

        private void SetDirection(Vector2 direction)
        {
            if (direction.y > 0)
                FaceDirection = Direction.Up;
            else if (direction.y < 0)
                FaceDirection = Direction.Down; 
            else if (direction.x > 0)
                FaceDirection = Direction.Right;
            else if (direction.x < 0)
                FaceDirection = Direction.Left;
        }
    }
}