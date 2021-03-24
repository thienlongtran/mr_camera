﻿/*
===================================================================
Unity Assets by MAKAKA GAMES: https://makaka.org/o/all-unity-assets
===================================================================

Online Docs (Latest): https://makaka.org/unity-assets
Offline Docs: You have a PDF file in the package folder.

=======
SUPPORT
=======

First of all, read the docs. If it didn’t help, get the support.

Web: https://makaka.org/support
Email: info@makaka.org

If you find a bug or you can’t use the asset as you need, 
please first send email to info@makaka.org (in English or in Russian) 
before leaving a review to the asset store.

I am here to help you and to improve my products for the best.
*/

using System.Diagnostics;
using System.Collections.Generic;	

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[HelpURL("https://makaka.org/unity-assets")]
[AddComponentMenu ("Scripts/Makaka Games/Everyday Tools/Scene Control/Menu Scene Control")]
public class MenuSceneControl : MonoBehaviour 
{
	[SerializeField]
	private string nameOfSceneWithLoadScreen = "LoadScreen";

    public void LoadSceneWithScreenOrientationLandscapeLeft(string sceneName)
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		LoadScreenControl.Instance.LoadScene(
			sceneName, false, nameOfSceneWithLoadScreen);
	}

	public void LoadSceneWithScreenOrientationPortrait(string sceneName)
	{
		Screen.orientation = ScreenOrientation.Portrait;

		LoadScreenControl.Instance.LoadScene(
			sceneName, false, nameOfSceneWithLoadScreen);
	}

	public void ReloadCurrentScene()
	{
		LoadScreenControl.Instance.LoadScene(
			SceneManager.GetActiveScene().name,
			false,
			nameOfSceneWithLoadScreen);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void OpenLink(string link = "https://makaka.org/support")
	{
		Process.Start(link);
	}
}