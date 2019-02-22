using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public SceneAsset  SingleScene;
    public SceneAsset  MultiScene;
    public void OnSingle()
    {
        SceneManager.LoadScene(SingleScene.name);
    }
    public void OnMulti()
    {
        SceneManager.LoadScene(MultiScene.name);
    }
}
