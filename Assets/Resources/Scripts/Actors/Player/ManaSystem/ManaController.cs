using System.Collections;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.Services;
using Resources.Scripts.Utils;
using UnityEngine;

namespace Resources.Scripts.Actors.Player.ManaSystem
{
    public class ManaController
    {
        public int Mana { get; private set; }
        private Coroutine _currentRestore;
        private readonly PlayerCharacter _player = ServiceLocator.Instance.Get<PlayerCharacter>();
        private readonly CoroutinesManager _coroutinesManager;
        private readonly ManaSettings _manaSettings;

        public ManaController(ManaSettings manaSettings)
        {
            _manaSettings = manaSettings;
            _coroutinesManager = ServiceLocator.Instance.Get<CoroutinesManager>();
            Mana = _manaSettings.maxMana;
        }

        private IEnumerator Restore()
        {
            while (Mana < _manaSettings.maxMana)
            {
                Change(_manaSettings.restoreValue);
                yield return new WaitForSeconds(_manaSettings.restoreTime);
            }
        }
        
        public void Change(int manaDelta)
        {
            if (manaDelta != 0)
            {
                if (Mana + manaDelta < 0)
                {
                    Mana = 0;
                } else if (Mana + manaDelta > _manaSettings.maxMana)
                {
                    Mana = _manaSettings.maxMana;
                }
                else
                {
                    Mana += manaDelta;
                }
                _player.OnUpdateStat.Invoke();
            }

            if (manaDelta >= 0) return;
            
            if (_currentRestore != null)
            {
                _coroutinesManager.StopCoroutineHandle(_currentRestore);
            }
            
            _currentRestore = _coroutinesManager.StartCoroutineHandle(Restore());
        }
    }
}