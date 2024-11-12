using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    public float waterLevel = 0.0f;       // Ketinggian air (y-axis)
    public float floatStrength = 10.0f;   // Kekuatan gaya apung
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Mengecek posisi objek terhadap permukaan air
        if (transform.position.y < waterLevel)
        {
            // Menambahkan gaya ke atas untuk menciptakan efek mengapung
            float force = floatStrength * (waterLevel - transform.position.y);
            rb.AddForce(Vector3.up * force, ForceMode.Force);
        }
    }
}
