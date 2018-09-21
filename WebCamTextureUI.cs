using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 使用摄像头作为UI背景，挂载在UI的RawImage组件下
/// </summary>
public class WebCamTextureUI: MonoBehaviour {
    public WebCamTexture webcamTexture;
    public RawImage rawImage;
    public string[] devicesNames;

	// Use this for initialization
	void Start () {
        if (rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
        }
        if (rawImage != null)
        {
            StartCoroutine(Camtexture());
        }
        
    }

    /// <summary>
    /// 控制视频的播放和停止
    /// </summary>
    /// <param name="isPlay"></param>
    public void StartPlayWebcamTexture(bool isPlay = true)
    {
        if (isPlay)
        {
            if (webcamTexture == null)
            {
                StartCoroutine(Camtexture());
            }
            else
            {
                webcamTexture.Play();
            }
        }
        else
        {
            if (webcamTexture == null)
            {
                StartCoroutine(Camtexture());
                webcamTexture.Stop();
            }
            else
            {
                webcamTexture.Stop();
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 开始读取摄像头
    /// </summary>
    /// <returns></returns>
    public IEnumerator Camtexture()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length <= 0)
            {
                yield return false;
            }
            webcamTexture = new WebCamTexture(devices[0].name, 1920, 1080,15);
            rawImage.texture = webcamTexture;
            webcamTexture.Play();
        }

    }
}
