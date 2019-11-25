using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    public int noteType;
    private GameManager.judges judge;
    private KeyCode keycode;
    // Start is called before the first frame update
    void Start()
    {
        if (noteType == 1)
            keycode = KeyCode.D;
        else if (noteType == 2)
            keycode = KeyCode.F;
        else if (noteType == 3)
            keycode = KeyCode.J;
        else if (noteType == 4)
            keycode = KeyCode.K;
    }
    public void Initialize()
    {
        judge = GameManager.judges.NONE;      //판정 상태 초기값
    }
    // Update is called once per frame
    void Update()
    {
        /* transform.Translate(Vector3.down * Speed;) */
        transform.Translate(Vector3.down * GameManager.instance.noteSpeed);
        // 사용자가 노트 키를 입력한 경우
        if (Input.GetKey(keycode))
        {
            GameManager.instance.processJudge(judge, noteType);
            //노트가 판정 선에 닿기 시작한 이후로는 해당 노트를 제거합니다
            if (judge != GameManager.judges.NONE) gameObject.SetActive(false);
        }
    }
    //각 노트의 현재 위치에 대하여 판정을 수행합니다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judges.BAD;
        }
        else if (other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judges.PERFECT;
        }
        else if (other.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judges.MISS;
            GameManager.instance.processJudge(judge, noteType);
            gameObject.SetActive(false);
        }
    }
}

