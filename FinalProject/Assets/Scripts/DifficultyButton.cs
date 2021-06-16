using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    public GameObject startMenu;
    public GameManager gm;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        gm.StartGame(difficulty);
    }

    void Update()
    {

    }
}
