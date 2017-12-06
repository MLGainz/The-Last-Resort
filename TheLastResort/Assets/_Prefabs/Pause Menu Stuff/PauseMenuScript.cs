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
   
	void Start () {

		lobby = GameObject.Find("NetworkManager").GetComponent<NetworkManagerOverrides> ();
        MainMenuButton = MainMenuButton.GetComponent<Button>();
        ExitButton = ExitButton.GetComponent<Button>();
        PauseCanvas = PauseCanvas.GetComponent<Canvas>();
       
	}
	
	

    public void GoToMainMenu()
    {
		EndScene stop = FindObjectOfType<EndScene> ();
		stop.EndGame();
        SceneManager.LoadScene("Lobby");
        Cursor.visible = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
