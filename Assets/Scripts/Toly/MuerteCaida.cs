using System.Collections;
using UnityEngine;

public class MuerteCaida : MonoBehaviour
{
    public Animator playerAnimator;         // Animator del jugador
    public float delayBeforeRestart = 1.5f; // Espera antes de mover al jugador

    private bool isRestarting = false;      // Evita múltiples ejecuciones

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player") && !isRestarting)
    {
        isRestarting = true;
        StartCoroutine(HandlePlayerFall(collision.gameObject));
    }
}



    private IEnumerator HandlePlayerFall(GameObject player)
    {
        // Reproduce animación de muerte si hay animator
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Death");
        }

        // Desactiva movimiento
        var mover = player.GetComponent<Mover>();
        if (mover != null) mover.enabled = false;

        // Espera la animación
        yield return new WaitForSeconds(delayBeforeRestart);

        // Reduce la vida usando PlayerHealth
        var health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(1); // Resta una vida

            if (health.currentHealth > 0)
            {
                // Si aún hay vidas, reubica al jugador al punto de inicio
                player.transform.position = Vector3.zero;

                // Reactiva movimiento
                if (mover != null) mover.enabled = true;

                isRestarting = false;
                yield break; // Termina aquí si no ha muerto
            }
        }

        // Si no hay más vidas, el PlayerHealth se encarga de llamar a Die()
    }

    void Update()
    {
        if (!isRestarting && transform.position.y < -12f) // Ajusta el valor -10 según tu nivel
        {
            isRestarting = true;
            StartCoroutine(HandlePlayerFall(gameObject));
        }
    }

}


