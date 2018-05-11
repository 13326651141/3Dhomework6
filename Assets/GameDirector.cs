using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : System.Object
{
    private static GameDirector _instance;
    public SceneController currentSceneController { get; set; }
    //场景控制器


    //单例模式 

    public static GameDirector getInstance()
    {
        if (_instance == null)
        {
            _instance = new GameDirector();
        }
        return _instance;
    }
    public int getFPS()
    {
        //得到targetFrameRate
        return Application.targetFrameRate;
    }
    public void setFPS(int fps)
    {
        //设置targetFrameRate
        Application.targetFrameRate = fps;
    }
}