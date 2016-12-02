using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class title_cs : MonoBehaviour {

    //public GameObject prefab = null;
    long op_time;

    GameObject hi_score_text_d;
    GameObject op_panel_d;
    GameObject entry_panel_d;
    public InputField rank_input_d;
    GameObject kakunin_panel_d;
    public GameObject kakunin_text_d;

    // Use this for initialization
    void Start () {
        Debug.Log("hello");
        if (my.load_done == 0) { my.load_mode(); my.load_done = 1; }


        op_panel_d = GameObject.Find("panel_op");
        op_panel_d.SetActive(true);

        hi_score_text_d = GameObject.Find("hi_score_text");
        hi_score_text_d.GetComponent<Text>().text = "HiScore " + my.hi_score.ToString("N0") + "点";

        entry_panel_d = GameObject.Find("panel_entry");
        entry_panel_d.SetActive(false);
        entry_panel_d.transform.position = op_panel_d.transform.position;
        
        kakunin_panel_d = GameObject.Find("panel_kakunin");
        kakunin_panel_d.SetActive(false);
        
        kakunin_panel_d.transform.position = op_panel_d.transform.position;


        audio_manager.Instance.play_bgm("1",0.3f);
        op_time = (System.DateTime.Now + new System.TimeSpan(0, 0, 0, -2)).Ticks;
        
    }

    int op_c = 0;

    // Update is called once per frame
    void Update () {

        if (time_keika_sec(op_time) >= 1.25 && op_c <= 9)
        {

            /*
             * int i = op_c;
            // プレハブからインスタンスを生成
            GameObject obj = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
            // 作成したオブジェクトを子として登録
            obj.transform.SetParent(transform);

            obj.transform.localScale = new Vector2(1, 1);
            obj.transform.position = new Vector3(0, 0.1f - 0.02f * i, 0);
            string[] s = new string[] {
                "海で潜るとキラキラ光る砂が見えた",
                "沖の水はちょっぴり冷たくて怖かった",
                "ゴムボートが足の届かない所まで流されて",
                "追いつけなかった",
                "ヘトヘトになって波間から見上げた空に",
                "大きな雲が浮いていた",
                "作った砂山にトンネルを掘った",
                "波が全てをさらっていった……",
                " ",
                "海の思い出はたくさん。"};

            obj.GetComponent<Text>().text = s[op_c];

            op_time = System.DateTime.Now.Ticks;
            op_c++;
            */


        }
	}

    public void panel_change(string to_panel)
    {
        audio_manager.Instance.playSE("pin");

        if (to_panel == "ranking")
        {            
            op_panel_d.SetActive(false);

            if (my.name == "" || my.name == null)
            {
                entry_panel_d.SetActive(true);
                return;                
            } else
            {
                SceneManager.LoadScene("ranking");
            }
        }

        if (to_panel == "kakunin")
        {
            string s = rank_input_d.text;
            if (s == "" || s == null)
            {
                return;
            }
        
            my.name = s;
            my.pass = Random.Range(0, 100000000);
            entry_panel_d.SetActive(false);
            kakunin_panel_d.SetActive(true);
            kakunin_text_d.GetComponent<Text>().text = "Player Name> " + my.name;
            return;

        }
        if (to_panel == "kakunin_cancel")
        {
            my.name = "";
            kakunin_panel_d.SetActive(false);
            entry_panel_d.SetActive(true);
        }
        if (to_panel == "kakunin_ok")
        {
            kakunin_panel_d.SetActive(false);
            SceneManager.LoadScene("ranking");
        }

        if (to_panel == "op")
        {
            entry_panel_d.SetActive(false);
            op_panel_d.SetActive(true);


        }
    }

    public float time_keika_sec(long start_time)
    {
        long now_time = System.DateTime.Now.Ticks;

        return (float)((now_time - start_time) / (1000 * 1000 * 10));
    }
}
