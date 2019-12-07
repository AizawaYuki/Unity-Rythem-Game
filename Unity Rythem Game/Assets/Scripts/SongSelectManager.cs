using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SongSelectManager : MonoBehaviour
{
    public Image musicImageUI;
    public Text musicTitleUI;
    public Text bpmUI;

    private int musicIndex;
    private int musicCount = 4;

    private void UpdateSong(int musicIndex)  // 해당 곡에 대해 이미지를 보여주기 위함
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        
        //리소스에서 비트(beat) 텍스트 파일을 읽어올 수 있도록 함
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader stringReader = new StringReader(textAsset.text);  // 텍스트의 적혀있는 정보 가져오기
        //첫번쨰 줄에 적힌 곡 이름을 읽어서 UI에 업뎃
        musicTitleUI.text = stringReader.ReadLine();
        //두번째 줄 읽고 처리 x (텍스트 파일 두번째줄은 아티스트 이름이기떄문)
        stringReader.ReadLine();
        //세번째 줄에 적힌 bpm을 UI에 업데이트
        bpmUI.text = "BPM : " + stringReader.ReadLine().Split(' ')[0];
        // 리소스에서 비트 음악 파일을 불러와 재생
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();
        //리소스에서 비트(beat) 이미지 파일을 불러옴.
        musicImageUI.sprite = Resources.Load<Sprite>("Beats/" + musicIndex.ToString());

    }

    public void Right()
    {
        musicIndex = musicIndex + 1;
        if(musicIndex > musicCount)
        {
            musicIndex = 1;
        }
        UpdateSong(musicIndex);
    }

    public void Left()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < musicCount)
        {
            musicIndex = musicCount;
        }
        UpdateSong(musicIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        musicIndex = 1;
        UpdateSong(musicIndex);

    }

   public void GameStart()
    {
        PlayerInformation.selectedMusic = musicIndex.ToString();
        SceneManager.LoadScene("GameScene");
    }
}
