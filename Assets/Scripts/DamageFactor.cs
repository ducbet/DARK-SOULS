using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class DamageFactor : MonoBehaviour
    {
        public int damage = 25;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PlayerManager.PLAYER_TAG))
            {
                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                playerStats.TakeDamage(damage);
            }
        }
    }
}
