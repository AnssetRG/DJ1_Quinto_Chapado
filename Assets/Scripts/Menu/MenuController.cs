using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainBallsPanel;
    private Animator mainBallsPanelAnimator;
    [SerializeField]
    private GameObject mainButtonsPanel;
    private Animator mainButtonsPanelAnimator;
    [SerializeField]
    private Button btnPlay;
    [SerializeField]
    private Button btnBalls;
    [SerializeField]
    private Button btnBack;

    // Start is called before the first frame update
    void Start()
    {
        mainBallsPanelAnimator = mainBallsPanel.GetComponent<Animator>();
        mainButtonsPanelAnimator = mainButtonsPanel.GetComponent<Animator>();
        btnPlay.onClick.AddListener(() => goPLay());
        btnBalls.onClick.AddListener(() => goBalls());
        mainBallsPanel.SetActive(false);
        btnBack.onClick.AddListener(() => goMenu());
    }

    void goPLay()
    {
        SceneManager.LoadScene("Game");
    }

    void goBalls()
    {
        mainBallsPanel.SetActive(true);
        mainBallsPanelAnimator.Play("fadeIn");
        mainButtonsPanelAnimator.Play("fadeOut");
    }

    void goMenu()
    {
        mainBallsPanelAnimator.Play("fadeOut");
        mainButtonsPanelAnimator.Play("fadeIn");
    }
}
