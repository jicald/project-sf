using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ranking_start_sc : MonoBehaviour {
    public GameObject http_sc_d;
    public GameObject rank_text_prefab_d;
    public GameObject content_d;
    public GameObject tusin_text_d;

    // Use this for initialization
    void Start () {
        http_sc_d.GetComponent<http_sc>().http_on();	
	}
	
	// Update is called once per frame
	void Update () {
        tusin_text_d.GetComponent<Text>().text = my.tusin_mes;
        if (my.http_load_done == 1)
        {
            my.http_load_done = 2;
            for (int i = 0; i < my.rank.Count; i++)
            {
                GameObject item = (GameObject)Instantiate(rank_text_prefab_d, content_d.transform, false);
                //item.transform.FindChild("rank_text_score").gameObject.GetComponent<Text>().text = i + 1 + " > " + my.rank[i].hi_score + "点";
                foreach (Transform child in item.transform)
                {
                    Debug.Log(child.name);
                    Color i_color = new Color(1, 1, 1);
                    if (my.name == my.rank[i].name && my.pass == my.rank[i].pass) { i_color = new Color(1, 1, 0); }
                    if (child.name == "rank_text_score") { child.GetComponent<Text>().text = (i + 1 )+ " > " +my.rank[i].hi_score.ToString("N0") + "点"; child.GetComponent<Text>().color = i_color; }
                    if (child.name == "rank_text_name") { child.GetComponent<Text>().text = my.rank[i].name; child.GetComponent<Text>().color = i_color; }
                    if (child.name == "rank_text_data") { child.GetComponent<Text>().text = "Max面:" + my.rank[i].max_stage + " Play: " + my.rank[i].play_num + " (" + my.rank[i].day + ")";
                        child.GetComponent<Text>().color = i_color;
                    }
                }



            }
        }
	
	}
}
