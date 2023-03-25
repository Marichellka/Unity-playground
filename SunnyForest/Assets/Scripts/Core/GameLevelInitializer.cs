using System;
using System.Collections.Generic;
using Core.InputReader;
using Core.Services.Updater;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInputView;
        
        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerSystem _playerSystem;
        private ProjectUpdater _projectUpdater;

        private List<IDisposable> _disposables;

        private void Awake()
        {
            if (ProjectUpdater.Instance == null)
                _projectUpdater = new GameObject().AddComponent<ProjectUpdater>();
            else
                _projectUpdater = ProjectUpdater.Instance as ProjectUpdater;

            _disposables = new List<IDisposable>();
            _externalDevicesInput = new ExternalDevicesInputReader();
            _disposables.Add(_externalDevicesInput);

            _playerSystem = new PlayerSystem(_playerEntity, new List<IEntityInputSource>()
            {
                _gameUIInputView,
                _externalDevicesInput
            });
        }

        private void OnDestroy()
        {
            foreach(IDisposable disposable in _disposables)
                disposable.Dispose();
        }
    }
}