using UnityEngine;
using System.Collections;

public class shot_cs : MonoBehaviour {

	// Use this for initialization
	void Start () {
        yw = my.max_world_y;

        y_top = my.max_world_y / 2;
        o_xw = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        o_yw = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        
    }

    float o_xw;
    float o_yw;
    float yw;
    float y_top;
    // Update is called once per frame
    void Update () {
        float y = transform.position.y + yw / 15;
        transform.position = new Vector2(transform.position.x,y );
        if (transform.position.y > y_top)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        coll_on(other);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        coll_on(other);
    }

    void coll_on(Collider2D other)
    {
        if (my.dead >= 1) { return; }

        if (other.GetComponent<enemy_sc>())
        { Vector2 enemy_size = other.GetComponent<SpriteRenderer>().bounds.size;
            float enemy_xw = enemy_size.x;
            float enemy_yw = enemy_size.y;
            float enemy_x = other.transform.position.x;
            float enemy_y = other.transform.position.y;

            if (enemy_x - enemy_xw / 2 <= gameObject.transform.position.x + o_xw / 2 && enemy_x + enemy_xw / 2 >= gameObject.transform.position.x - o_xw / 2 &&
                enemy_y - enemy_yw / 2 <= gameObject.transform.position.y + o_yw  && enemy_y + enemy_yw / 2 >= gameObject.transform.position.y - o_yw)
            {

                other.GetComponent<enemy_sc>().hp_down(1);
                Destroy(gameObject);
            }
        }
        

            
    }


}
