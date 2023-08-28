using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string sceneName;
    //씬 불러오는 함수 Load
    public void Load()
    {
        
        SceneManager.LoadScene(sceneName);

    }
}
