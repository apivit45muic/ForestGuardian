using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{   
    private UIManager uiManager;

    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isBoss;
    [SerializeField] private bool isEnemy;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioSource enemyAudioSource;

    [SerializeField] private AudioClip playerDeadSound;
    [SerializeField] private AudioClip bossDeadSound;
    [SerializeField] private AudioClip enemyDeadSound;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (isPlayer && SceneManager.GetActiveScene().buildIndex==1){
            currentHealth = startingHealth;
        }
        else if (isPlayer && SceneManager.GetActiveScene().buildIndex!=1){
        currentHealth = PlayerPrefs.GetFloat("PlayerHealth", startingHealth);
        } else {
        currentHealth = startingHealth;
        }
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;
                
                dead = true;
                if (isPlayer){
                    playerAudioSource.PlayOneShot(playerDeadSound, 0.3F);
                    uiManager.GameOver();
                } else if (isBoss){
                    bossAudioSource.PlayOneShot(bossDeadSound, 0.3F);
                    uiManager.Victory();
                } else if (isEnemy){
                    enemyAudioSource.PlayOneShot(enemyDeadSound, 0.3F);
                }
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}