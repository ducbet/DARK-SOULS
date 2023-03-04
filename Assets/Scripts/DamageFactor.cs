using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(Collider))]
    public class DamageFactor : MonoBehaviour
    {
        public int damage = 0;
        private Collider damageCollider;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        public void SetDamage(int _damage)
        {
            damage = _damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PlayerManager.PLAYER_TAG) || other.CompareTag(EnemyManager.ENEMY_TAG))
            {
                CharacterStats characterStats = other.GetComponent<CharacterStats>();
                characterStats.TakeDamage(damage);
            }
        }
    }
}
