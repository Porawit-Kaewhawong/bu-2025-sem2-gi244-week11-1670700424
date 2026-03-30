using System.Collections;
using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    public float coolDownTime = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.isStunActive(coolDownTime);

                Destroy(gameObject);
            }
        }
    }
}
