using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> AllEnemies = new List<Enemy>();
    public static bool IsGlobalStun = false;

    public float speed = 3f;

    private Rigidbody rb;
    private GameObject player;

    private Coroutine stunRoutine;

    void OnEnable() { AllEnemies.Add(this); }

    void OnDisable() { AllEnemies.Remove(this); }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGlobalStun)
        {
            // Target's position - own's position = target's direction
            Vector3 dir = player.transform.position - transform.position;

            // Normalize "dir" to maintain a stable speed
            dir.Normalize();

            rb.AddForce(dir * speed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestroyArea"))
        {
            Destroy(gameObject);
        }
    }
}