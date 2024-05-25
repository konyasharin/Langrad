using System;
using Resources.Scripts.LevelGenerate;
using Resources.Scripts.UI;
using Resources.Scripts.UI.Bars;
using Resources.Scripts.UI.Inventory;
using UnityEngine;

namespace Resources.Scripts.Bootstraps
{
    [RequireComponent(typeof(LevelGenerator))]
    public class LevelBootstrap : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        
        private void Awake()
        {
            _levelGenerator = GetComponent<LevelGenerator>();
        }

        private void Start()
        {
            _levelGenerator.Generate();
            HealthBar.Instance.Initialize();
            ArmorBar.Instance.Initialize();
            ManaBar.Instance.Initialize();
            DeathWindow.Instance.Initialize();
            QuickAccessInventoryDisplay.Instance.Initialize();
        }
    }
}
