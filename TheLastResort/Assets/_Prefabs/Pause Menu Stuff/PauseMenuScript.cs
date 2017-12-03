using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class PauseMenuScript : MonoBehaviour {

    public Button MainMenuButton;
    public Button ExitButton;
    public Canvas PauseCanvas;
	private NetworkManagerOverrides lobby;
    //public Text MainMenuText;
    //ublic Text ExitText;
	// Use this for initialization
	void Start () {

		lobby = GameObject.Find("NetworkManager").GetComponent<NetworkManagerOverrides> ();
        MainMenuButton = MainMenuButton.GetComponent<Button>();
        ExitButton = ExitButton.GetComponent<Button>();
        PauseCanvas = PauseCanvas.GetComponent<Canvas>();
        //MainMenuText = MainMenuText.GetComponent<Text>();
        //ExitText = ExitText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToMainMenu()
    {
		EndScene stop = FindObjectOfType<EndScene> ();
		stop.EndGame();
        //NetworkManager.Shutdown();
        //NetworkServer.Reset();
        //SceneManager.LoadScene("Start_Menu_Scene");
       
        //Network.Disconnect();
        //MasterServer.UnregisterHost();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
