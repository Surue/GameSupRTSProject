using System;
using UnityEngine;

namespace CC
{
    public class PlayerWeapon : MonoBehaviour
    {
        private enum AttackPhase
        {
            None,
            Attacking
        }

        private AttackPhase _attackPhase;
        private bool _startAttack;
        private Collider[] _attackColliderBuffer;
        private int _attackDamage;

        private void Awake()
        {
            _attackPhase = AttackPhase.None;
            _attackColliderBuffer = new Collider[10];
        }

        public void Init(int attackDamage)
        {
            _attackDamage = attackDamage;
        }

        public void StartAttack()
        {
            _startAttack = true;
        }

        private void Update()
        {
            switch (_attackPhase)
            {
                case AttackPhase.None:
                    if (_startAttack)
                    {
                        _attackPhase = AttackPhase.Attacking;
                        _startAttack = false;
                    }
                    break;
                case AttackPhase.Attacking:
                    int hitCount = Physics.OverlapBoxNonAlloc(transform.position, Vector3.one, _attackColliderBuffer);

                    if (hitCount > 0)
                    {
                        for (int i = 0; i < hitCount; i++)
                        {
                            if (_attackColliderBuffer[i].TryGetComponent(out Enemy enemy))
                            {
                                enemy.ApplyDamage(_attackDamage);
                            }
                        }
                    }

                    _attackPhase = AttackPhase.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}