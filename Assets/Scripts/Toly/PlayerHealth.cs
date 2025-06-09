using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public Slider healthSlider;
    public GameObject gameOverText; // 👈 arrastra tu texto Game Over desde la jerarquía
    public float restartDelay = 2f;
    public HeartManager heartManager; // 👈 conecta aquí tu script de corazones

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (gameOverText != null)
            gameOverText.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthSlider.value = currentHealth;

        // Actualizar corazones visualmente
        if (heartManager != null)
            heartManager.UpdateHearts(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡Jugador sin vidas!");
        if (gameOverText != null)
            gameOverText.SetActive(true);

        // Ocultar al jugador
        gameObject.SetActive(false);

        // Reiniciar escena después de un delay
        Invoke(nameof(RestartLevel), restartDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}





