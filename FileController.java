package com.unity.androidplugin;
import android.content.Intent;
import android.os.Bundle;
import android.os.Environment;
import android.widget.Toast;
import android.content.Context;

import com.unity3d.player.UnityPlayerActivity;

import java.io.File;
import java.io.FileInputStream;
import java.io.FilenameFilter;

public class FileController {

    public static Context ShowContext = null;

public static void SetContext(Context con)
{
	ShowContext = con;
}
	
    public static void ShowToast(String info,Context context)
    {
        Toast.makeText(context,info,Toast.LENGTH_LONG).show();
    }

	 public static void ShowToast(String info)
    {
        Toast.makeText(ShowContext,info,Toast.LENGTH_LONG).show();
    }



    public static byte[] ReadFileData(String Path)
    {
        File file = new File(Path);
        byte[] data = null;
        if(file.exists()) {
            try {
                FileInputStream fs = new FileInputStream(file.getAbsolutePath());
                data = new byte[(int)file.length()];
                fs.read(data,0,data.length);
                fs.close();
                return  data;
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
        return  data;
    }

    public static boolean CreateFile(String Path)
    {
        File file = new File(Path);
        if(file.exists())
        {
            ShowToast("文件已存在："+file.getAbsolutePath());
            return  false;
        }else
        {
            try
            {
                file.createNewFile();
                ShowToast("文件创建成功：" + file.getAbsolutePath());
                return true;
            }
            catch (Exception e)
            {
                e.printStackTrace();
                return false;
            }
        }
    }

	    public static void CreateFileTest(String FileName)
    {
        File file = new File(Environment.getDataDirectory(),FileName);
        if(file.exists())
        {
            ShowToast("文件已存在："+file.getAbsolutePath());
        }else
        {
            try
            {
                file.createNewFile();
                ShowToast("文件创建成功：" + file.getAbsolutePath());
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
        }
    }

    public static boolean LoadFileTest(String Path)
    {
        File file = new File(Path);
        if(file.exists())
        {
                byte[] data = ReadFileData(file.getAbsolutePath());
                ShowToast(String.format("FileSize%1s:%2s %3s %4s %5s.",file.length(),data[0],data[1],data[2],data[3]));
                return  true;
        }else
        {
            ShowToast("文件不存在："+file.getAbsolutePath());
            return false;
        }
    }

    public static int GetFileSize(String Path)
    {
        File file = new File(Path);
        if(file.exists())
        {
            ShowToast("文件大小："+file.length());
            return  (int)file.length();
        }else
        {
            ShowToast("文件不存在："+file.getAbsolutePath());
            return -1;
        }
    }

    public static String GetPath()
    {
		ShowToast(Environment.getExternalStorageDirectory().getAbsolutePath());
        return Environment.getExternalStorageDirectory().getAbsolutePath();
		
    }

    public static String[] GetALLFiles(String Path,String Fliter)
    {
        File file = new File(Path);
        return file.list();
    }

}
