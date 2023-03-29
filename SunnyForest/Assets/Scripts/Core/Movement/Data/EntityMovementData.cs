using System;
using Core.Enums;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class EntityMovementData
    {
        [field: SerializeField] public Direction Direction { get; protected internal set; }
    }
}