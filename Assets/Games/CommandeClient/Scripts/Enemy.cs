using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace CC
{
    public class Enemy : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int _initialHealthPoint = 0;
        
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _stopDistance = 1f;

        private Transform _target;
        private EnemyManager _enemyManager;

        private int _currentHealthPoint;

        [HideInInspector]
        public UnityEvent deathEvent;
        
        private void Start()
        {
            _target = FindAnyObjectByType<Player>().transform;
            _currentHealthPoint = _initialHealthPoint;
        }
        
        private void Update()
        {
            Vector3 direction = _target.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= _stopDistance)
            {
                return;
            }
            
            Vector3 directionNormalized = direction.normalized;
            
            
            transform.position += directionNormalized * _moveSpeed * Time.deltaTime;
        }
        
        public void ApplyDamage(int damage)
        {
            _currentHealthPoint -= damage;

            if (_currentHealthPoint <= 0)
            {
                Debug.Log("Die");
                Die();
            }
        }

        private void Die()
        {
            deathEvent.Invoke();
            Destroy(gameObject);
        }
    }
}