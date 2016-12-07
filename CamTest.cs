using UnityEngine;  
using System.Collections;  

public class CamTest : MonoBehaviour  
{  
	private float speed = 0;//
    private float SpeedRota = 10.0f;//镜头旋转速度

    private Vector3 V3_Rota;
	
	// Use this for initialization  
	void Start()  
	{  
	}  
	
	// Update is called once per frame  
	void Update()  
	{
        if (Input.GetMouseButton(2))
        {
            FollowMouse();
        }
	}

    /// <summary>
    /// 镜头跟随鼠标旋转
    /// </summary>
    void FollowMouse()
    {
        float xRot = Input.GetAxis("Mouse Y") * (-1) * SpeedRota;
        float yRot = Input.GetAxis("Mouse X") * SpeedRota;
        V3_Rota.x = xRot;
        V3_Rota.y = yRot;
        RotateOnXY(V3_Rota);
    }

    bool RotateOnXY(Vector3 v3)
    {
        this.transform.RotateAround(this.transform.position, Vector3.up, v3.y);
        this.transform.RotateAround(this.transform.position, this.transform.right, v3.x);
        return true;
    }
}  