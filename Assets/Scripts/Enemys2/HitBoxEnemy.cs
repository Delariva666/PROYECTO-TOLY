using UnityEngine;
using System.Collections;

public class HitBoxEnemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(1);
            // ⚠️ Evita múltiples daños instantáneos
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(ReactivateCollider()); // Opcional: reactivar después de un tiempo
        }
    }
}

IEnumerator ReactivateCollider()
{
    yield return new WaitForSeconds(1f); // Ajusta según el diseño
    GetComponent<Collider2D>().enabled = true;
}

}

