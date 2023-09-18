using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{

    private Transform target;
    [SerializeField] private GetPickup pickup;

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Transform targetTransform = player.GetComponent<Transform>();
        transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 2f, targetTransform.position.z);

        transform.LookAt(pickup.PositionPickup().transform.position);
    }
}
