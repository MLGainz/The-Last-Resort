using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button exitText;
    public Button startOnlineText;
	//public Text controls;
    
    

	
	void Start () {

        quitMenu = quitMenu.GetComponent<Canvas>();
        startOnlineText = startOnlineText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
		//controls = controls.GetComponent<Text> ();
        quitMenu.enabled = false;
		//controls.enabled = false;
    }
	
   

    public void OnlinePress()
    {
       
        startOnlineText.enabled = false;
        startOnlineLobby();
        
    }

	//public void ControlPress(){
	///	if (controls.enabled = true) {
	//		controls.enabled = false;
	//	} else {
	//		controls.enabled = true;
	//	}
	//}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startOnlineText.enabled = false;
        exitText.enabled = false;


    }

    public void goBackPress()
    {
        quitMenu.enabled = false;
        startOnlineText.enabled = true;
      

    }
	

    public void NoPress()
    {
        quitMenu.enabled = false;
        startOnlineText.enabled = true;
        exitText.enabled = true;
    }

   

    public void startOnlineLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

   

    public void ExitGame()
    {
        Application.Quit();
    }
}
