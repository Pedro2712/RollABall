using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour

{
    public TextMeshProUGUI countText;

    public GameObject winTextObject;
    public GameObject enemyPrefab;
    public GameObject pickupPrefab;


    private Health healf;


    private Rigidbody rb;
    private int count = 0;
    public float speed = 0;

    private float movementX;
    private float movementY;

    [SerializeField] private AudioClip _pickupMusic;
    [SerializeField] private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healf = GetComponent<Health>();

        SetCountText();
        winTextObject.SetActive(false);

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
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Instantiate(pickupPrefab, new Vector3(UnityEngine.Random.Range(-10f, 10f), 0.5f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
            other.gameObject.SetActive(false);
            count++;
            _source.PlayOneShot(_pickupMusic);

            SetCountText();

            if (count <= 3)
            {
                Instantiate(enemyPrefab, new Vector3(UnityEngine.Random.Range(-10f, 10f), 0.5f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
            }
        }

        if (other.CompareTag("Enemy"))
        {
            if (healf != null) {
                healf.TakeDamage();
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        if (transform.position.y <= -5) {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0, 2f, 0);
            if (healf != null)
            {
                healf.TakeDamage();
            }
        }

    }
}
