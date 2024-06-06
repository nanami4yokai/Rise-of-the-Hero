using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public Animator animator;
    private AudioManager audioManager;
    public int maxHealth = 100;
    public int currentHealth;
    public ParticleSystem deathParticles;
    [SerializeField] Enemy_HPBar hp_bar;

    public GameObject projectilePrefab;
    public float attackCooldown;
    public float projectileSpeed;

    [HideInInspector] public bool isInAttackRange = false;
    [HideInInspector] public Transform target;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        hp_bar.UpdateHealthBar(currentHealth, maxHealth);

        hp_bar = GetComponentInChildren<Enemy_HPBar>();

        StartCoroutine(Attack());

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hp_bar.UpdateHealthBar(currentHealth, maxHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died");

        // Instantiate and play the particle system at the enemy's position and rotation
        ParticleSystem particles = Instantiate(deathParticles, transform.position, transform.rotation);
        particles.Play();

        // Start the coroutine to destroy the particles after their duration
        StartCoroutine(DestroyParticlesAfterDuration(particles));
        audioManager.PlayAudio(audioManager.monsterDeath);
        Destroy(gameObject);

        LootBag lootBag = GetComponent<LootBag>();
        if (lootBag == null)
        {
            Debug.Log("No LootBag component found on the chest.");
            return;
        }

        lootBag.InstantiateLoot(transform.position);
    }

    private IEnumerator DestroyParticlesAfterDuration(ParticleSystem particles)
    {
        yield return new WaitForSeconds(particles.main.duration);
        Destroy(particles.gameObject);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            if (isInAttackRange && target != null)
            {
                // Spawn and shoot projectile
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Vector2 direction = (target.position - transform.position).normalized;
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            }
            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
