using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class man_cs : MonoBehaviour {

    public Sprite[] man_cg = new Sprite[10];
    SpriteRenderer sp_pic;

    GameObject clear_button_d;
    GameObject dead_button_d;
    GameObject score_text_d;
    GameObject score_text_d2;
    GameObject bairitu_text_d;
    GameObject debug_text_d;
    GameObject clear_text_d;
    GameObject clear_text_d2;

    int clear_text_on_c = 0;

    int game_start;

    float timeOut = my.fps;
    float timeElapsed;
    //long enemy_shot_big_time = 0;
    int enemy_shot_big_time_int = 0;
    public int enemy_shot_big_sec;
    long clear_text_time;
    long save_time;
    public GameObject camera_obj;

    int muteki_out_sec;
    // Use this for initialization

    void Start() {
        my.stage_shokika();
        my.play++;
        my.score = 0;
        my.stage = 1;
        my.bairitu = 1.0f;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // スケールを求める。
        Vector2 scale = max * 0.4f;

        // スケールを変更。
        GameObject.Find("sky").transform.localScale = new Vector3(scale.x, scale.y * 1f, 1);
        GameObject.Find("sky").transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));

        
        man_cg = Resources.LoadAll<Sprite>("cg/");
        sp_pic = gameObject.GetComponent<SpriteRenderer>();
        bairitu_text_d = GameObject.Find("bairitu_text").gameObject;
        score_text_d = GameObject.Find("score_text").gameObject;
        score_text_d2 = GameObject.Find("score_text2").gameObject;

        debug_text_d = GameObject.Find("debug_text").gameObject;
        clear_text_d = GameObject.Find("clear_text").gameObject;
        clear_text_d2 = GameObject.Find("clear_text2").gameObject;
        clear_text_d2.SetActive(false);

        clear_button_d = GameObject.Find("clear_button").gameObject;
        dead_button_d = GameObject.Find("dead_button").gameObject;

        gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.15f, 10));

        //enemy_shot_big_time = System.DateTime.Now.Ticks;
        save_time = System.DateTime.Now.Ticks;

        
        my.max_world_x = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x * 2;
        my.max_world_y = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y * 2;
        Debug.Log("max world x " + my.max_world_x);

        my.xw = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        my.yw = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
       
        stage_shokika();
	
    }

    void stage_shokika()
    {
        Debug.Log("Stage Shokika");

        game_start = 0;
        my.clear = 0;
        my.stage_time = System.DateTime.Now.Ticks;
        my.stage_clear_time = 0;

        clear_button_d.SetActive(false);
        dead_button_d.SetActive(false);

        clear_text_on("STAGE " + my.stage + " START!", new Color(0.8f, 0.8f, 1f, 0.9f));
        clear_text_d2.SetActive(false);
        my.x = max_x() / 2; my.xn = my.x;
        my.y = max_y() * 0.1f; my.yn = my.y;
        muteki_on(2);

    }


    int t_c = 0;
    void Update() {

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut) {
            main_phase();
            timeElapsed = 0.0f;
        }
        if (Input.GetButtonUp("Fire1"))
        {

        }

        if (Input.GetMouseButtonUp(0)) { taps.mouse_on = 0; }


        if (((taps.ons() == 1) || (taps.move_on() == 1)) && (my.dead == 0))
        {
            my.xn = taps.x();
            my.yn = taps.y();



        }

        /*if (time_keika_sec(enemy_shot_big_time) >= enemy_shot_big_sec && game_start==1 && my.clear == 0 && my.dead == 0)
        {
            enemy_shot_big_time = System.DateTime.Now.Ticks;
            enemy_shot_big_on();
        }*/
        enemy_shot_big_time_int++;
        if (enemy_shot_big_time_int >= enemy_shot_big_sec*28 && game_start == 1 && my.clear == 0 && my.dead == 0)
        {
            enemy_shot_big_time_int = 0;
            enemy_shot_big_on();
        }



        int i_present = 30;
        if (time_keika_sec(my.start_time) >= i_present && my.dead == 0)
        {
            int i_num = time_keika_sec(my.start_time) / i_present;
            for (int i = 1; i <= i_num; i++)
            {
                //if (fish_sc.Instance.fish_num() < 10) { on_fish(); }
            }

            my.start_time = System.DateTime.Now.Ticks;
        }

        /*
          if (time_keika_sec(save_time) >= 60)
        {
            my.save_mode();
            save_time = System.DateTime.Now.Ticks;
        }*/



    }



    /*void OnTriggerEnter2D(Collider2D other)
    {
        coll_on(other);
    }*/
    void OnTriggerStay2D(Collider2D other)
    {
        coll_on(other);
    }

    void coll_on(Collider2D other)
    {
        if (my.dead == 0)
        {
            string s = other.name;
            if (s.IndexOf("enemy") >= 0 && s.IndexOf("_") == -1)
            {
                hp_down(1);
            }
        }
    }
    //public GameObject fish_prefab = null;




    int score_c;
    void main_phase()
    {
        //Debug.Log("anime = " + my.anime + "timeElapsed = " + timeElapsed + "Timout = " + timeOut);
        t_c++; if (t_c > 99999) { t_c = 0; }
        anime_draw();

        ///Vector2 v = camera_obj.transform.position;
        float x = 0 + my.world_x / 2;
        float y = 0 + my.world_y / 10;

        if (my.my_damage_efe >= 1) {
            x = x - UnityEngine.Random.Range(0, my.max_world_x/ 100 * my.my_damage_efe) + UnityEngine.Random.Range(0, my.max_world_x / 100 * my.my_damage_efe);
            y = y - UnityEngine.Random.Range(0, my.max_world_y / 100 * my.my_damage_efe) + UnityEngine.Random.Range(0, my.max_world_y / 100 * my.my_damage_efe);
            my.my_damage_efe--;
        }
        camera_obj.transform.position = new Vector3(x, y, -0.5f);
        float c = camera_obj.GetComponent<Camera>().orthographicSize;
        if (c > 0.19) {
            camera_obj.GetComponent<Camera>().orthographicSize = c - 0.01f; }

        if (my.score > score_c) { score_c += (int)((my.score - score_c) / 4 + 1); }
        score_text_d.GetComponent<Text>().text = (score_c).ToString("N0");
        bairitu_text_d.GetComponent<Text>().text = "倍率 x "+(my.bairitu).ToString("N1");

        string s_time = time_keika_sec(my.stage_time).ToString();
        if (my.clear == 1 || my.dead == 1) { s_time = my.stage_clear_time + ""; }

        score_text_d2.GetComponent<UnityEngine.UI.Text>().text = "STAGE " + (my.stage) + "\n敵 残り " + (my.enemy_num).ToString("N0") + "機\nTime "+s_time + "秒\nLife " + (my.hp).ToString();


        //int i_e_shots = GameObject.FindGameObjectsWithTag("enemy_shot").Length;
        //debug_text_d.GetComponent<UnityEngine.UI.Text>().text = "My World x:" + (my.max_world_x).ToString("N3") + " y:" + (my.max_world_y).ToString("N3") + "\nMX:" + (my.x).ToString("N1") + " MY:" + (my.y).ToString("N1") + "\nX:" + (taps.x()).ToString("N1") + " Y:" + (taps.y()).ToString("N1") + "\nXN:" + (my.xn).ToString("N1") + " YN:" + (my.yn).ToString("N1") + "\nenemy shots :" + (i_e_shots);

        if (my.enemy_num <= 0 && game_start==1 && my.clear == 0)
        {
            int i = GameObject.FindGameObjectsWithTag("enemy").Length;
            if (i >= 1)
            {
                my.enemy_num = i; return;
            } else { Debug.Log(i); my.enemy_num = 0; }

            muteki_on(2);
            my.clear = 1;
            my.stage_clear_time = time_keika_sec(my.stage_time);

            clear_text_on("STAGE CLEAR!!", new Color(1, 0.9f, 0, 0.9f));
            audio_manager.Instance.playSE("on");

            int i_score = (int)((40 - my.stage_clear_time) * 100 * my.bairitu);
            if (i_score >= 1)
            {
                score_get((40 - my.stage_clear_time) * 100, new Vector2(0, 0),2);
                clear_text_on2("Time Bonus +" + i_score.ToString("N0"), new Color(1, 0.7f, 0.9f));

            }

            clear_button_d.SetActive(true);
        }

        if (clear_text_on_c == 1)
        {
            if (t_c % 8 < 4)
            {
                clear_text_d.SetActive(true);
            } else
            {
                clear_text_d.SetActive(false);
            }

            if ((time_keika_sec(clear_text_time) >= 2) && (game_start == 0)) { game_start = 1; enemy_start(); }

            if (time_keika_sec(clear_text_time) >= 3 && my.clear == 0)
            {
                clear_text_on_c = 0; clear_text_d.SetActive(false);
                
            }
        }
    }

   

    void clear_text_on(string s, Color i_color)
    {
        clear_text_d.GetComponent<Text>().text = s;
        clear_text_d.GetComponent<Text>().color = i_color;
        clear_text_on_c = 1;
        clear_text_time = System.DateTime.Now.Ticks;
    }
    void clear_text_on2(string s, Color i_color)
    {
        clear_text_d2.GetComponent<Text>().text = s;
        clear_text_d2.GetComponent<Text>().color = i_color;
        clear_text_d2.SetActive(true);
    }

    void anime_draw() {

        int a = 0;
        my.anime += 1;
        if (my.xn - my.x < 0) { my.muki = 0; } else { my.muki = 1; }

        if (my.muki == 0) { gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1); } else { gameObject.transform.localScale = new Vector3(-0.1f, 0.1f, 1); }

        //通常時
        if (my.dead == 0)
        {
            if (my.muteki == 0)
            {
                if (my.anime >= 4) { my.anime = 0; }
                if (my.anime / 2 == 0) { a = anime_num("player"); } else if (my.anime / 2 == 1) { a = anime_num("player2"); }
                //Debug.Log("anime = " + my.anime + " , a = " + a);
                Color i_color = sp_pic.color; i_color.a = 1f;
                sp_pic.color = i_color;
                
            }
            else
            { //無敵時
                if (my.anime >= 2) { my.anime = 0; }
                if (my.anime / 1 == 0) { a = anime_num("muteki"); } else if (my.anime / 1 == 1) { a = anime_num("muteki2"); }
                if (t_c % 3 == 0)
                {
                    Color i_color = sp_pic.color; i_color.a = 0.5f;
                    sp_pic.color = i_color;
                } else
                {
                    Color i_color = sp_pic.color; i_color.a = 1f;
                    sp_pic.color = i_color;
                }


                if (time_keika_sec(my.muteki_time) >= muteki_out_sec && my.clear == 0)
                {
                    my.muteki = 0;
                }
            }
        } else
        {
            Color i_color = sp_pic.color; i_color.a = 0f;
            sp_pic.color = i_color;
        }

        sp_pic.sprite = man_cg[a];

        if (Mathf.Abs(my.x-my.xn) > 0.001)
        {
            float ix = (my.x - my.xn) / 2;
            my.swim_kyori += Mathf.Abs(ix) / 10;
            my.x -= ix;
            
        }
        if (Mathf.Abs(my.y - my.yn) > 0.001)
        {
            float iy = (my.y - my.yn) / 2;
            my.swim_kyori += Mathf.Abs(iy) / 10;
            my.y -= iy; 
        }
                

        if (my.x > max_x()*0.95f) { my.x = max_x()*0.95f; }
        if (my.x < max_x()*0.05f) { my.x = max_x()*0.05f; }
        if (my.y > max_y()*0.9f) { my.y = max_y()*0.9f; }
        if (my.y < max_y()*0.05f) { my.y = max_y()*0.05f; }

        //if (my.xn > max_x()) { my.xn = max_x(); }
        //if (my.xn < 0) { my.xn = 0; }
        //if (my.yn > max_y()) { my.yn = max_y(); }
        //if (my.yn < 50) { my.yn = 50; }

        Vector2 v = Camera.main.ScreenToWorldPoint(new Vector2(my.x, my.y));
        my.world_x = v.x;
        my.world_y = v.y;


        gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(my.x, my.y,1));
        

        shoot_on();

    }
    int anime_num(string s)
    {
        int j = -1;
        for (int i = 0; i < man_cg.Length; i++)
        {
            if (man_cg[i].name == s)
            {
                j = i;
            }
        }
        return j;
    }

    public GameObject shot_prefab;
    void shoot_on()
    {
        if (my.dead >= 1) { return; }
        audio_manager.Instance.playSE("tama2");
        Instantiate(shot_prefab, new Vector2(transform.position.x, transform.position.y + sp_pic.bounds.size.y ), Quaternion.identity);
    }


    void enemy_start()
    {
        my.enemy_num = my.stage*4+25;
		if (my.enemy_num >= 50) {
			my.enemy_num = 50;
		}
        for (int i = 0; i < my.enemy_num; i++)
        {
            enemy_on();

        }
    }

    public GameObject enemy_prefab;
    public void enemy_on()
    {
        float x = -UnityEngine.Random.Range(0, my.max_world_x) + UnityEngine.Random.Range(0, my.max_world_x);
        float y = (my.max_world_y / 2 * 0.8f) - UnityEngine.Random.Range(0, my.max_world_y * 0.2f);

        GameObject i = (GameObject)Instantiate(enemy_prefab, new Vector2(x,y), Quaternion.identity);
        if (my.stage >= 4 && UnityEngine.Random.Range(0, 15) == 0) { i.GetComponent<enemy_sc>().kind = 1; }
        if (my.stage >= 8 && UnityEngine.Random.Range(0, 25) == 0) { i.GetComponent<enemy_sc>().kind = 2; }


    }

    public GameObject enemy_shot_big_prefab;
    void enemy_shot_big_on()
    {
        float x = -UnityEngine.Random.Range(0, my.max_world_x) + UnityEngine.Random.Range(0, my.max_world_x);
        float y = my.max_world_y * 0.6f;

        Instantiate(enemy_shot_big_prefab, new Vector2(x, y), Quaternion.identity);
    }


    public GameObject point_prefab;
    public GameObject point_x_prefab;
    public void point_on(Vector2 pos)
    {
       if (UnityEngine.Random.Range(0, 5) == 1)
       {
            if (UnityEngine.Random.Range(0, 5) == 1)
            {
                GameObject a = (GameObject)Instantiate(point_x_prefab, pos, Quaternion.identity);
                a.name = "point_x";
                
            }
            else
            {
                GameObject a = (GameObject)Instantiate(point_prefab, pos, Quaternion.identity);
                a.name = "point";
            }
        }

        
    }
    public void point_up(int ic,Vector2 pos)
    {
        if (ic==1)
        {
            score_get(100,pos,1);
            audio_manager.Instance.playSE("get2");
        }
        if (ic == 2)
        {
            my.bairitu += 0.1f;
            efe_sc.Instance.efe_text_on(0, pos.x,pos.y, 20, "倍率UP!",new Color(0.6f,0.6f,1f));
            audio_manager.Instance.playSE("GET");
        }
        muteki_on(1);

    }

    public void score_get(int i_score,Vector2 pos,int i_kind)
    {
        i_score = (int)(i_score * my.bairitu);
        int i_size = 15 + (i_score / 100);
        my.score += i_score;
        if (i_kind == 0)
        {
            efe_sc.Instance.efe_text_on(0, pos.x, pos.y, i_size, i_score.ToString(),new Color(1,1,1));
        } else if (i_kind == 1)
        {
            efe_sc.Instance.efe_text_on(0, pos.x, pos.y, i_size, i_score.ToString(), new Color(1, 1, 0));
        } else
        {
            efe_sc.Instance.efe_text_on(0, pos.x, pos.y, i_size, i_score.ToString(), new Color(0.5f, 1, 0));
        }

    }
    public void hp_down(int ic)
    {
        if (my.muteki >= 1) { return; }
        my.my_damage_efe = 10;
        my.hp--;
        for (int i = 0; i <= 16; i++) { efe_sc.Instance.efe_on(1, gameObject.transform.position.x, gameObject.transform.position.y, new Vector2(0.1f, 0.1f)); }

        if (my.hp <= 0)
        {
            my.dead = 1; my.my_damage_efe = 15;
            my.stage_clear_time = time_keika_sec(my.stage_time);
            for (int i = 0; i <= 16; i++) { efe_sc.Instance.efe_on(1, gameObject.transform.position.x, gameObject.transform.position.y, new Vector2(0.25f, 0.25f)); }
            clear_text_on("GAME OVER", new Color(1, 0, 0, 0.9f));
            audio_manager.Instance.playSE("bom2");
            audio_manager.Instance.playSE("end");

            if (my.hi_score < my.score)
            {
                my.hi_score = my.score;
                clear_text_on2("New Record!!", new Color(1, 0, 1, 0.9f));
            }

            my.save_mode();
            dead_button_d.SetActive(true);

        }
        else
        {
            muteki_on(2);
            audio_manager.Instance.playSE("baku");
        }
    }

    void muteki_on(int i_sec)
    {
        my.muteki_time = System.DateTime.Now.Ticks;
        my.muteki = 1;
        muteki_out_sec = i_sec;
    }

    public void button_on()
    {
        audio_manager.Instance.playSE("pin");
        
    }
    public void button_push(string s)
    {
        audio_manager.Instance.playSE("pin");
        if (s == "next_stage")
        {
            
            my.stage++;
            if (my.max_stage < my.stage)
            {
                my.max_stage = my.stage;
            }
            stage_shokika();

        }
    }

    public int time_keika_sec(long start_time)
    {
        long now_time = System.DateTime.Now.Ticks;

        return (int)((now_time - start_time) / (1000 * 1000 * 10));
    }



    public static float max_x()
    {
        return Screen.width;
    }
    public static float max_y()
    {
        return Screen.height;
    }
    

    tap_d taps = new tap_d();
    public class tap_d
    {
        public int mouse_on = 0;

        public float x()
        {
            float x = 0f;
            if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.IPhonePlayer))
            {
                // Android or iOS
                if (Input.touchCount > 0)
                {
                    Touch tap0 = Input.GetTouch(0);
                    x = tap0.position.x;
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // Windows editer unity
                x = Input.mousePosition.x;
            }
            //if (x == 0) { x = Screen.width / 2; }
            return x;
        }
        public float y()
        {
            float y = 0f;
            if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.IPhonePlayer))
            {
                // Android or iOS
                if (Input.touchCount > 0)
                {
                    Touch tap0 = Input.GetTouch(0);
                    y = tap0.position.y;
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // Windows editer unity
                y = Input.mousePosition.y;
            }
            //if (y == 0) { y = Screen.height * 0.3f; }
            return y + max_y() / 10; 
        }
        public int ons()
        {
            int c = 0;
            //タッチがあるかどうか？

            if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.IPhonePlayer))
            {
                // Android or iOS
                if (Input.touchCount > 0)
                {
                    Touch tap0 = Input.GetTouch(0);
                    if (tap0.phase == TouchPhase.Began) { c = 1; }

                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // Windows editer unity
                if (Input.GetMouseButtonDown(0)) { c = 1; mouse_on = 1; }
            }

            return c;
        }
        public int move_on()
        {
            int c = 0;
            if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.IPhonePlayer))
            {
                // Android or iOS
                if (Input.touchCount > 0)
                {
                    Touch tap0 = Input.GetTouch(0);
                    if (tap0.phase == TouchPhase.Moved) { c = 1; }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // Windows editer unity
                if (mouse_on == 1) { c = 1; }
            }
            return c;
        }

    }

}

/*
 * 
 *  ix:=jx+trunc(cos(k*3.1415/180)*l);iy:=jy+trunc(sin(k*3.1415/180)*l);
 *  
//コンポーネントをオブジェクトに追加　コンポーネントがあるかないか
/*
             * if (button_tirasi_d.GetComponent<admob_inter>())
            {
                Destroy(button_tirasi_d.GetComponent<admob_inter>());
            }

            button_tirasi_d.AddComponent<admob_inter>();
            button_tirasi_d.GetComponent<admob_inter>().Android_Interstitial = "ca-app-pub-6451179755762834/6357202417";
         


//ボタンが表示中か非表示か
     if (time_keika_sec(my.tirasi_time) >= tirasi_on_sec && button_tirasi_d.activeSelf == false)
        {

//ボタンのON OFF
            button_tirasi_d.GetComponent<Button>().interactable = false;
            button_tirasi_d.SetActive(true);

        }

        if (time_keika_sec(my.tirasi_time) >= tirasi_on_sec && button_tirasi_d.activeSelf == true)
        {
            if (my.tirasi_ok == 1)
            {
                my.tirasi_time = System.DateTime.Now.Ticks;
                button_tirasi_d.GetComponent<Button>().interactable = true;
            }
          
        }

//現在時刻より減算

    if (my.tirasi_time == 0)
        {5 秒減算
            my.tirasi_time = (System.DateTime.Now + new System.TimeSpan(0, 0, 0, -5)).Ticks;
        }
        


//他オブジェクトのスクリプトを実行

    if (s == "tirasi")
        {
            if (button_tirasi_d.GetComponent<admob_inter>())
            {
                button_tirasi_d.GetComponent<admob_inter>().on_show();
            }
            my.tirasi_time = System.DateTime.Now.Ticks;
            GameObject.Find("button_tirasi").SetActive(false);
         
*/