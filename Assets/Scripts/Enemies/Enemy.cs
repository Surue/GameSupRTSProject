using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GS
{
    [RequireComponent(typeof(WayPointFollower))]
    public class Enemy : MonoBehaviour
    {
#region SerializeFields
        [SerializeField] private EnemyData _enemyData;

        [Header("UI")]
        [SerializeField] private Image _healthBarImage;
#endregion

        // Health
        private int _currentHealthPoint = 0;
        
        private WayPointFollower _wayPointFollower;

        private void Awake()
        {
            _currentHealthPoint = _enemyData.InitialHealthPoint;
            _wayPointFollower = GetComponent<WayPointFollower>();
            
            _wayPointFollower.SetEnemyData(_enemyData);
            
            UpdateHealthBarUI();
        }

        private void Update()
        {
            InputAction jumpAction = InputSystem.actions.FindAction("Jump");

            if (jumpAction.IsPressed())
            {
                Debug.Log("Jump");
            }

            InputAction moveAction = InputSystem.actions.FindAction("Move");
            Vector2 moveVector = moveAction.ReadValue<Vector2>();
            Debug.Log("moveVector: " + moveVector);
        }

        public void SetPath(WaypointPath waypointPath)
        {
            _wayPointFollower.SetPath(waypointPath);
        }

        public virtual void TakeDamage(int damage)
        {
            _currentHealthPoint -= damage;
            
            UpdateHealthBarUI();

            if (_currentHealthPoint <= 0)
            {
                Die();
            }
        }

        private void UpdateHealthBarUI()
        {
            _healthBarImage.fillAmount = _currentHealthPoint / (float)_enemyData.InitialHealthPoint;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            HUD hud = FindAnyObjectByType<HUD>();
            hud.DecreaseEnemyCount();
        }
    }
}
