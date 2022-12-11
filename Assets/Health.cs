using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private AudioSource playerAudio;
    [SerializeField]private GameObject deathEffect;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball;
    [SerializeField] private AudioSource explosion;

    private bool isDead;
    private bool damaged;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
        animator = player.GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Ground")&&!isDead)
        {
            TakeDamage(1);
        }
    }



    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
        healthSlider.value = currentHealth;

        playerAudio.Play();

        if(currentHealth <=0 && !isDead)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;
//        explosion.Play();
        Instantiate(deathEffect, transform.position, transform.rotation);
        Instantiate(deathEffect, player.transform.position, player.transform.rotation);
        animator.Play("death");
        //Destroy(player);
        Destroy(ball);
        GameManager.instance.onFail();
        
        playerAudio.clip = deathClip;
        playerAudio.Play();
    }

}
