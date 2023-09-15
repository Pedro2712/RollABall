using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControler : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI lifeText;
    public GameObject winTextObject;
    public GameObject deadTextObject;

    private Rigidbody rb;
    private int count = 0;
    private int life = 3;

    private float movementX;
    private float movementY;

    private bool isImmortal = false;
    private float immortalDuration = 5.0f; // Duração da imortalidade em segundos.
    private float blinkInterval = 0.2f; // Intervalo de piscar em segundos.
    private float blinkTimer = 0.0f; // Cronômetro para controlar o piscar.
    private Renderer playerRenderer;

    public GameObject enemyPrefab;
    public GameObject pickupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetCountText();
        SetLifeText();
        winTextObject.SetActive(false);
        playerRenderer = GetComponent<Renderer>();

        Instantiate(pickupPrefab, new Vector3(UnityEngine.Random.Range(-10f, 10f), 0.5f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
    }

    void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }

    void SetLifeText()
    {
        lifeText.text = "life: " + life.ToString();
        if (life == 0)
        {
            deadTextObject.SetActive(true);
        }
    }

    public void TakeDamage()
    {
        if (!isImmortal)
        {
            life--;
            SetLifeText();

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

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Instantiate(pickupPrefab, new Vector3(UnityEngine.Random.Range(-10f, 10f), 0.5f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
            other.gameObject.SetActive(false);
            count++;

            SetCountText();

            if (count <= 3)
            {
                Instantiate(enemyPrefab, new Vector3(UnityEngine.Random.Range(-10f, 10f), 0.5f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
            }
        }

        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
}
