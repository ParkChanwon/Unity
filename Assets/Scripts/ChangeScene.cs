using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click_to_Main(){
        SceneManager.LoadScene("MainScene");
    }
    public void Click_to_GameStage(){
        SceneManager.LoadScene("GameScene1");
    }

    public void Click_to_ExplainScene(){
        SceneManager.LoadScene("Explain");
    }
}
