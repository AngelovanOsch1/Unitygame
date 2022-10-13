using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ScoreText;
    private int scoreCounter;
    // Start is called before the first frame update

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        scoreCounter = PlayerPrefs.GetInt("score");
        ScoreText.text = "Score: " + scoreCounter;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
