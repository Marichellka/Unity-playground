using UnityEngine;

namespace Player
{
    public interface IEntityInputSource
    {
        Vector2 Direction { get; }
        bool Attack { get; }

        void ResetOneTimeActions();
    }
}