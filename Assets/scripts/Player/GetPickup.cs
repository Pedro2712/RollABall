using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPickup : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject enemyPrefab;
    public GameObject pickupPrefab;
    public GameObject hourGlassPrefab;
    public GameObject potionLife;

    [SerializeField] SandClock clock;
    [SerializeField] Health health;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _pickupMusic;
    [SerializeField] private AudioClip _lifeUp;
    [SerializeField] private AudioClip _timeUp;

    private GameObject newPickup;

    private float roomWidth = 30.0f;
    private float roomHeight = 30.0f;
    public float wallProximityThreshold = 1.0f;

    public static int count = 0;
    private float x;
    private float y;

    public float minSpawnDelay = 30f; // Tempo mínimo de espera entre spawns
    public float maxSpawnDelay = 60f; // Tempo máximo de espera entre spawns


    private float nextHourGlassSpawnTime;
    private float nextPotionLifeSpawnTime;

    private int hourGlassCount = 0;
    private int potionLifeCount = 0;
    private int maxSpawnCount = 3;


    private void Start()
    {
        x = (roomWidth / 2) - wallProximityThreshold;
        y = (roomHeight / 2) - wallProximityThreshold;

        count = 0;

        SetCountText();


        SpawnObjects(pickupPrefab, 1);
    }

    public GameObject PositionPickup()
    {
        return newPickup;
    }

    private void SpawnObjects(GameObject prefab, int countToSpawn)
    {
        for (int i = 0; i < countToSpawn; i++)
        {
            Vector3 position = GenerateRandomPosition();
            GameObject spawnedObject = Instantiate(prefab, position, Quaternion.identity);

            // Verifica se o objeto criado é um pickupPrefab
            if (prefab == pickupPrefab)
            {
                newPickup = spawnedObject;
            }
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-x, x);
        float randomY = 0.5f;
        float randomZ = Random.Range(-y, y);
        return new Vector3(randomX, randomY, randomZ);
    }

    private void SetCountText()
    {
        countText.text = count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            SpawnObjects(pickupPrefab, 1);
            Destroy(other.gameObject);
            count++;
            _source.PlayOneShot(_pickupMusic);

            SetCountText();

            if (count <= 5)
            {
                SpawnObjects(enemyPrefab, 1);
            }
        }

        if (other.gameObject.CompareTag("hourGlass"))
        {
            hourGlassCount--;
            if (hourGlassCount < 0)
                hourGlassCount= 0;
            
            Destroy(other.gameObject);
            _source.PlayOneShot(_timeUp);


            clock.IncreaseTime(5);
        }

        if (other.gameObject.CompareTag("PotionLife"))
        {
            potionLifeCount--;
            if (potionLifeCount < 0)
                potionLifeCount= 0;
            Destroy(other.gameObject);
            _source.PlayOneShot(_lifeUp);

            health.RecoverLife();
        }
    }

    private void ScheduleNextHourGlassSpawn()
    {
        nextHourGlassSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void ScheduleNextPotionLifeSpawn()
    {
        nextPotionLifeSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void Update()
    {
        // Verificar se é hora de spawnar hourGlassPrefab
        if (Time.time >= nextHourGlassSpawnTime)
        {
            int random = Mathf.Clamp(Random.Range(1, 3), 0, maxSpawnCount - hourGlassCount);
            SpawnObjects(hourGlassPrefab, random);
            ScheduleNextHourGlassSpawn();
            hourGlassCount += random;
        }

        // Verificar se é hora de spawnar potionLife
        if (Time.time >= nextPotionLifeSpawnTime)
        {
            int random = Mathf.Clamp(Random.Range(1, 3), 0, maxSpawnCount - potionLifeCount);
            SpawnObjects(potionLife, random);
            ScheduleNextPotionLifeSpawn();
            potionLifeCount += random;
        }


    }
}
