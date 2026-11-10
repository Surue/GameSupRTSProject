using UnityEngine;
using UnityEngine.InputSystem;

namespace GS
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed;

        [Header("Attack")] 
        [SerializeField] private float _firingIntervalInSeconds;
        
        [Header("Inputs")] 
        [SerializeField] private InputActionReference  _moveActionRef;
        [SerializeField] private InputActionReference  _fireActionRef;

        private float _lastFiringTimeInSeconds;
        
        private void Start()
        {
            _lastFiringTimeInSeconds = Time.time;
        }

        private void Update()
        {
            // Fire
            if (_fireActionRef.action.IsPressed() && _lastFiringTimeInSeconds + _firingIntervalInSeconds < Time.time)
            {
                _lastFiringTimeInSeconds = Time.time;
                Debug.Log("Fire");
            }
            
            // Movement
            Vector2 inputVector = _moveActionRef.action.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            transform.Translate(moveDirection * _moveSpeed * Time.deltaTime);
        }
    }
}