using UnityEngine;
using System.Collections;

public class point_sc : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        xw = my.max_world_x * 1.5f;
        yw = my.max_world_y * 1.1f;

        x_top = my.max_world_x / 2;
        y_top = my.max_world_y / 2;
        x_under = -my.max_world_x / 2;
        y_under = -my.max_world_y / 2;

        //float xs = Random.Range(0.06f, 0.5f);
        //transform.localScale = new Vector2(xs, xs);

        xa = -Random.Range(0, xw / 100) + Random.Range(0, xw / 100);
        ya = -Random.Range(0, yw / 100)-yw / 1000;

        o_xw = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        o_yw = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;


        enemy_shot_pic = gameObject.GetComponent<SpriteRenderer>();
        Color i_color = enemy_shot_pic.color;
        i_color.a = 0.9f;
        enemy_shot_pic.color = i_color;
    }
    float xa;
    float ya;
    float xw;
    float yw;
    float x_top;
    float y_top;
    float x_under;
    float y_under;
    float o_xw;
    float o_yw;
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


        if (t_c / 1 % 2 == 0) { enemy_shot_pic.sprite = nomal_cg; }
        if (t_c / 1 % 2 == 1) { enemy_shot_pic.sprite = nomal_cg2; }
       

        if (my.clear >= 1)
        {
            efe_sc.Instance.efe_on(2, transform.position.x, transform.position.y, transform.localScale * 2);
            Destroy(gameObject);
        }

        if (transform.position.x > x_top + xw / 4 || transform.position.x < x_under - xw / 4 || transform.position.y > y_top + yw / 4 || transform.position.y < y_under - yw / 4)
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
        if (other.name.IndexOf("man") >= 0)
        {
            //Debug.Log("point at man " + gameObject.name);
            if (my.world_x - my.xw / 2 <= gameObject.transform.position.x + o_xw / 2 && my.world_x + my.xw / 2 >= gameObject.transform.position.x - o_xw / 2 &&
                my.world_y - my.yw / 2 <= gameObject.transform.position.y + o_yw / 2 && my.world_y + my.yw / 2 >= gameObject.transform.position.y - o_yw / 2)
            {
                if (other.GetComponent<man_cs>())
                {
                    if (gameObject.name == "point")
                    {
                        other.GetComponent<man_cs>().point_up(1,gameObject.transform.position);
                    } else
                    {
                        other.GetComponent<man_cs>().point_up(2, gameObject.transform.position);
                    }
                }
                Destroy(gameObject);
            }
        }

    }



}
