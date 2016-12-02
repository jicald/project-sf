using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class http_sc : MonoBehaviour
{
    // URL
    string url = "http://andymente.moo.jp/html/game/app/shooting_flower/sf.php";
    // サーバへリクエストするデータ
    
    // タイムアウト時間
    float timeoutsec = 5f;

    void Awake()
    {
        
    }

  
    public void http_on()
    {
        my.tusin_mes = "Now Loading....";
        my.http_load_done = 0;

        string user_name = my.name;
        string user_pass = ""+my.pass;


        string user_score = "" + my.hi_score;
        string user_max_stage = "" + my.max_stage;
        string user_play_num = "" + my.play;

        string user_data = u_encode(user_name)+"#{" + user_pass + "#{" +
                           user_score + "#{" + user_max_stage + "#{" + user_play_num + "#{";


        // サーバへPOSTするデータを設定 
        /*
         * Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("id", user_id);
        dic.Add("name", user_name);
        dic.Add("data", user_data);
        StartCoroutine(HttpPost(url, dic));  // POST
        */

        // サーバへGETするデータを設定
     
        user_data = WWW.EscapeURL(user_data);
        
        string get_param = "?add=" + user_data;
        //Debug.Log(get_param);
        StartCoroutine(HttpGet(url + get_param));  // GET
    }

    string u_encode(string s)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Convert.ToBase64String(bytes);
    }
    
    // HTTP POST リクエスト
    IEnumerator HttpPost(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<String, String> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);

        // CheckTimeOut()の終了を待つ。5秒を過ぎればタイムアウト
        yield return StartCoroutine(CheckTimeOut(www, timeoutsec));

        if (www.error != null)
        {
            Debug.Log("HttpPost NG: " + www.error);
        }
        else if (www.isDone)
        {
            // サーバからのレスポンスを表示
            Debug.Log("HttpPost OK: " + www.text);
        }
    }

    // HTTP GET リクエスト
    IEnumerator HttpGet(string url)
    {
        WWW www = new WWW(url);

        // CheckTimeOut()の終了を待つ。5秒を過ぎればタイムアウト
        yield return StartCoroutine(CheckTimeOut(www, timeoutsec));

        if (www.error != null)
        {
            Debug.Log("HttpGet NG: " + www.error);
            my.tusin_mes = "Loading Error....Try again later.";
        }
        else if (www.isDone)
        {
            // サーバからのレスポンスを表示
            my.tusin_mes = "Load done.";
            Debug.Log("HttpGet OK: " + www.text);
            my.http_load(www.text);
            my.http_load_done = 1;
        }
    }

    // HTTPリクエストのタイムアウト処理
    IEnumerator CheckTimeOut(WWW www, float timeout)
    {
        float requestTime = Time.time;

        while (!www.isDone)
        {
            if (Time.time - requestTime < timeout)
                yield return null;
            else
            {
                Debug.Log("TimeOut");  //タイムアウト
                my.tusin_mes = "Time out....Try again later.";
                break;
            }
        }
        yield return null;
    }
}