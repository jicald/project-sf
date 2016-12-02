using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class audio_manager : SingletonMonoBehaviour<audio_manager>
    {


    public float volume_d;
            
    public AudioSource bgm_source = null;
    public AudioSource se_source = null;
    public GameObject audio_box_obj;

    [System.Serializable]
    public class tse_d
    {
        public String name;
        public AudioClip audio;

        public tse_d(string s_name,AudioClip i_audio)
        {
            name = s_name; audio = i_audio;
        }

    }
    [SerializeField] public List<tse_d> se_d;



    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        audio_box_obj = GameObject.Find("audio_box");
        //audio_box_obj.AddComponent<AudioListener>();
        se_source = audio_box_obj.AddComponent<AudioSource>();
        bgm_source = audio_box_obj.AddComponent<AudioSource>();



        object[] se_list = Resources.LoadAll("se");
        foreach (AudioClip se in se_list)
        {
            se_d.Add(new tse_d(se.name, se));
        }
    }
    public void play_bgm(string s, float i_volume)
    {
        object bgm_d = Resources.Load("bgm/" + s);
        bgm_source.clip = bgm_d as AudioClip;
        bgm_source.volume = i_volume;
        bgm_source.Play();

    }
    public float bgm_time()
    {
        return bgm_source.time;
        
    }
    public void playSE(string seName)
    {
        int ic = -1;
        for (int i = 0; i < se_d.Count; i++)
        {
            if (se_d[i].name == seName)
            {
                ic = i;
            }
        }
        if (ic != -1)
        {
            //se_source.clip = se_d[ic].audio;
            se_source.pitch = 1;
            se_source.volume = volume_d;
            se_source.PlayOneShot(se_d[ic].audio as AudioClip);
            

        }
        else
        {
            Debug.Log("non audio ic = -1 ");
        }


    }
    public void play_pitch(string seName,float i_pitch)
    {
        int ic = -1;
        for (int i = 0; i < se_d.Count; i++)
        {
            if (se_d[i].name == seName)
            {
                ic = i;
            }
        }
        if (ic != -1)
        {
            //se_source.clip = se_d[ic].audio;
            se_source.volume = volume_d;
            se_source.pitch = i_pitch;
            se_source.PlayOneShot(se_d[ic].audio as AudioClip);


        }
        else
        {
            Debug.Log("non audio ic = -1 ");
        }

    }
    /*


        public void PlaySE(string seName)
        {
            if (!this.seDict.ContainsKey(seName)) throw new ArgumentException(seName + " not found", "seName");

            AudioSource source = this.seSources.FirstOrDefault(s => !s.isPlaying);
            if (source == null)
            {
                if (this.seSources.Count >= this.MaxSE)
                {
                    Debug.Log("SE AudioSource is full");
                    return;
                }

                source = this.gameObject.AddComponent<AudioSource>();
                this.seSources.Add(source);
            }

            source.clip = this.seDict[seName];
            source.Play();
        }
        // Use this for initialization
        void Start () {

        }

        // Update is called once per frame
        void Update () {

        }
        */
}
