using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;


public static class my {

    public static string name = "";
    public static int pass = 0;

    public static float fps = 0.035f;

    public static float x;
    public static float y;
    public static float xn;
    public static float yn;
    public static float xw;
    public static float yw;
    public static float speed;
    public static int muki;
    public static int hp;
    public static int muteki;
    public static long muteki_time;
    public static int dead;
    public static long start_time;
    public static long stage_time;
    public static int stage_clear_time;
    public static long tirasi_time;

    public static float max_world_x;
    public static float max_world_y;
    public static float world_x;
    public static float world_y;

    public static int my_damage_efe = 0;
    public static int story = 0;
    public static int enemy_num = 0;
    public static float swim_kyori = 0;
    public static float bairitu = 1.0f;
    public static int score = 0;
    public static int hi_score = 0;
    public static int anime = 0;
    public static int anime_now = 0;
    public static int pic = 0;
    public static int stage;
    public static int max_stage = 0;
    public static int play;
    public static int clear;
    public static string tusin_mes = "";
    public static int http_load_done = 0;
    public static int tirasi_ok = 0;
    public static int load_done = 0;

    public static List<trank> rank = new List<trank>();
            
    public class trank
    {
        public string name = "";
        public int pass = 0;
        public int hi_score = 0;
        public int max_stage = 0;
        public int play_num = 0;
        public string day = "00/00/00 00:00:00";

        public trank(string i_name,int i_pass,int i_score,int i_stage,int i_play,string i_day)
        {
            name = i_name;
            pass = i_pass;
            hi_score = i_score; max_stage = i_stage; play_num = i_play; day = i_day;            
        }
    }

    public static void http_load(string s)
    {
        if (rank.Count>=1) { rank.Clear(); }//Listすべての要素を削除
        if (s=="" || s==null || s.IndexOf("sf_rank#") == -1) { my.tusin_mes = my.tusin_mes + "\nNone response"; return; }
        string[] s_text = s.Split(new string[] { "#;" }, System.StringSplitOptions.None);

        for (int i=0; i<s_text.Length; i++)
        {
            if (s_text[i].IndexOf("sf_rank#{") >= 0 && s_text[i].IndexOf("add rank >") == -1)
            {
                Debug.Log("s_text " + i + " > " + s_text[i]);
                string[] s_data = s_text[i].Split(new string[] { "#{" }, System.StringSplitOptions.None);

                string s_name = "";
                if (s_data.Length >=2 && s_data[1] != null && s_data[1] != "" )
                {
                    byte[] decodedBytes = Convert.FromBase64String(s_data[1]);
                    s_name = Encoding.UTF8.GetString(decodedBytes);
                }

                string s_day0 = "";
                string[] s_day = s_text[i].Split(new string[] { "t{" }, System.StringSplitOptions.None);
                if (s_day.Length >= 2 && s_day[1] != null && s_day[1] != "" ) {
                    s_day0 = s_day[1];
                }
                //Debug.Log("s_data " + i + " > " + s_name + " / " + s_data[2] + " / " + s_data[3] + " / " + s_data[4] + " / " + s_data[5] + " / " + s_day0);
                rank.Add(new my.trank(s_name, int.Parse(s_data[2]), int.Parse(s_data[3]), int.Parse(s_data[4]), int.Parse(s_data[5]), s_day0));
            }
        }

    }

    public static void stage_shokika()
    {
        my.dead = 0;
        my.muteki = 0;
        my.hp = 3;
        
    }
    public static void load_mode()
    {
        //max_stage = SaveData.GetFloat("max_stage");
        name = SaveData.GetString("name");
        pass = SaveData.GetInt("pass");
        max_stage = SaveData.GetInt("max_stage2");
        hi_score = SaveData.GetInt("hi_score");
        play = SaveData.GetInt("play");
        start_time = long.Parse(SaveData.GetString("start_time"));
        //tirasi_time = long.Parse(SaveData.GetString("tirasi_time"));

        if (my.start_time == 0)
        {
            my.start_time = System.DateTime.Now.Ticks;
        }

        Debug.Log("load done. start_time = " + start_time);
        
    }

    public static void save_mode()
    {
        //saveData.SetFloat("swim_kyori", swim_kyori);
        SaveData.SetString("name", name);
        SaveData.SetInt("pass", pass);
        SaveData.SetInt("max_stage2", max_stage);
        SaveData.SetInt("hi_score", hi_score);
        SaveData.SetInt("play", play);
        SaveData.SetString("start_time", start_time.ToString());
        //SaveData.SetString("tirasi_time", tirasi_time.ToString());
        SaveData.Save();
        Debug.Log("save done.");
    }

    

}
