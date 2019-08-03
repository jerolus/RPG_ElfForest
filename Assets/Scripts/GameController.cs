using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	private static GameController m_instance;
	public GameObject pauseMenu;
	public Animator fadeAnimator;
	public Text fadeText;

	private bool isPlaying = false;

	private void Awake()
	{
		SetInstance();
		DontDestroyOnLoad(gameObject);
	}

    private void Start()
    {
        
    }

    private void Update()
    {
		if(Input.GetKeyDown(KeyCode.P) && isPlaying)
		{
			CheckPause();
		}
	}

	public static GameController GetInstance()
	{
		if (m_instance != null)
		{
			return m_instance;
		}
		else
		{
			return null;
		}
	}

	private void SetInstance()
	{
		if (!m_instance)
		{
			m_instance = this;
		}
	}

	public void StartGame()
	{
		StartCoroutine(StartGameCoroutine());
	}

	public void MainMenu()
	{
		isPlaying = false;
		pauseMenu.SetActive(false);
		SceneManager.LoadScene("Menu");
	}

	public void ResumeGame()
	{
		CheckPause();
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	private void CheckPause()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		if (pauseMenu.activeSelf)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	private IEnumerator StartGameCoroutine()
	{
		isPlaying = true;
		DoFade();
		yield return new WaitForSeconds(0.6f);
		ChangeTextFade("Own Village");
		SceneManager.LoadScene("Tutorial");
		yield return new WaitForSeconds(1f);
	}

	public void DoFade()
	{
		fadeAnimator.SetTrigger("doFade");
	}

	public void ChangeTextFade(string textToShow)
	{
		fadeText.text = textToShow;
	}
}
