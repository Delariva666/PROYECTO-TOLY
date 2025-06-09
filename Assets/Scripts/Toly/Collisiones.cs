using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collisiones : MonoBehaviour
{
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public int maxHealth = 5; // Máximo número de corazones
    private int currentHealth; // Salud actual
    public float recoveryTime = 1.5f; // Tiempo de recuperación tras cada colisión
    private bool canCollide = true; // Controla si el jugador puede colisionar
    public Image[] corazones; // Arreglo de imágenes (corazones)
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    public GameObject gameOverPanel;



    public bool IsDead => currentHealth <= 0; // Verifica si el jugador está muerto

    BoxCollider2D col2D;
    Toly toly;

    private void Awake()
    {
        col2D = GetComponent<BoxCollider2D>();
        toly = GetComponent<Toly>();
        currentHealth = maxHealth; // Salud inicial al máximo
    }

    public bool Grounded()
    {
        Vector2 footLeft = new Vector2(col2D.bounds.center.x - col2D.bounds.extents.x, col2D.bounds.center.y);
        Vector2 footRight = new Vector2(col2D.bounds.center.x + col2D.bounds.extents.x, col2D.bounds.center.y);

        Debug.DrawRay(footLeft, Vector2.down * col2D.bounds.extents.y * 1.5f, Color.magenta);
        Debug.DrawRay(footRight, Vector2.down * col2D.bounds.extents.y * 1.5f, Color.magenta);

        if (Physics2D.Raycast(footLeft, Vector2.down, col2D.bounds.extents.y * 1.5f, groundLayer) ||
            Physics2D.Raycast(footRight, Vector2.down, col2D.bounds.extents.y * 1.5f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        return isGrounded;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Colisioné con: {collision.gameObject.name}, capa: {LayerMask.LayerToName(collision.gameObject.layer)}");

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && canCollide)
        {
            StartCoroutine(HandleCollision());
        }
    }



    private IEnumerator HandleCollision()
    {
        canCollide = false; // Desactiva colisiones adicionales durante el tiempo de recuperación
        currentHealth--; // Reduce la salud en 1
        ActualizarCorazones();

        Debug.Log($"¡Colisión con enemigo! Salud restante: {currentHealth}");

        toly.Hit(); // Ejecuta la lógica del impacto

        if (IsDead)
        {
            Dead(); // Llama a Dead() si la salud llega a 0
        }
        else
        {
            // Espera el tiempo de recuperación antes de permitir nuevas colisiones
            yield return new WaitForSeconds(recoveryTime);
            canCollide = true;
        }
    }

    public void Dead()
    {
        if (IsDead)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerDead");
            Debug.Log("¡El jugador ha muerto!");
            // Desactivar movimiento
            toly.DisableMovement();
            // 🔴 Mostrar el panel de Game Over
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            else
            {
                Debug.LogWarning("GameOverPanel no está asignado en el Inspector.");
            }
            // Inicia el reinicio de escena
            StartCoroutine(RestartScene());
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHit playerHit = collision.GetComponent<PlayerHit>();
        if (playerHit != null)
        {
            playerHit.Hit();
        }
    }

    private IEnumerator RestartScene()
    {
        // Espera 2 segundos antes de reiniciar
        yield return new WaitForSeconds(2f);

        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ActualizarCorazones()
{
    for (int i = 0; i < corazones.Length; i++)
    {
        if (i < currentHealth)
        {
            corazones[i].sprite = corazonLleno;
        }
        else
        {
            corazones[i].sprite = corazonVacio;
        }
    }
}


}
