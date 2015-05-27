using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	public string MainGameSceneName = "Game";
//	public string MainMenuSceneName = "MainMenu";
//	public string GameOverSceneName = "GameOver";

	public void LoadMainGame()
	{
		Application.LoadLevel(MainGameSceneName);
	}

//	public void LoadMainMenu()
//	{
//		Application.LoadLevel (MainMenuSceneName);
//	}
//
//	public void LoadGameOver()
//	{
//		Application.LoadLevel (GameOverSceneName);
//	}

}
