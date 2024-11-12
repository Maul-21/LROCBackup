using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //untuk mengakses komponen CharacterController
    public CharacterController controller;

    // Kecepatan pergerakan karakter
    public float movementSpeed = 5.0f;

    // Tinggi lompatan
    public float jumpHeight = 3.0f;

    // Gravitasi yang mempengaruhi karakter
    public float gravity = -9.81f;

    // Transform untuk mengecek apakah karakter berada di tanah
    public Transform groundCheck;

    // Jarak dari groundCheck ke tanah untuk menentukan apakah karakter dianggap di tanah atau tidak
    public float groundDistance = 0.4f;

    // LayerMask untuk mendeteksi tanah
    public LayerMask groundMask;

    // Vektor untuk menyimpan kecepatan vertikal karakter
    public Vector3 velocity;

    // Status apakah karakter berada di tanah atau tidak
    bool isGrounded;

    void Update()
    {
        // Memeriksa apakah karakter berada di tanah
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Mendapatkan input dari sumbu horizontal (A dan D atau Left Arrow dan Right Arrow)
        float horizontal = Input.GetAxis("Horizontal");

        // Mendapatkan input dari sumbu vertikal (W dan S atau Up Arrow dan Down Arrow)
        float vertical = Input.GetAxis("Vertical");

        // Menggabungkan vektor gerak horizontal dan vertikal
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Menggerakkan karakter
        controller.Move(move * movementSpeed * Time.deltaTime);

        // Jika karakter berada di tanah, kecepatan vertikal diatur ke nilai rendah
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Jika tombol lompat ditekan dan karakter berada di tanah
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Menghitung dan menerapkan kecepatan vertikal untuk melompat
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Menerapkan gravitasi
        velocity.y += gravity * Time.deltaTime;

        // Menggerakkan karakter berdasarkan kecepatan vertikal
        controller.Move(velocity * Time.deltaTime);
    }
}   
