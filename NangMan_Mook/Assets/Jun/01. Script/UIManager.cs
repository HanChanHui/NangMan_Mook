using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Player 오브젝트 넣기")][SerializeField] private GameObject Player;

    [Header("GameOver 오브젝트 넣기")][SerializeField] private Image GameOverText;
    [Header("FadeIn 오브젝트 넣기")][SerializeField] private Image FadeInImg;

    [Header("main Group 오브젝트 넣기")][SerializeField] private CanvasGroup mainGroup;
    [Header("new Group 오브젝트 넣기")][SerializeField] private CanvasGroup newGroup;

    [Header("LineDrawer 오브젝트 넣기")][SerializeField] private LinesDrawer LD;

    public static bool isDraw = false;
    public static bool isPaused = false;
    private void Start()
    {
        Time.timeScale = 1;
        isDraw = false;
        isPaused = false;
    }

    private void Update()
    {
        DrawMode();
        GameOver();
    }

    private bool isGameOver = false;
    private void DrawMode()
    {
        if(!isDraw && !isPaused && !isGameOver && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.G)) && Player.GetComponent<PlayerController>().HealthPoint > 0)
        {
            Time.timeScale = 0;
            isDraw = true;
            LD.isPaused = true;
            Player.layer = 7;
            CanvasGroupOn(newGroup);
            CanvasGroupOff(mainGroup);
        }
        else if(isDraw && !LD.Drawing &&!isPaused && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.G)))
        {
            Time.timeScale = 1;
            isDraw = false;
            LD.isPaused = false;
            Player.layer = 8;
            CanvasGroupOn(mainGroup);
            CanvasGroupOff(newGroup);
        }
    }

    private void GameOver()
    {
        if(Player.GetComponent<PlayerController>().HealthPoint <= 0 && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOverScene());
        }
    }

    public void MapOut()
    {
        isGameOver = true;
        StartCoroutine(GameOverScene());
    }

    private float time = 0f;
    private float F_time = 1f;
    [Header("UI 오브젝트 넣기")][SerializeField] private SoundManager soundManager;
    private IEnumerator GameOverScene()
    {
        soundManager.PlaySound("GAMEOVER");
        GameOverText.gameObject.SetActive(true);
        Color alpha = GameOverText.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            GameOverText.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        time = 0f;
        F_time = 1f;
        FadeInImg.gameObject.SetActive(true);
        alpha = FadeInImg.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            FadeInImg.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("03. GameOverScene");
    }

    private void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    private void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}