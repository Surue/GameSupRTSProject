using System;
using System.Collections.Generic;
using UnityEngine;

namespace CC
{
    [Serializable]
    public struct EnemyGroup
    {
        public List<Enemy> enemies;
        public float intervalBeforeThisWaveInSeconds;
    }
    
    [CreateAssetMenu(menuName = "Scriptable Objects/CC/Create EnemyWave", fileName = "EnemyWave", order = 0)]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private List<EnemyGroup> _enemyGroup;
        
        public List<EnemyGroup> EnemyGroup => _enemyGroup;
    }
}