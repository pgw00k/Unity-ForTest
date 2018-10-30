using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class Test : MonoBehaviour {
    public UnityEngine.UI.Text Log;
    public UnityEngine.UI.InputField InputPath;

    // Use this for initialization
    void Start () {
        InputPath.text = Application.persistentDataPath;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void BtnTestUnity()
    {
        if(File.Exists(InputPath.text))
        {
            Log.text += "文件已存在\n";

        }else
        {
            try
            {
                FileStream fs = new FileStream(InputPath.text, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Close();
                fs.Dispose();
            }catch(Exception e)
            {
                Log.text += "文件创建失败：" + e.Message;
            }
        }
    }

    public void BtnTestJava()
    {
        Log.text += "Unity Call :";
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity.androidplugin.FileController");
            AndroidJavaClass jc_UnityDefault = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo_UnityActivity = jc_UnityDefault.GetStatic<AndroidJavaObject>("currentActivity");
            jc.CallStatic("SetContext",jo_UnityActivity);
            jc.CallStatic("ShowToast", "Unity Call");
            jc.CallStatic("CreateFileTest","JavaCreatTest");
            //Log.text += jc.CallStatic<int>("Test", 3) + "\n";
            //AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("com.unity.standard.FileActivity");
            //jo.Call("ShowToast", "Unity Call");
        }
        catch (Exception e)
        {
            Log.text += e.Message + " \n";
        }
        finally
        {
            Log.text += " Success\n";
        }
    }
}
