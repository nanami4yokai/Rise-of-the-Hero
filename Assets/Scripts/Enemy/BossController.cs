using UnityEngine;

public class BossController : MonoBehaviour
{
    private AudioManager audioManager;
    private bool isBossFightStarted = false;
    private Enemy_AI enemyAI;
    private Enemy_Combat enemyCombat;

    void Start()
    {
        audioManager = AudioManager.instance;
        enemyAI = GetComponent<Enemy_AI>();
        enemyCombat = GetComponent<Enemy_Combat>();
    }

    void Update()
    {
        if (!isBossFightStarted && IsPlayerInRadius())
        {
            StartBossFight();
        }

        if (isBossFightStarted && enemyCombat != null && enemyCombat.currentHealth <= 0)
        {
            EndBossFight();
        }
    }

    private bool IsPlayerInRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyAI.checkRadius, LayerMask.GetMask("Player"));
        return colliders.Length > 0;
    }

    private void StartBossFight()
    {
        // Get the boss fight audio clip from the AudioManager
        AudioClipInfo bossFightClip = audioManager.bossFight;

        // Stop the main background music
        audioManager.StopLoopingAudio(audioManager.bgMusicAudioSource);

        // Start the boss fight audio clip
        audioManager.PlayLoopingAudio(bossFightClip, audioManager.bgMusicAudioSource);

        // Set the flag to indicate that the boss fight has started
        isBossFightStarted = true;
    }

    public void EndBossFight()
    {
        // Stop the boss fight audio clip
        audioManager.StopLoopingAudio(audioManager.bgMusicAudioSource);

        // Resume the main background music
        audioManager.PlayLoopingAudio(audioManager.bgMusic, audioManager.bgMusicAudioSource);

        // Reset the flag
        isBossFightStarted = false;
    }
}
