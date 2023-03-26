using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;
using StatsSystem.Enums;
using UnityEngine;

namespace StatsSystem
{
    public class StatsController: IDisposable, IStatValueGiver
    {
        private readonly List<Stat> _currentStats;
        private List<StatModificator> _activeModifications;

        public StatsController(List<Stat> stats)
        {
            _currentStats = stats;
            _activeModifications = new List<StatModificator>();
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }

        public float GetStatValue(StatType statType) =>
            _currentStats.Find(stat => stat.Type == statType);

        public void ProcessModificator(StatModificator modificator)
        {
            var statToChange = _currentStats.Find(stat => stat.Type == modificator.Stat.Type);
            
            if (statToChange == null)
                return;

            var addedValue = modificator.Type == StatModificatorType.Additive
                ? statToChange + modificator.Stat
                : statToChange * modificator.Stat;
            
            statToChange.SetStatValue(statToChange + addedValue);
            
            if (modificator.Duration < 0)
                return;

            if (_activeModifications.Contains(modificator))
            {
                _activeModifications.Remove(modificator);
            }
            else
            {
                var addedStat = new Stat(modificator.Stat.Type, - addedValue);
                var tempModificator = new StatModificator(
                    addedStat, StatModificatorType.Additive, modificator.Duration, Time.time
                    );
                _activeModifications.Add(tempModificator);
            }
        }
        
        public void Dispose()
        {
            ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
        }

        private void OnUpdate()
        {
            if(_activeModifications.Count == 0)
                return;

            var expiredModifications = _activeModifications.Where(
                mod => mod.StartTime + mod.Duration >= Time.time);

            foreach (var modificator in expiredModifications) 
            {
                ProcessModificator(modificator);
            }
        }
    }
}