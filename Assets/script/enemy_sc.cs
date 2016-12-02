using UnityEngine;
using System.Collections;

public class enemy_sc: MonoBehaviour
{
    public int kind = 0;
    float zx;
    float zy;
    float zxn;
    float zyn;
    // Use this for initialization
    void Start()
    {
        max_world_x = my.max_world_x;
        max_world_y = my.max_world_y;

        x_top = my.max_world_x / 2;
        y_top = my.max_world_y / 2;
        x_under = -my.max_world_x / 2;
        y_under = -my.max_world_y / 2;

        zxn = Random.Range(0.05f, 0.2f);
        zyn = Random.Range(0.05f, 0.2f);
        zx = 0.01f; zy = 0.01f;       
        transform.localScale = new Vector2(zx,zy) ;
        hp = (int)(zxn * 15 + zyn * 15) + (int)(my.stage / 2);

        enemy_pic = gameObject.GetComponent<SpriteRenderer>();
        xw = enemy_pic.bounds.size.x;
        yw = enemy_pic.bounds.size.y;

		enemy_shot_sec = 5 + (int)(my.stage / 3); if (enemy_shot_sec>=8){enemy_shot_sec = 8;}
        //enemy_shot_sec = 1;
        if (kind == 1) { enemy_shot_sec = 1; }
        if (kind == 2) { enemy_shot_sec = 3; }
        enemy_shot_time = (System.DateTime.Now + new System.TimeSpan(0, 0, 0, -Random.Range(0,enemy_shot_sec))).Ticks;
        enemy_shot_time_int = Random.Range(0,enemy_shot_sec * 28);
        man_d = GameObject.Find("man");
    }

    public int enemy_shot_sec;
    long enemy_shot_time = 0;
    int enemy_shot_time_int = 0;

    GameObject man_d;

    int hp;
    float xw;
    float yw;
    float max_world_x;
    float max_world_y;
    float x_top;
    float y_top;
    float x_under;
    float y_under;
    float timeOut = my.fps;
    float timeElapsed;
    

    public GameObject enemy_shot_prefab;
    public Sprite nomal_cg;
    public Sprite nomal_cg2;
    public Sprite damage_cg;
    SpriteRenderer enemy_pic;
    int t_c;
    int damage_efe;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            main_phase();
            timeElapsed = 0.0f;
        }
    }

    void main_phase() { 
        t_c++; if (t_c > 100000) { t_c = 0; }
        float xa = -Random.Range(0,max_world_x / 100) + Random.Range(0, max_world_x / 100);
        float ya = -Random.Range(0, max_world_y / 200) + Random.Range(0, max_world_y / 200);

        float x = transform.position.x;
        float y = transform.position.y;

        float i_baif = 1.7f;
        if ((transform.position.x + xa < x_top*i_baif && transform.position.x + xa > x_under*i_baif) && (transform.position.y + ya < y_top && transform.position.y + ya > y_under)) {
            x += xa;
            y += ya;
        }
        if (transform.localScale.x < zxn) { zx += 0.01f; }
        if (transform.localScale.y < zyn) { zy += 0.01f; }
        transform.localScale = new Vector2(zx, zy);
        
        if (x + xw / 2 > x_top * i_baif) { x = x_top * i_baif - xw / 2; }
        if (x - xw / 2 < x_under * i_baif) { x = x_under * i_baif + xw / 2; }
        if (y + yw / 2 > y_top * i_baif) { y = y_top * i_baif - yw / 2; }
        if (y - yw / 2 < y_under * i_baif) { y = y_under * i_baif + yw / 2; }
       
        transform.position = new Vector2(x, y);
        
        var i_damage = 0;
        if (damage_efe>=1) { damage_efe--;
            if (t_c % 2 == 0) { i_damage = 1; }
        }
        if (i_damage == 0)
        {
            if (t_c / 4 % 2 == 0) { enemy_pic.sprite = nomal_cg; } else { enemy_pic.sprite = nomal_cg2; }
        }
        else { enemy_pic.sprite = damage_cg; }

        //if (time_keika_sec(enemy_shot_time)>=enemy_shot_sec && my.dead == 0) { shoot_on(); enemy_shot_time = System.DateTime.Now.Ticks; }
        enemy_shot_time_int++; 
        if (enemy_shot_time_int >= enemy_shot_sec*28 && my.dead == 0) { shoot_on(); enemy_shot_time_int = 0; }
    }

    public int time_keika_sec(long start_time)
    {
        long now_time = System.DateTime.Now.Ticks;

        return (int)((now_time - start_time) / (1000 * 1000 * 10));
    }

    void shoot_on()
    {
        if (GameObject.FindGameObjectsWithTag("enemy_shot").Length >= 85) { return; }


        if (kind == 2)
        {
            for (int j = 0; j < 8; j++)
            {
                float i_kaku = j * 40;
                float i_l = my.max_world_x;
                GameObject io = (GameObject)Instantiate(enemy_shot_prefab, transform.position, Quaternion.identity);
                io.GetComponent<enemy_shot_sc>().xn = transform.position.x + Mathf.Cos(i_kaku * (Mathf.PI / 180)) * i_l;
                io.GetComponent<enemy_shot_sc>().yn = transform.position.y + Mathf.Sin(i_kaku * (Mathf.PI / 180)) * i_l;
                io.GetComponent<enemy_shot_sc>().kind = 2;

            }
            return;
        }

        GameObject i = (GameObject)Instantiate(enemy_shot_prefab, transform.position, Quaternion.identity);
        if (kind == 1) { i.GetComponent<enemy_shot_sc>().kind = 1; }

    }

    public void hp_down(int ia)
    {
        if (my.dead >=1 || my.clear >= 1) { return; }
        damage_efe = 2;
        hp--;

        if (hp <= 0)
        {
            my.my_damage_efe = 1; 
            man_d.GetComponent<man_cs>().point_on(gameObject.transform.position);
            man_d.GetComponent<man_cs>().score_get(50,new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + yw / 3),0);


            for (int i = 0; i <= 9; i++) { efe_sc.Instance.efe_on(0, gameObject.transform.position.x, gameObject.transform.position.y, new Vector2(0.05f,0.05f), gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.9f, gameObject.GetComponent<SpriteRenderer>().bounds.size.y); }
            my.enemy_num--;
            audio_manager.Instance.playSE("bom");
            Destroy(gameObject);
            
        } else
        {
            audio_manager.Instance.playSE("enemy_cut");

        }
    }


}
