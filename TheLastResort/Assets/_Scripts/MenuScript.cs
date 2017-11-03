using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas OfflineMenu;
    public Canvas OnlineMenu;
    public Button startHunterText;
    public Button startDeerText;
    public Button exitText;
    public Button startOnlineText;
    public Button startOfflineText;
    public Button GoBackText;

	// Use this for initialization
	void Start () {

        quitMenu = quitMenu.GetComponent<Canvas>();
        startOnlineText = startOnlineText.GetComponent<Button>();
        startOfflineText = startOfflineText.GetComponent<Button>();

        //startHunterText = startHunterText.GetComponent<Button>();
        //startDeerText = startDeerText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        //startHunterText.enabled = false;
        //startDeerText.enabled = false;
        OfflineMenu.enabled = false;
        OnlineMenu.enabled = false;
        quitMenu.enabled = false;

		
	}
	
   

    public void OnlinePress()
    {
        OnlineMenu.enabled = true;
        startOnlineText.enabled = false;
        startOfflineText.enabled = false;
    }

    public void OfflinePress()
    {
        OfflineMenu.enabled = true;
        startOnlineText.enabled = false;
        startOfflineText.enabled = false;
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startOnlineText.enabled = false;
        startOfflineText.enabled = false;
        exitText.enabled = false;


    }

    public void goBackPress()
    {
        quitMenu.enabled = false;
        OfflineMenu.enabled = false;
        OnlineMenu.enabled = false;
        startOnlineText.enabled = true;
        startOfflineText.enabled = true;


    }
	

    public void NoPress()
    {
        quitMenu.enabled = false;
        startOnlineText.enabled = true;
        startOfflineText.enabled = true;
        exitText.enabled = true;
    }

    public void startDeerOffline()
    {
        SceneManager.LoadScene("Scene_Deer_Offline-------------Not Working Yet");
    }

    public void startHunterOffline()
    {
        SceneManager.LoadScene("Scene_0");
    }

    public void startHunterOnline()
    {
        SceneManager.LoadScene("Scene_Hunter_Online-------------Not Working Yet");
    }

    public void startDeerOnline()
    {
        SceneManager.LoadScene("Scene_Deer_Online-------------Not Working Yet");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
