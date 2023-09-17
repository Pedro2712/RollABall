using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    public float forceMagnitude = 50.0f; // Magnitude da força inicial.
    private float roomWidth = 30.0f;
    private float roomHeight = 30.0f;
    public float changePositionInterval = 5.0f;
    public float wallProximityThreshold = 1.0f; // Distância das paredes que aciona o movimento para o centro.

    private float timeUntilChangePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeUntilChangePosition = changePositionInterval;
        ApplyRandomForce();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -roomWidth / 2, roomWidth / 2);
        position.z = Mathf.Clamp(position.z, -roomHeight / 2, roomHeight / 2);
        transform.position = position;
        timeUntilChangePosition -= Time.deltaTime;

        if (timeUntilChangePosition <= 0.0f)
        {
            ApplyRandomForce();
            timeUntilChangePosition = changePositionInterval;
        }

        // Verifica a proximidade das paredes.
        if (Mathf.Abs(position.x) > roomWidth / 2 - wallProximityThreshold || Mathf.Abs(position.z) > roomHeight / 2 - wallProximityThreshold)
        {
            // Se estiver próximo das paredes, aplique uma força direcionada para o centro.
            Vector3 centerForce = new Vector3(-position.x, 0f, -position.z).normalized * 0.5f;
            rb.AddForce(centerForce, ForceMode.Impulse);
        }
    }

    private void ApplyRandomForce()
    {
        Vector3 randomForce = new Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f)).normalized * forceMagnitude;
        rb.AddForce(randomForce, ForceMode.Impulse);
    }
}
