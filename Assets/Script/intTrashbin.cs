using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intTrashbin : MonoBehaviour
{
    public GameObject intTrash; // UI yang akan muncul
    public GameObject objectToSpawn;         // Objek yang akan di-spawn
    public float spawnHeight = 10f;          // Tinggi spawn objek dari atas
    public string targetTag = "Trashbin"; // Tag objek yang akan di-interaksi

    private void Update()
    {
        // Cek apakah ada klik mouse kiri
        if (Input.GetMouseButtonDown(0)) // 0 = tombol kiri mouse
        {
            // Buat ray dari posisi kamera ke arah kursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Jika ray mengenai objek
            if (Physics.Raycast(ray, out hit))
            {
                // Cek apakah objek yang di-klik memiliki tag yang sesuai
                if (hit.collider.CompareTag("Trashbin"))
                {
                    // Panggil fungsi untuk spawn objek dari atas
                    SpawnObject(hit.point);
                }
            }
        }
    }

    private void SpawnObject(Vector3 position)
    {
        // Tentukan posisi spawn objek di atas
        Vector3 spawnPosition = new Vector3(position.x, position.y + spawnHeight, position.z);

        // Spawn objek pada posisi yang telah ditentukan
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        Debug.Log("Objek telah di-spawn dari atas.");
    }
}

