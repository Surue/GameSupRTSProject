using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CC
{
    public class EnemyManager : MonoBehaviour
    {
        private enum State
        {
            Idle,
            SpawningEnemies,
            WaitEndWave,
            EndWave,
            Over
        }
        
        [SerializeField] private List<EnemyWave> _waves;
        
        private State _state = State.Idle;
        private int _currentWave = 0;
        private int _currentEnemyGroup = 0;
        private float _lastWaveSpawnTime;
        private int _currentEnemyCount = 0;

        private Transform _playerTransform;

        private void Start()
        {
            _currentWave = 0;
            _lastWaveSpawnTime = Time.time;
            _playerTransform = FindAnyObjectByType<Player>().transform;
            _currentEnemyCount = 0;
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Idle:
                    _state = State.SpawningEnemies;
                    break;
                case State.SpawningEnemies:
                    EnemyGroup enemyGroup = _waves[_currentWave].EnemyGroup[_currentEnemyGroup];
                    if (Time.time - _lastWaveSpawnTime > enemyGroup.intervalBeforeThisWaveInSeconds)
                    {
                        // Spawn all enemies
                        for (int i = 0; i < enemyGroup.enemies.Count; i++)
                        {
                            Vector3 randomSpawnPosition = _playerTransform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)); // TODO remove magical number
                            Enemy instance = Instantiate(enemyGroup.enemies[i], randomSpawnPosition, Quaternion.identity);
                            instance.deathEvent.AddListener(OnEnemyDeath);
                            _currentEnemyCount++;
                            
                        }

                        _currentEnemyGroup++;
                        _lastWaveSpawnTime = Time.time;
                    }

                    if (_currentEnemyGroup >= _waves[_currentWave].EnemyGroup.Count)
                    {
                        _state = State.WaitEndWave;
                    }
                    break;
                case State.WaitEndWave:
                    if (_currentEnemyCount <= 0)
                    {
                        _state = State.EndWave;
                    }
                    break;
                case State.EndWave:
                    _currentWave++;
                    _currentEnemyGroup = 0;

                    if (_currentWave < _waves.Count)
                    {
                        _state = State.SpawningEnemies;
                    }
                    else
                    {
                        _state = State.Over;
                    }
                    
                    break;
                case State.Over:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnemyDeath()
        {
            _currentEnemyCount--;
        }
    }
}