using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Scripts.Actors.Player
{
    public class ManaController : MonoBehaviour
    {
        public static ManaController Instance { get; private set; }
        [field: SerializeField]
        public int Mana { get; private set; }
        [SerializeField, Min(1)]
        private int restoreValue;
        [SerializeField, Min(0.5f)] 
        private float restoreTime;
        private int _maxMana;
        private Coroutine _currentRestore;

        private void Awake()
        {
            Instance = this;
            _maxMana = Mana;
        }

        public IEnumerator Restore()
        {
            while (Mana < _maxMana)
            {
                Change(restoreValue);
                yield return new WaitForSeconds(restoreTime);
            }
        }
        
        public void Change(int manaDelta)
        {
            if (manaDelta != 0)
            {
                if (Mana + manaDelta < 0)
                {
                    Mana = 0;
                } else if (Mana + manaDelta > _maxMana)
                {
                    Mana = _maxMana;
                }
                else
                {
                    Mana += manaDelta;
                }
                PlayerCharacter.Instance.OnUpdateStat.Invoke();
            }

            if (manaDelta < 0)
            {
                if (_currentRestore != null)
                {
                    StopCoroutine(_currentRestore);   
                }
                _currentRestore = StartCoroutine(Restore());
            }
        }
    }
}