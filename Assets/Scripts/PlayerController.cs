using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform focalPoint;
    public bool hasPushPowerUp;
    public GameObject powerUpIndicator;
    private GameObject runTimePowerUp;

    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;

    private Coroutine powerUpRoutine;
    private Coroutine stunRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
    }

    // Update is called once per frame
    void Update()
    {
        // Create a variable to read value of "moveAction" in Vector2 and store in "move"
        var move = moveAction.ReadValue<Vector2>();

        rb.AddForce(move.y * speed * focalPoint.forward);

        // If "breakAction" is pressed
        if (breakAction.IsPressed())
        {
            // Make "rb"'s linear velocity zero
            rb.linearVelocity = Vector3.zero;
        }
    }

    void LateUpdate()
    {
        if (runTimePowerUp != null)
        {
            runTimePowerUp.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPushPowerUp = true;
            Destroy(other.gameObject);

            if (runTimePowerUp != null)
            {
                Destroy(runTimePowerUp);
            }

            runTimePowerUp = Instantiate(powerUpIndicator);
            
            if (powerUpRoutine != null)
            {
                StopCoroutine(powerUpRoutine);
            }
            powerUpRoutine = StartCoroutine(PowerUpCooldown());
        }
    }

    IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(10f);

        if (runTimePowerUp != null)
        {
            Destroy(runTimePowerUp);
        }

        hasPushPowerUp = false;
        powerUpRoutine = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPushPowerUp)
            {
                var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                
                // var v = enemyRb.linearVelocity;
                // v.Normalize();

                // Target's position - own's position = this to target direction
                var dir = enemyRb.transform.position - transform.position;
                dir.Normalize();

                // If target adds force with this to target direction, it will reverse target's direction / bounce back
                enemyRb.AddForce(dir * 10, ForceMode.Impulse);
            }
        }
    }

    public void isStunActive(float duration)
    {
        Enemy.IsGlobalStun = true;

        if (stunRoutine != null)
        {
            StopCoroutine(stunRoutine);
        }
        stunRoutine = StartCoroutine(stunCoolDown(duration));
    }

    public IEnumerator stunCoolDown(float duration)
    {
        yield return new WaitForSeconds(duration);

        Enemy.IsGlobalStun = false;
        stunRoutine = null;
        Debug.Log("Enemies are not stunned anymore!");
    }
}