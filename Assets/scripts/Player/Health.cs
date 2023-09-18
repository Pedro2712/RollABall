using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class Health : MonoBehaviour

{
    public int numOfHearts = 4;
    public int life = 4;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeary;

    private float immortalDuration = 2.0f; // Dura��o da imortalidade em segundos.
    private float blinkInterval = 0.2f; // Intervalo de piscar em segundos.
    private float blinkTimer = 0.0f; // Cron�metro para controlar o piscar.
    private bool isImmortal = false;

    private Rigidbody rb;
    private Renderer playerRenderer;
    public CameraShake cameraShake;

    [SerializeField] private AudioClip _deathMusic;
    [SerializeField] private AudioSource _source;

    ParticleSystem blood;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        blood = GetComponent<ParticleSystem>();
        blood.Stop();

        isImmortal = true;
        StartCoroutine(DisableImmortality());
    }

    public void TakeDamage()
    {
        if (!isImmortal)
        {
            if (cameraShake != null)
            {
                cameraShake.ShakeCamera();
            }


            _source.PlayOneShot(_deathMusic);
            life--;


            isImmortal = true;
            blinkTimer = 0.0f;

            StartCoroutine(DisableImmortality());
        }
    }

    private IEnumerator DisableImmortality()
    {
        while (blinkTimer < immortalDuration)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            blinkTimer += blinkInterval;
        }

        playerRenderer.enabled = true;
        isImmortal = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isImmortal) {
                blood.Play();
            }
            TakeDamage();
        }
    }

    public void RecoverLife() {
        if (life < 4) {
            life++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < life)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeary;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (transform.position.y <= -10)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0, 2f, 0);
            TakeDamage();
        }
    }
}
