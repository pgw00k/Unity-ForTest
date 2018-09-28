using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

/// <summary>
/// 使用WWW类来实现的Http回调
/// 只用于测试，需要挂载在场景中
/// 即必须实例化，否则无法使用Unity的StartCoroutine函数
/// </summary>
public class HttpRequestWWW : MonoBehaviour {
    public string URLInspector;
    public string FilePath;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (GUILayout.Button("Link"))
        {
            if (URLInspector != null || URLInspector != string.Empty)
            {
                StartLink(URLInspector);
            }
        }
    }
#endif

    public void StartLink(string URL =null)
    {
        if (URL == null && URLInspector != null)
        {
            URL = URLInspector;
        }
        FilePath = Application.persistentDataPath + "/" + DateTime.Now.ToString("yyyy-MM-dd-hhmmss") + ".png";
        ScreenCapture.CaptureScreenshot(FilePath);
        StartCoroutine(HttpRequest(URL));

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Http请求的主要函数
    /// </summary>
    /// <param name="URL"></param>
    /// <returns></returns>
    public IEnumerator HttpRequest(string URL)
    {
        yield return new WaitForSeconds(3);
        JsonData data = new JsonData();
        byte[] FileData = File.ReadAllBytes(FilePath);
        data["img"] = Convert.ToBase64String(FileData);

        WWW www = new WWW(URL,System.Text.Encoding.Default.GetBytes(data.ToJson()));
        yield return www;
        Debug.LogFormat("HttpRequestResult:{0}", www.text);
        JsonData returnData = JsonMapper.ToObject(www.text);
        if ((bool)returnData["result"])
        {
            //StartDownload(returnData["url"].ToString());
            WWW www_2 = new WWW(returnData["url"].ToString());
            yield return www_2;
            try
            {
                GetComponent<RawImage>().texture = www_2.texture;
            }
            finally
            { }
        }
    }

    public void StartDownload(string url)
    {
        StartCoroutine(url);
    }


    /// <summary>
    /// 下载并显示二维码
    /// </summary>
    /// <param name="URL"></param>
    /// <returns></returns>
    public IEnumerator HttpRequestOver(string URL)
    {
        WWW www = new WWW(URL);
        yield return www;
        try
        {
            GetComponent<RawImage>().texture = www.texture;
        }
        finally
        { }
    }
}
