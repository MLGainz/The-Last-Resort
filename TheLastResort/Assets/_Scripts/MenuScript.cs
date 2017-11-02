using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button startHunterText;
    public Button startDeerText;
    public Button exitText;

	// Use this for initialization
	void Start () {

        quitMenu = quitMenu.GetComponent<Canvas>();
        startHunterText = startHunterText.GetComponent<Button>();
        startDeerText = startDeerText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
		
	}
	
	public void ExitPress()
    {
        quitMenu.enabled = true;
        startHunterText.enabled = false;
        startDeerText.enabled = false;
        exitText.enabled = false;


    }
	

    public void NoPress()
    {
        quitMenu.enabled = false;
        startHunterText.enabled = true;
        startDeerText.enabled = true;
        exitText.enabled = true;
    }

    public void StartDeerLevel()
    {
        SceneManager.LoadScene("DeerScene");
    }

    public void StartHunterLevel()
    {
        SceneManager.LoadScene("HunterScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
