using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoresampah : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Drag your UI Text here in the Inspector
    private int score = 0;

    void Start()
    {
       if (scoreText == null)
    {
        Debug.LogError("ScoreText is not assigned in the Inspector!");
    }
    UpdateScoreText();
        
    }

    void Update()
    {
        // Check for mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            DetectObjectClick();
        }
    }
}