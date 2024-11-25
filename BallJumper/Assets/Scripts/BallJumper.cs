using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJumper : MonoBehaviour
{
    public float maxJumpForce; // Максимальная сила прыжка
    public float minJumpForce;  // Минимальная сила прыжка
    public float forceChangeSpeed; // Скорость изменения силы
    public float groundCheckDistance; // Расстояние для проверки земли
    public LayerMask groundLayer; // Слой земли

    public Color originalColor;
    public Color changedColor;

    public int jumpDistanceColor;

    public float currentJumpForce; // текущая сила прыжка

    private Rigidbody rb;
    private bool increasingForce = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentJumpForce = maxJumpForce;
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // Изменяем силу прыжка
        if (increasingForce)
        {
            currentJumpForce += forceChangeSpeed * Time.deltaTime;
            if (currentJumpForce >= maxJumpForce)
            {
                currentJumpForce = maxJumpForce;
                increasingForce = false;
            }
        }
        else
        {
            currentJumpForce -= forceChangeSpeed * Time.deltaTime;
            if (currentJumpForce <= minJumpForce)
            {
                currentJumpForce = minJumpForce;
                increasingForce = true;
            }
        }

        // Проверка, на земле ли мяч
        if (IsGrounded())
        {
            Jump();
        }

        // Изменяем цвет, если необходимо
        if (currentJumpForce < jumpDistanceColor)
        {
            GetComponent<Renderer>().material.color = changedColor;

            if (!lowSpeed)
            {
                lowSpeed = true;
                lowSpeedTime = Time.time;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = originalColor;

            if (lowSpeed)
            {
                lowSpeed = false;
                Debug.Log("Low speed time: " + (Time.time - lowSpeedTime));
            }
        }
    }

    bool lowSpeed = false;
    float lowSpeedTime;
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, currentJumpForce, rb.velocity.z);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}

