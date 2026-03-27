using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Target's position - own's position = target's direction
        Vector3 dir = player.transform.position - transform.position;

        // Normalize "dir" to maintain a stable speed
        dir.Normalize();

        rb.AddForce(dir * speed);
    }
}
