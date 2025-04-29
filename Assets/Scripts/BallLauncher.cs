using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public static BallLauncher Instance { get; private set; }

    public GameObject ballPrefab;
    public Transform launchPoint;
    public float maxPower = 10f;
    public float chargeSpeed = 2f;

    public float currentPower;
    public bool isCharging;
    public bool canLaunch = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && canLaunch)
        {
            isCharging = true;
            currentPower = 0;
        }

        if (isCharging)
        {
            currentPower += chargeSpeed * Time.deltaTime;
            if (currentPower > maxPower) currentPower = maxPower;
        }

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return)) && isCharging)
        {
            LaunchBall();
            isCharging = false;
        }
    }

    void LaunchBall()
    {
        GameObject ball = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(launchPoint.up * currentPower, ForceMode.Impulse);
        canLaunch = false;
    }

    public void ResetLauncher()
    {
        canLaunch = true;
    }
}