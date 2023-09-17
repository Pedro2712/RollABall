using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class GetPickup : MonoBehaviour
{
    public TextMeshProUGUI countText;

    public GameObject winTextObject;
    public GameObject enemyPrefab;
    public GameObject pickupPrefab;

    [SerializeField] private AudioClip _pickupMusic;
    [SerializeField] private AudioSource _source;


    private float roomWidth = 30.0f;
    private float roomHeight = 30.0f;
    public float wallProximityThreshold = 1.0f; // Distância das paredes que aciona o movimento para o centro.


    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetCountText();

        winTextObject.SetActive(false);

        Instantiate(pickupPrefab, new Vector3(UnityEngine.Random.Range(-10f, 10f), 0.5f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);

        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            float x = (roomWidth / 2) - wallProximityThreshold;
            float y = (roomHeight / 2) - wallProximityThreshold;
            Instantiate(pickupPrefab, new Vector3(UnityEngine.Random.Range(-x, x), 0.5f, UnityEngine.Random.Range(-y, y)), Quaternion.identity);
            other.gameObject.SetActive(false);
            count++;
            _source.PlayOneShot(_pickupMusic);

            SetCountText();

            if (count <= 5)
            {
                Instantiate(enemyPrefab, new Vector3(UnityEngine.Random.Range(-x, x), 0.5f, UnityEngine.Random.Range(-y, y)), Quaternion.identity);
            }
        }
    }
}
