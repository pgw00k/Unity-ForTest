//2018-1-17
//镜头跟随鼠标变化
//v1-基本操作

using UnityEngine;
using System.Collections;

public class CamTest : MonoBehaviour
{
    private float SpeedTransform = 1f;//镜头位移速度
    private float SpeedRota = 10.0f;//镜头旋转速度
    private float SpeedZoom = 5.0f;//镜头缩放速度

    private Vector3 V3_Rota;

    //变换开关
    private bool bTransform;
    private bool bRotate;

    // Use this for initialization  
    void Start()
    {
        bTransform = false;
        bRotate = false;
    }

    // Update is called once per frame  
    void Update()
    {
        bTransform = Input.GetMouseButton(2);
        bRotate = Input.GetMouseButton(1);
        if (bRotate)
        {
            RotateByMouse();
        }
        if (bTransform)
        {
            TranslateByMouse();
        }
        if (Input.GetAxis("Mouse ScrollWheel") !=0)
        {
            Debug.LogFormat("CameTest:Mouse ScrollWheel:{0}",Input.GetAxis("Mouse ScrollWheel"));
            ScaleByMouse();
        }
    }

    /// <summary>
    /// 镜头跟随鼠标旋转
    /// </summary>
    void RotateByMouse()
    {
        float xRot = Input.GetAxis("Mouse Y") * (-1) * SpeedRota;
        float yRot = Input.GetAxis("Mouse X") * SpeedRota;
        V3_Rota.x = xRot;
        V3_Rota.y = yRot;
        RotateOnXY(V3_Rota);
    }

    void RotateOnXY(Vector3 v3)
    {
        this.transform.RotateAround(this.transform.position, Vector3.up, v3.y);
        this.transform.RotateAround(this.transform.position, this.transform.right, v3.x);
    }

    /// <summary>
    /// 镜头跟随鼠标位移
    /// </summary>
    void TranslateByMouse()
    {
        float y = Input.GetAxis("Mouse Y") * SpeedTransform;
        float x = Input.GetAxis("Mouse X") * SpeedTransform;

        transform.Translate(x,y,0,Space.Self);

        Debug.LogFormat("CamTest:X = {0},Y = {1}",x,y);
    }



    void ScaleByMouse()
    {
        float z = Input.GetAxis("Mouse ScrollWheel") * SpeedZoom;
        transform.Translate(0, 0, z,Space.Self);
    }
}
