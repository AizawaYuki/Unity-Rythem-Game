using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;

    // Start is called before the first frame update
    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "" + (int)PlayerInformation.score;
        maxComboUI.text = "" + PlayerInformation.maxCombo;
        //리소스에서 비트(beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //1,2번째 무시
        reader.ReadLine();
        reader.ReadLine();
        // 세 번째 줄에 적힌 비트 정보(s랭크,a랭크,b랭크,c랭크)
        string beatInformation = reader.ReadLine();
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);
        //성적에 맞는 랭크 이미지를 불러옵니다
        if(PlayerInformation.score >= scoreS)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank S");
        }
        else if(PlayerInformation.score >= scoreA)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank A");
        }
        else if (PlayerInformation.score >= scoreB)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank B");
        }
        else
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
}
