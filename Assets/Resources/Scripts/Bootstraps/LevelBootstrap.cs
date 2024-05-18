using System;
using Resources.Scripts.Canvas.Bars;
using Resources.Scripts.LevelGenerate;
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
        }
    }
}
