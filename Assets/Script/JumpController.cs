using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
   public float jumpForce = 5f; // Kekuatan lompatan
    public KeyCode jumpKey = KeyCode.Space; // Tombol untuk melompat
    public LayerMask groundLayer; // Layer yang dianggap sebagai tanah
    public Transform groundCheck; // Objek untuk memeriksa apakah pemain di tanah
    public float groundCheckRadius = 0.2f; // Radius pengecekan tanah

    private Rigidbody rb; // Referensi ke Rigidbody
    private bool isGrounded; // Cek apakah karakter berada di tanah

    private void Start()
    {
        // Ambil komponen Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Cek apakah karakter di tanah
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Jika tombol lompatan ditekan dan karakter berada di tanah
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            // Tambahkan gaya lompatan ke Rigidbody
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
