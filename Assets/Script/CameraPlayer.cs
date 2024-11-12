using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
   public float mouseSensitivity = 100.0f; // Menentukan sensitivitas mouse
    public Transform playerBody; // Variabel yang berisi Transform dari objek yang akan mengontrol rotasi horizontal
    private float verticalRotation = 0; // Menyimpan rotasi vertikal kamera

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Mengunci kursor mouse ke tengah layar
    }

    void Update()
    {
        // Mendapatkan input dari mouse dan mengalikannya dengan sensitivitas dan deltaTime untuk gerakan yang konsisten
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Menghitung rotasi vertikal dan memastikan kamera tidak berputar lebih dari 90 derajat ke atas atau ke bawah
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Mengatur rotasi vertikal kamera
        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Mengatur rotasi horizontal objek yang mengontrol rotasi horizontal (biasanya karakter pemain)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

