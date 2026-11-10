using UnityEngine;
using UnityEngine.InputSystem;

namespace CC
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private float _movementSpeed;
        
        [Header("Attack")]
        [SerializeField] private InputActionReference _attackAction;
        [SerializeField] private int _attackDamage;
        [SerializeField] private PlayerWeapon _weaponGameObject;

        private void Awake()
        {
            _weaponGameObject.Init(_attackDamage);
        }
        
        private void Update()
        {
            Move();

            if (_attackAction.action.IsPressed())
            {
                Attack();
            }
        }

        private void Attack()
        {
            _weaponGameObject.StartAttack();
        }

        private void Move()
        {
            Vector2 inputDirection = _moveAction.action.ReadValue<Vector2>();
            transform.position +=  new Vector3(inputDirection.x, 0, inputDirection.y) * _movementSpeed * Time.deltaTime;
        }
    }
}
