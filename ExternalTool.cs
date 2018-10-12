using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExternalTool : MonoBehaviour
{
    [MenuItem("Tool/GetComponent")]
    public static void GetComponent()
    {
        List<CameraFacingBillboard> list = GetAllObjsOfType<CameraFacingBillboard>(false);
        foreach (CameraFacingBillboard c in list)
        {
            c.enabled = false;
        }
    }


    public static List<T> GetAllObjsOfType<T>(bool onlyRoot) where T : Component
    {
        T[] Objs = (T[])Resources.FindObjectsOfTypeAll(typeof(T));

        List<T> returnObjs = new List<T>();

        foreach (T Obj in Objs)
        {
            if (onlyRoot)
            {
                if (Obj.transform.parent != null)
                {
                    continue;
                }
            }

            if (Obj.hideFlags == HideFlags.NotEditable || Obj.hideFlags == HideFlags.HideAndDontSave)
            {
                continue;
            }

            if (Application.isEditor)
            {
                //检测资源是否存在，不存在会返回null或empty的字符串，存在会返回文件名
                string sAssetPath = AssetDatabase.GetAssetPath(Obj.transform.root.gameObject);
                if (!string.IsNullOrEmpty(sAssetPath))
                {
                    continue;
                }
            }

            returnObjs.Add(Obj);
        }

        return returnObjs;
    }
}
