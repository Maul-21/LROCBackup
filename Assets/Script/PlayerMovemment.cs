using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemment : MonoBehaviour
{
    public float moveSpeed = 5f;           // Kecepatan gerakan
    public float jumpForce = 5f;           // Kekuatan lompat
    public float mouseSensitivity = 2f;    // Sensitivitas mouse
    public Transform playerCamera;         // Referensi ke kamera pemain
    public LayerMask groundLayer;          // Layer tanah (untuk mendeteksi jika pemain di tanah)

    private Rigidbody rb;                  // Referensi Rigidbody
    private bool isGrounded;               // Menyimpan status apakah pemain berada di tanah
    private float xRotation = 0f;          // Rotasi sumbu X untuk rotasi vertikal kamera

    private void Start()
    {
        rb = GetComponent<Rigidbody>();    // Ambil referensi Rigidbody
        Cursor.lockState = CursorLockMode.Locked;   // Menyembunyikan kursor dan menguncinya di tengah layar
        Cursor.visible = false;                    // Menyembunyikan kursor
    }

    private void Update()
    {
        MovePlayer();
        LookAround();
        Jump();
    }

    private void MovePlayer()
    {
        // Ambil input pergerakan horizontal dan vertikal
        float moveX = Input.GetAxis("Horizontal"); // A/D atau Arrow Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S atau Arrow Up/Down

        // Buat vektor pergerakan berdasarkan input
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        
        // Terapkan pergerakan ke Rigidbody
        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);
    }

    private void LookAround()
    {
        // Rotasi kamera dengan input mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotasi horizontal untuk pemain
        transform.Rotate(Vector3.up * mouseX);

        // Rotasi vertikal untuk kamera, dengan pembatasan agar tidak terbalik
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Jump()
    {
        // Cek apakah pemain menekan tombol lompat dan apakah di tanah
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Tambahkan gaya lompatan ke Rigidbody
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay()
    {
        // Cek apakah pemain sedang berada di tanah (melalui collider)
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        // Jika keluar dari tanah (misalnya jatuh)
        isGrounded = false;
    }
}