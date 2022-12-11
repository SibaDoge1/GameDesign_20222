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

    private bool isDead;
    private bool damaged;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        currentHealth = startingHealth;
        
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
            TakeDamage(2);
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

        playerAudio.clip = deathClip;
        playerAudio.Play();
        //RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
