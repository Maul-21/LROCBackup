using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CrosshairInteraction : MonoBehaviour
{
    public GameObject interactionUI; // UI yang akan muncul saat objek dapat diinteraksi
    public GameObject intTrash; // UI yang akan muncul untuk interaksi dengan Trashbin
    public GameObject intTrasbin;
    public GameObject objectToSpawn; // Objek yang akan di-spawn
    public float spawnHeight = 10f; // Tinggi spawn objek dari atas
    public float interactionRange = 3f; // Jarak interaksi
    private Transform player; // Referensi ke pemain
    private int score = 0;
    public TextMeshProUGUI scoreUI;
    private bool canClick = true;

    private void Start()
    {
        player = Camera.main.transform; // Asumsi kamera utama adalah pemain
        interactionUI.SetActive(false);
        intTrash.SetActive(false); // Sembunyikan UI di awal
        intTrasbin.SetActive(false);
        score = 0;
    }

    private void Update()
{
    // Lakukan Raycast untuk mendeteksi objek di depan
    Ray ray = new Ray(player.position, player.forward);
    RaycastHit hit;

    // Cek apakah ray mengenai objek dalam jarak interaksi
    if (Physics.Raycast(ray, out hit, interactionRange))
    {
        // Debug log untuk memastikan raycast mengenai objek
        Debug.Log("Raycast mengenai objek: " + hit.collider.gameObject.name);

        // Cek jika objek memiliki tag tertentu untuk Trash
        if (hit.collider.CompareTag("Trash"))
        {
            intTrash.SetActive(true);
            interactionUI.SetActive(true); // Tampilkan UI interaksi untuk Trash

            // Aksi interaksi: Hancurkan objek ketika diklik, hanya jika skor < 5
            if (canClick && Input.GetMouseButtonDown(0))
            {
                Destroy(hit.collider.gameObject); // Hancurkan objek
                Debug.Log("Trash dihancurkan.");
                score += 1;
                scoreUI.text = score.ToString();

                // Periksa apakah skor sudah mencapai 5
                if (score >= 5)
                {
                    canClick = false; // Nonaktifkan klik pada objek bertag Trash
                    Debug.Log("Skor mencapai 5, klik pada objek Trash dinonaktifkan.");
                }
            }
        }
        // Cek jika objek memiliki tag tertentu untuk Trashbin
        else if (hit.collider.CompareTag("Trashbin"))
        {
             // Tampilkan UI interaksi untuk Trashbin
            intTrasbin.SetActive(true);
            interactionUI.SetActive(true);
            Debug.Log("UI Trashbin muncul.");

            // Cek apakah skor sudah mencukupi untuk menjalankan aksi
            if (Input.GetMouseButtonDown(0))
            {
                if (score >= 5) // Jika skor >= 5, jalankan kondisi
                {
                    ExecuteCondition(); // Jalankan kondisi khusus yang diinginkan
                    ResetScore(); // Reset skor setelah kondisi dijalankan
                    canClick = true; // Nonaktifkan interaksi lebih lanjut
                    SpawnObject(hit.point);
                }
                else
                {
                    Debug.Log("Skor belum mencukupi untuk menjalankan kondisi.");
                }
            }
        }

        else if (hit.collider.CompareTag("boat"))
            {
                Debug.Log("Mengarah ke objek bertag 'Boat'. Klik untuk pindah scene.");

                // Periksa jika tombol mouse kiri diklik
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Objek 'Boat' diklik. Memindahkan ke scene baru.");
                    ChangeScene("UnderWater"); // Ganti dengan nama scene yang ingin dibuka
                }
            }
    }
    else
    {
        // Sembunyikan UI jika tidak ada objek yang dapat diinteraksi
        interactionUI.SetActive(false);
        intTrash.SetActive(false);
        intTrasbin.SetActive(false);
    }
}

private void ExecuteCondition()
{
    Debug.Log("Kondisi dijalankan karena skor mencukupi.");
    // Tambahkan logika khusus Anda di sini (misalnya membuka pintu, spawn item, dll)
}

private void ResetScore()
{
    score = 0;
    scoreUI.text = score.ToString();
    Debug.Log("Skor telah di-reset ke 0.");
}

private void SpawnObject(Vector3 position)
    {
        // Tentukan posisi spawn objek di atas
        Vector3 spawnPosition = new Vector3(position.x, position.y + spawnHeight, position.z);

        // Spawn objek pada posisi yang telah ditentukan
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        Debug.Log("Objek telah di-spawn dari atas.");
    }
private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}