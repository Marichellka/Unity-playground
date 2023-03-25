using System;
using Core.Services.Updater;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.InputReader
{
    public class ExternalDevicesInputReader : IEntityInputSource, IDisposable
    {
        public Vector2 Direction => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        public bool Attack { get; private set; }

        public ExternalDevicesInputReader()
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }
        
        public void ResetOneTimeActions()
        {
            Attack = false;
        }

        public void Dispose()
        {
            ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
        }
        
        private void OnUpdate()
        {
            if (!IsPointerOverUI() && Input.GetButtonDown("Fire1"))
                Attack = true;
        }

        private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    }
}