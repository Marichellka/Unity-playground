using System;
using Core.Enums;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class EntityMovementData
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}