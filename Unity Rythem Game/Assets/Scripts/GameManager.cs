using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public float noteSpeed;

    public GameObject scoreUI;
    private float score;
    private Text scoreText;

    public GameObject comboUI;
    private int Combo;
    private Text comboText;
    private Animator comboAnimator;

   
    /*
     *Bad : 1
     *Good : 2
     *Perfect : 3 
     *Miss : 4
     */

    public enum judges { NONE = 0, BAD, GOOD, PERFECT, MISS };
    public GameObject judgeUI;
    private Sprite[] judgeSprites;
    private Image judgementSpriteRenderer;
    private Animator judgementSpriteAnimator;

    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderers;

    // 음악 관련
    private AudioSource audioSource;
    public string music = "1";

    //음악 실행하는 함수
    void MusicStart()
    {
        //리소스에서 비트음악 파일을 불러와 재생
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + music);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play(); // 재생 함수
    }
    void Start()
    {
        Invoke("MusicStart", 2);
        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        //resources 함수를 이용하면 사진의 저장 파일이 뭐인지 상관없이 사진을 가져옴

        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");


        trailSpriteRenderers = new SpriteRenderer[trails.Length];
        for (int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자가 입력한 키에 해당하는 라인에 이펙ㅌ 처리
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(1);
        if (Input.GetKey(KeyCode.J)) ShineTrail(2);
        if (Input.GetKey(KeyCode.K)) ShineTrail(3);
        // 이펙트 발생 후 라인 반복적으로 원상태로 바꿈
        for(int i =0; i<trailSpriteRenderers.Length; i++)
        {
            Color color = trailSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailSpriteRenderers[i].color = color; 
        }
    }
        //특정한 키를 눌러 해당 라인에 이펙트를 처리
      public void ShineTrail(int index)
       {
           Color color = trailSpriteRenderers[index].color;
           color.a = 0.32f;
           trailSpriteRenderers[index].color = color;
       }

    //노트 판정 이후 판정 결과를 화면에 출력
    void showJudgement()
    {
        //점수 이미지
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);
        //판정 이미지
        judgementSpriteAnimator.SetTrigger("Show");
        //콤보가 2 이상일 떄만 콤보 이미지
        if (Combo >= 2)
        {
            comboText.text = "COMBO " + Combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
    }

    //노트 판정
    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;
        //Miss 판정을 받은 경우 콤보 종료
        if(judge == judges.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            Combo = 0;
            if (score >= 15) score -= 15;
        }
        else if (judge == judges.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            Combo = 0;
            if (score >= 15) score -= 5;
        }
        else
        {
            if (judge == judges.GOOD)
            {
                judgementSpriteRenderer.sprite = judgeSprites[1];
                score += 10;
            }
            else if(judge == judges.PERFECT)
            {
                judgementSpriteRenderer.sprite = judgeSprites[3];
                score += 20;
            }
            Combo += 1;
            score += (float)Combo * 0.1f;
        }
        showJudgement();

    }
}
