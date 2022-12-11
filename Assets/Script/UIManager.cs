using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Var
    [Header("Wave")]
    public TextMeshProUGUI waveNum;
    public int currentwave;

    [Header("Point")]
    public TextMeshProUGUI txtNum;
    public int currentscore;

    [Header("Health")]
    public TextMeshProUGUI txtHlt;
    public int currenthealth;
    public Player pl;

    [Header("Best Score")]
    public TextMeshProUGUI txtBest;
    public int BestScore;

    //UI Var
    public GameObject gameOverUI;
    public GameObject PausePanel;
    public GameObject PointUI;
    public bool paused = false;

    private void Awake()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        paused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        BestScore = PlayerPrefs.GetInt("HighScore", 0);
        txtBest.text = BestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currenthealth = pl.Health;

        waveNum.text = currentwave.ToString();
        txtNum.text = currentscore.ToString();
        txtHlt.text = currenthealth.ToString();
        

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausing();
        }

        if (pl.Health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverUI.gameObject.SetActive(true);
        paused = true;
    }

    public void waveupdate(int currwave)
    {
        currentwave = currwave;
    }

    public void txtupdate(int score)
    {
        currentscore += score;
        if(BestScore < currentscore)
        {
            PlayerPrefs.SetInt("HighScore", currentscore);
        }
    }

    public void scene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void pausing()
    {
        PausePanel.gameObject.SetActive(true);
        PointUI.gameObject.SetActive(false);
        paused = true;
    }

    public void Continue()
    {
        PausePanel.gameObject.SetActive(false);
        PointUI.gameObject.SetActive(true);
        paused = false;
    }

}
