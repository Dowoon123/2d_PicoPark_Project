using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string sceneName;
    //�� �ҷ����� �Լ� Load
    public void Load()
    {
        
        SceneManager.LoadScene(sceneName);

    }
}
