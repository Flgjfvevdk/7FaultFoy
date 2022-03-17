using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_BoutonPause : MonoBehaviour
{
    public SC_ManageClient mngClient;
    
    public void reprendreBtn()
    {
        mngClient.pauseMenu();
    }

    public void menuBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void quitterBtn()
    {
        Application.Quit();
    }

}
