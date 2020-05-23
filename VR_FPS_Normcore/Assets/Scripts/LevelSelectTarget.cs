using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectTarget : TargetController
{
    public string sceneToLoad;
    public TextMeshPro levelText;

    void Start()
    {
        levelText.text = sceneToLoad;
    }

    override public void TakeDamage()
    {
        base.TakeDamage();
        //SceneManager.LoadScene(sceneToLoad);
        GameController.instance.ChangeScene(sceneToLoad);
    }
}
