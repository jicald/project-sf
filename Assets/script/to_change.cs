using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class to_change : MonoBehaviour {

  

    public void on_click(string button_name)
    {
        
        audio_manager.Instance.playSE("pin");

        if (button_name == "START")
        {
            audio_manager.Instance.play_bgm("2", 0.3f);
            SceneManager.LoadScene("main");

        } else if (button_name == "to_title")
        {
            SceneManager.LoadScene("title");
            Debug.Log("to title");
            
           
        }

    }
}
