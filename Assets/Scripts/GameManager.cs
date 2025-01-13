using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController player;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformList;

    private ScoreManager theScoreManager;

    private bool isGameStarted = false;

    public GameObject panelsUI;
    public GameObject startPanelUI;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();

        panelsUI.SetActive(false);
    }

    private void Update()
    {
        if (isGameStarted == true)
        {
            startPanelUI.SetActive(false);
            panelsUI.SetActive(true);
            player.StartRunning();
            theScoreManager.scoreIncreasing = true;
        }
        else
        {
            startPanelUI.SetActive(true);
            panelsUI.SetActive(false);
            theScoreManager.scoreIncreasing = false;
        }
    }

    public void StartGame()
    {
        isGameStarted = true;        
    }

    public void RestartGame()
    {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestroyer>();

        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        player.gameObject.SetActive(true);

        player.coin = 0;
        player.coinText.text = player.coin.ToString();

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }
}
