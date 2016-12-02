using UnityEngine;
using System.Collections;

public class enemy_shot_sc : MonoBehaviour
{
    public int kind = 0;
    public float xn = 0;
    public float yn = 0;

    // Use this for initialization
    void Start()
    {
        xw = my.max_world_x;
        yw = my.max_world_y;

        x_top = my.max_world_x / 2;
        y_top = my.max_world_y / 2;
        x_under = -my.max_world_x / 2;
        y_under = -my.max_world_y / 2;

        float xs = Random.Range(0.03f, 0.03f) + (float)(my.stage-1) / 200;
        float ys = Random.Range(0.03f, 0.03f) + (float)(my.stage-1) / 200;
        
        
        float sp = 100 - my.stage * 2; if (sp<10) { sp = 10; }
        xa = -Random.Range(0, xw / sp) + Random.Range(0, xw / sp);
        ya = -Random.Range(0, yw / sp) - yw / 1000;
        if (Random.Range(0,10) == 0)
        {
            xa = xa * 1.5f;
            ya = ya * 1.5f;
        }
        enemy_shot_pic = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(kind);
        if (kind == 1)
        {
            xa = (my.world_x - transform.position.x) / 30;
            ya = (my.world_y - transform.position.y) / 30;
            xs = 0.03f;
            ys = 0.03f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.8f);
          
        }
        if (kind == 2)
        {
            xa = (xn - transform.position.x) / 50;
            ya = (yn - transform.position.y) / 50;
            xs = 0.03f;
            ys = 0.03f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0, 0.9f);
        }

        transform.localScale = new Vector2(xs, ys);
        o_xw = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        o_yw = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;


    }
    float o_xw;
    float o_yw;
    float xa;
    float ya;
    float xw;
    float yw;
    float x_top;
    float y_top;
    float x_under;
    float y_under;
    float timeOut = my.fps;
    float timeElapsed;

    public Sprite nomal_cg;
    public Sprite nomal_cg2;
    SpriteRenderer enemy_shot_pic;
    int t_c;
    
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            main_phase();
            timeElapsed = 0.0f;
        }
    }

    void main_phase()
    {
        t_c++; if (t_c > 100000) { t_c = 0; }
        
        float x = transform.position.x;
        float y = transform.position.y;

        x += xa;
        y += ya;

        transform.position = new Vector2(x, y);

      
        if (t_c / 1 % 2 == 0) { enemy_shot_pic.sprite = nomal_cg; } else { enemy_shot_pic.sprite = nomal_cg2; }

        if (my.clear >= 1)
        {
            efe_sc.Instance.efe_on(2, transform.position.x, transform.position.y, transform.localScale*4);
            Destroy(gameObject);
        }

        if (transform.position.x > x_top + xw / 2 || transform.position.x < x_under - xw / 2 || transform.position.y > y_top + yw / 5 || transform.position.y < y_under - yw / 5)
        {
            Destroy(gameObject);
        }

        

    }

 
    void OnTriggerStay2D(Collider2D other)
    {
        coll_on(other);
    }

    void coll_on(Collider2D other)
    {
        if (my.dead >= 1 || my.clear >= 1) { return; }

        if (my.world_x - my.xw / 2 <= gameObject.transform.position.x + o_xw / 2 && my.world_x + my.xw / 2 >= gameObject.transform.position.x - o_xw / 2 &&
            my.world_y - my.yw / 2 <= gameObject.transform.position.y + o_yw / 2 && my.world_y + my.yw / 2 >= gameObject.transform.position.y - o_yw / 2)
        {
            if (other.GetComponent<man_cs>())
            {
                other.GetComponent<man_cs>().hp_down(1);
            }
            if (other.name.IndexOf("man") >= 0) { Destroy(gameObject); }
        }
    }



}
