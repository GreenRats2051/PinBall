using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Flipper : MonoBehaviour
{
    [Header("Settings")]
    public float restAngle = 0f;
    public float pressedAngle = 45f;
    public float flipSpeed = 500f;
    public float returnSpeed = 200f;
    public KeyCode inputKey;
    public float hitForceMultiplier = 5f; // Новый параметр

    private Rigidbody rb;
    private bool isFlipping = false;
    private float currentAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        currentAngle = restAngle;
        UpdateRotation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(inputKey)) isFlipping = true;
        if (Input.GetKeyUp(inputKey)) isFlipping = false;

        float targetAngle = isFlipping ? pressedAngle : restAngle;
        float speed = isFlipping ? flipSpeed : returnSpeed;

        currentAngle = Mathf.MoveTowards(currentAngle, targetAngle, speed * Time.deltaTime);
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        rb.MoveRotation(Quaternion.Euler(0, 0, currentAngle));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && isFlipping)
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 forceDirection = (collision.transform.position - transform.position).normalized;
            ballRb.AddForce(forceDirection * hitForceMultiplier, ForceMode.Impulse);
        }
    }
}