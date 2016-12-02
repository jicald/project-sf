using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class efe_sc : SingletonMonoBehaviour<efe_sc>
{
    public int efe_n = 0;
    public GameObject efe_prefab;
    public GameObject efe_text_prefab;
    float timeOut = 0.033f;
    float timeElapsed;
    GameObject canvas_obj;

    [System.Serializable]
    public class tefe
    {
        public int c;
        public int kind = 0;
        public int text_efe = 0;
        public float x = 0f;
        public float y = 0f;
        public float xn = 0f;
        public float yn = 0f;
        public float xw = 0f;
        public float yw = 0f;
        public float slow = 50f;
        public float zx = 0.05f;
        public float zy = 0.05f;
        public float alpha = 1;
        public Color color;

        public GameObject fish;

        public tefe(int i_kind, float ix, float iy)
        {
            x = ix;
            y = iy;
            c = 1;
            kind = i_kind;
        }
        public tefe()
        {
            c = 0;
        }
    }
    [SerializeField]
    public List<tefe> efe_d;

    public void Start()
    {
        //efe_prefab = Resources.Load<GameObject>("fish");
        for (int i = 0; i < 100; i++)
        {
            efe_d.Add(new tefe());
        }
        canvas_obj = GameObject.Find("Canvas");
    }

    public int efe_num()
    {
        int c = 0;
        for (int i = 0; i < efe_d.Count; i++)
        {
            if (efe_d[i].c != 0) { c++; }
        }
        return c;
    }
    public void efe_on(int k, float x, float y)
    {
        efe_on0(k, x, y, new Vector2(0.05f, 0.05f),0,0);
    }
    public void efe_on(int k, float x, float y, Vector2 v_scale)
    {
        efe_on0(k, x, y, v_scale,0,0);
    }
    public void efe_on(int k, float x, float y, Vector2 v_scale, float half_between_x, float half_between_y)
    {
        efe_on0(k, x, y, v_scale,half_between_x,half_between_y);
    }

    public void efe_on0(int k, float x, float y,Vector2 v_scale, float half_between_x, float half_between_y)
    {
        efe_n++; if (efe_n >= 100) { efe_n = 0; }
        int i = efe_n;
        if (efe_d[i].c == 0)
        {
            efe_d[i] = new tefe(k, x, y);
            efe_d[i].fish = (GameObject)Instantiate(efe_prefab, new Vector2(x, y), Quaternion.identity);
            
            efe_d[i].fish.name = "efe," + i;
            efe_d[i].fish.transform.localScale = v_scale;
            if (efe_d[i].fish.GetComponent<SpriteRenderer>()) { efe_d[i].color = efe_d[i].fish.GetComponent<SpriteRenderer>().color; }
            efe_d[i].x = x + UnityEngine.Random.Range(0, half_between_x) - UnityEngine.Random.Range(0, half_between_x);
            efe_d[i].y = y + UnityEngine.Random.Range(0, half_between_y) - UnityEngine.Random.Range(0, half_between_y);
            //efe_d[i].xw = efe_d[i].fish.GetComponent<SpriteRenderer>().bounds.size.x;
            //efe_d[i].yw = efe_d[i].fish.GetComponent<SpriteRenderer>().bounds.size.y;
            efe_d[i].zx = v_scale.x;
            efe_d[i].zy = v_scale.y;
            efe_d[i].text_efe = 0;

            if (efe_d[i].kind == 0)
            {
                efe_d[i].xn = efe_d[i].x - UnityEngine.Random.Range(0.0f, half_between_x) + UnityEngine.Random.Range(0.0f, half_between_y);
                efe_d[i].yn = -0.18f + UnityEngine.Random.Range(0.0f, 0.03f);
            }
            if (efe_d[i].kind == 1)
            {
                efe_d[i].x = x;
                efe_d[i].y = y;
                efe_d[i].slow = 10;
                efe_d[i].color = new Color(1, 0, 0, 1);
                float i_kaku = UnityEngine.Random.Range(0f, 360f);
                float l = UnityEngine.Random.Range(0f, efe_d[i].zx);

                efe_d[i].xn = x + Mathf.Cos(i_kaku * (Mathf.PI / 180)) * l;
                efe_d[i].yn = y + Mathf.Sin(i_kaku * (Mathf.PI / 180)) * l;

            }
            if (efe_d[i].kind == 2)
            {
                efe_d[i].xn = x;
                efe_d[i].yn = y - efe_d[i].zy / 2;
                efe_d[i].slow = 100;
                efe_d[i].color = new Color(0.5f, 0.5f, 1, 1);
            }
            if (efe_d[i].kind == 3)
            {
                efe_d[i].xn = x;
                efe_d[i].yn = y + efe_d[i].zy / 2;
                efe_d[i].slow = 100;
                

            }

        }
    }
    public void efe_text_on(int k, float x, float y, int i_size, string s,Color i_color)
    {
        efe_n++; if (efe_n >= 100) { efe_n = 0; }
        int i = efe_n;
        if (efe_d[i].c == 0)
        {
            efe_d[i] = new tefe(k, x, y);
            efe_d[i].fish = (GameObject)Instantiate(efe_text_prefab, new Vector2(x, y), Quaternion.identity);
            efe_d[i].fish.GetComponent<Text>().text = s;

            efe_d[i].fish.name = "efe_text," + i;
            efe_d[i].fish.GetComponent<Text>().fontSize = i_size;
            efe_d[i].color = i_color;
            efe_d[i].x = x;
            efe_d[i].y = y;
            efe_d[i].zx = 1;
            efe_d[i].zy = 1;
            efe_d[i].text_efe = 1;
            efe_d[i].fish.transform.SetParent(canvas_obj.transform);
            efe_d[i].fish.transform.localScale = new Vector2(0.1f, 0.1f);
            

            if (efe_d[i].kind == 0)
            {
                efe_d[i].xn = x;
                efe_d[i].yn = y + my.max_world_y / 20;
                efe_d[i].slow = 100;
            }
        }
    }


    public void efe_dest(int c)
    {
        if (efe_d[c].c >= 1)
        {
            Destroy(efe_d[c].fish);
            efe_d[c].c = 0;
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            main_phase();
            timeElapsed = 0.0f;
        }
    }

    int t_c = 0;
    void main_phase()
    {
        t_c++; if (t_c > 100000) { t_c = 0; }

        if (efe_d != null)
        {
            for (int i = 0; i < efe_d.Count; i++)
            {
                if (efe_d[i].c >= 1)
                {
                    if (efe_d[i].text_efe == 0)
                    {
                        efe_d[i].fish.transform.position = new Vector2(efe_d[i].x, efe_d[i].y);
                        if (Mathf.Abs(efe_d[i].x - efe_d[i].xn) > 0.005)
                        {
                            efe_d[i].x -= (efe_d[i].x - efe_d[i].xn) / efe_d[i].slow;
                        }
                        if (Mathf.Abs(efe_d[i].y - efe_d[i].yn) > 0.005)
                        {
                            efe_d[i].y -= (efe_d[i].y - efe_d[i].yn) / efe_d[i].slow;
                        }
                        if (Mathf.Abs(efe_d[i].x - efe_d[i].xn) <= 0.005 && Mathf.Abs(efe_d[i].y - efe_d[i].yn) <= 0.005)
                        {

                        }

                        efe_d[i].fish.transform.localScale = new Vector2(efe_d[i].zx, efe_d[i].zy);
                        efe_d[i].color.a = efe_d[i].alpha;
                        //efe_d[i].fish.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, efe_d[i].alpha);
                        efe_d[i].fish.GetComponent<SpriteRenderer>().color = efe_d[i].color; 

                        efe_d[i].alpha -= 0.02f;
                        efe_d[i].zx = efe_d[i].zx * 0.95f - 0.002f;
                        efe_d[i].zy = efe_d[i].zy * 0.95f - 0.002f;
                        if (efe_d[i].zx <= 0 || efe_d[i].alpha <= 0) { efe_dest(i); }
                    } else //テキスト
                    {
                        efe_d[i].fish.transform.localScale = new Vector2(efe_d[i].zx, efe_d[i].zy);
                        efe_d[i].color.a = efe_d[i].alpha;
                        efe_d[i].fish.GetComponent<Text>().color = efe_d[i].color;


                        var pos = Vector2.zero;
                        var uiCamera = Camera.main;
                        var worldCamera = Camera.main;
                        var canvasRect = canvas_obj.GetComponent<RectTransform>();

                        var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, new Vector2(efe_d[i].x,efe_d[i].y));
                        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out pos);
                        efe_d[i].fish.GetComponent<RectTransform>().localPosition = pos;

                        if (Mathf.Abs(efe_d[i].x - efe_d[i].xn) > 0.005)
                        {
                            efe_d[i].x -= (efe_d[i].x - efe_d[i].xn) / efe_d[i].slow;
                        }
                        if (Mathf.Abs(efe_d[i].y - efe_d[i].yn) > 0.005)
                        {
                            efe_d[i].y -= (efe_d[i].y - efe_d[i].yn) / efe_d[i].slow;
                        }
                        if (Mathf.Abs(efe_d[i].x - efe_d[i].xn) <= 0.005 && Mathf.Abs(efe_d[i].y - efe_d[i].yn) <= 0.005)
                        {

                        }
                        
                        efe_d[i].alpha -= 0.02f;
                        //efe_d[i].zx = efe_d[i].zx * 0.95f - 0.002f;
                        //efe_d[i].zy = efe_d[i].zy * 0.95f - 0.002f;
                        if (efe_d[i].zx <= 0 || efe_d[i].alpha <= 0) { efe_dest(i); }
                    }

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
        
    }



}

