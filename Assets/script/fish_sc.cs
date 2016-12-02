using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class fish_sc : SingletonMonoBehaviour<fish_sc>
{
    public int fish_n = 0;
    public GameObject fish_prefab;
    public GameObject shell_prefab;

    [System.Serializable]
    public class te_d
    {
        public int c;
        public int kind = 0;
        public int enabled;
        public int muki=0;
        public string name = "";
        public float x = 0f;
        public float y = 0f;
        public float xn = 0f;
        public float yn = 0f;
        public GameObject fish;
       
        public te_d(string s_name, float ix, float iy, float ixn, float iyn)
        {
            name = s_name;
            x = ix;
            y = iy;
            xn = ixn;
            yn = iyn;
            c = 1;
            enabled = 0;
            kind = UnityEngine.Random.Range(0, 2);//Debug.Log(kind);
        }
        public te_d()
        {
            name = "";            
            c = 0;
            enabled = 1;
        }
    }
    [SerializeField] public List<te_d> e_d;

    public void Start()
    {
        fish_prefab = Resources.Load<GameObject>("fish");
        shell_prefab = Resources.Load<GameObject>("shell");
        for (int i = 0; i < 19; i++)
        {
            e_d.Add(new te_d());
        }
    }

    public int fish_num()
    {
        int c = 0;
        for (int i =0; i<e_d.Count;i++)
        {
            if (e_d[i].c != 0) { c++; }
        }
        return c;
    }
    public void fish_on()
    {
        fish_n++; if (fish_n>=20) { fish_n = 0; }
        int i = fish_n;
        if (e_d[i].c == 0)
        {
            e_d[i] = new te_d("fish," + i, transform.position.x, transform.position.y, -0.05f, -0.15f);
            if (e_d[i].kind == 0) { e_d[i].fish = (GameObject)Instantiate(fish_prefab, transform.position, Quaternion.identity); }
            if (e_d[i].kind == 1) { e_d[i].fish = (GameObject)Instantiate(shell_prefab, transform.position, Quaternion.identity);
                e_d[i].xn = -0.112f + UnityEngine.Random.Range(0.0f, 0.112f * 2);
                e_d[i].yn = -0.18f + UnityEngine.Random.Range(0.0f, 0.03f);
                e_d[i].name="shell,"+i;
            }

            e_d[i].fish.name = e_d[i].name;
            //DontDestroyOnLoad(e_d[i].fish);
        }
    }
    public void fish_dest(int c)
    {
        //Debug.Log("dest fish " + c);
        Destroy(e_d[c].fish);
        e_d[c].c = 0; e_d[c].enabled = 0;
        audio_manager.Instance.playSE("coin");
    }
    void Update()
    {
        if (e_d != null) {
            for (int i=0; i < e_d.Count; i++)
            {
                if (e_d[i].c >= 1)
                {
                    e_d[i].fish.transform.position = new Vector3(e_d[i].x, e_d[i].y, 0);
                    if (Mathf.Abs(e_d[i].x - e_d[i].xn) > 0.005)
                    {
                        e_d[i].x -= (e_d[i].x - e_d[i].xn) / 50;
                    }
                    if (Mathf.Abs(e_d[i].y - e_d[i].yn) > 0.005)
                    {
                        e_d[i].y -= (e_d[i].y - e_d[i].yn) / 50;
                    }
                    if (Mathf.Abs(e_d[i].x - e_d[i].xn) <= 0.005 && Mathf.Abs(e_d[i].y - e_d[i].yn) <= 0.005)
                    {
                        if (e_d[i].enabled == 0 && e_d[i].c==1) { e_d[i].enabled = 1; e_d[i].c = 2; }
                        if (e_d[i].c == 2 && e_d[i].kind == 0)
                        {
                            e_d[i].xn = -0.112f + UnityEngine.Random.Range(0.0f, 0.112f * 2);
                            e_d[i].yn = -0.18f + UnityEngine.Random.Range(0.0f, 0.07f);
                        }
                    }
                    if (e_d[i].xn - e_d[i].x < 0) { e_d[i].muki = 0; } else { e_d[i].muki = 1; }
                    if (e_d[i].muki == 0) { e_d[i].fish.transform.localScale = new Vector3(0.1f, 0.1f, 1); } else { e_d[i].fish.transform.localScale = new Vector3(-0.1f, 0.1f, 1); }


                    /*if (e_d[i].x > 0.112) { e_d[i].x = 0.112f; }
                    if (e_d[i].x < -0.112) { e_d[i].x = -0.112f; }
                    if (e_d[i].y > -0.11) { e_d[i].y = -0.11f; }
                    if (e_d[i].y < -0.18) { e_d[i].y = -0.18f; }*/

                }
            }
        }
    }



    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        //DontDestroyOnLoad(this.gameObject);
        
    }



}
