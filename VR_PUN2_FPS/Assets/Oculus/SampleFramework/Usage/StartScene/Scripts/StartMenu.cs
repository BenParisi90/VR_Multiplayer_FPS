using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun.Demo.PunBasics;

// Create menu of all scenes included in the build.
public class StartMenu : MonoBehaviour
{   
    public OVROverlay overlay;
    public OVROverlay text;
    public OVRCameraRig vrRig;
    public Launcher launcher;

    void Start()
    {
        DebugUIBuilder.instance.AddLabel("Select Sample Scene");
        
        DebugUIBuilder.instance.AddButton("Start", () => LoadScene(1));
        
        DebugUIBuilder.instance.Show();
    }

    void LoadScene(int idx)
    {
        DebugUIBuilder.instance.Hide();
        //Debug.Log("Load scene: " + idx);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(idx);
        launcher.Connect();
    }
}
