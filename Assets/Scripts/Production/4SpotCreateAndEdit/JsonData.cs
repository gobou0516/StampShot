﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // UnityJsonを使う場合に必要
using System.IO; // ファイル書き込みに必要

[Serializable]
public class Spot
{
    public string   name    = string.Empty;
    public string   description    = string.Empty;
    public float      latitude    = 0.0f;
    public float     longitude  = 0.0f;
} 

public class JsonData : MonoBehaviour
{
    public InputField InputSpotName;
    public InputField InputSpotDescription;

    public void OnClick()
    {
        var spot = new Spot();

        //データ入力
        spot.name = InputSpotName.text; //スポット名
        spot.description = InputSpotDescription.text; //スポットの説明
        spot.latitude = GetLocation.latitude; //緯度
        spot.longitude = GetLocation.longitude; //経度

        // JSONにシリアライズ
        var json = JsonUtility.ToJson (spot);

        // フォルダに保存する
        var path = Application.dataPath + "/Json/" + spot.name + ".json";//ファイル名をspot名にしてファイル指定
        var writer = new StreamWriter (path, false); // 上書き
        writer.WriteLine (json);
        writer.Flush ();
        writer.Close ();
    }
}

    //配列を作成するためのクラス
public static class JsonHelper 
{ 
    public static T[] FromJson<T>(string json) 
    { 
     Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json); 
     return wrapper.Items; 
    } 

    public static string ToJson<T>(T[] array) 
    { 
     Wrapper<T> wrapper = new Wrapper<T>(); 
     wrapper.Items = array; 
     return JsonUtility.ToJson(wrapper); 
    } 

    public static string ToJson<T>(T[] array, bool prettyPrint) 
    { 
     Wrapper<T> wrapper = new Wrapper<T>(); 
     wrapper.Items = array; 
     return JsonUtility.ToJson(wrapper, prettyPrint); 
    } 

    [Serializable] 
    private class Wrapper<T> 
    { 
     public T[] Items; 
    } 
} 