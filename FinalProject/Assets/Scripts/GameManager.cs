using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController pc;
    public SpawnManager spm;
    public bool isGameActive = false;
    public GameObject titleScreen;
    public GameObject gameover;
    public GameObject hud;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI endScore;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Cursor.visible = false;
        spm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        highScore.text = "Longest Surival: " + PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    public void GameOver()
    {
        gameover.SetActive(true);
        endScore.text = "You Surival: "+ (int)Mathf.Round(pc.timeSurivived);
        isGameActive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        pc.StartGame(difficulty);
        titleScreen.SetActive(false);
        hud.SetActive(true);
        spm.gameObject.SetActive(true);
        spm.difficulty = difficulty;
        AudioManager.instance.Play("BGM");
        isGameActive = true;
    }
}
