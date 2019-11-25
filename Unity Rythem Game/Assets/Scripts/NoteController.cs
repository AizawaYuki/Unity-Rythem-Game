using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class NoteController : MonoBehaviour
{
    //하나의 노트에 대한 정보를 담는 노트 클래스를 정의
    class Note
    {
        public int noteType { get; set; }
        public int order { get; set; }
        public Note(int noteType, int order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }

    public GameObject[] Notes;

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();
    private float x, z, startY = 8.0f;
    

    void MakeNote(Note note)
    {
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        //설정된 시작 라인으로 노트를 이동
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        obj.transform.position = new Vector3(x, startY, z);
        obj.GetComponent<NoteBehaviour>();
        obj.SetActive(true);
    }
    IEnumerator AwaitMakeNote(Note note)
    {
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(startingPoint + order * beatInterval);
        MakeNote(note);
    }
    
    //곡에 대한 정보를 집어넣을 변수
    private string MusicTitle;                       //제목
    private string musicArtist;                      //가수
    private int bpm;                                 //BPM
    private int divider;                             
    private float startingPoint;                     //시작시간
    private float beatCount;
    private float beatInterval;

    void Start()
    {
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>();
        //리솟에서 비트 텍스트 파일 불러오기
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + GameManager.instance.music);
        //StringReader: 특정한 텍스트 파일을 읽어올게 할 수 있도록 제공되는 라이브러리
        StringReader reader = new StringReader(textAsset.text);
        //첫 번째 줄에 적힌 곡 이름을 읽습니다.
        MusicTitle = reader.ReadLine();
        //두번째 줄에 적인 아티스트 이름을 읽어옵니다
        musicArtist = reader.ReadLine();
        //세번째 줄에 적힌 비트정보를 가져옴
        string beatInformation = reader.ReadLine();
        bpm = Convert.ToInt32(beatInformation.Split(' ')[0]); // 공백을 기준으로 나누고 거기서 첫번째 인덱스를 가져옴
        divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
        startingPoint = (float)Convert.ToDouble(beatInformation.Split(' ')[2]);
        //1초마다 떨어지는 비트 개수a
        beatCount = (float)bpm / divider;
        //비트가 떨어지는 간격 시간
        beatInterval = 1 / beatCount;  // 144/30
        //각 비트들이 떨어지는 위치 및 시간
        string line;
    
        while ((line = reader.ReadLine()) != null)
        {
            Note note = new Note(Convert.ToInt32(line.Split(' ')[0]) + 1, Convert.ToInt32(line.Split(' ')[1]));

            notes.Add(note);
        }


        //모든 노트를 정해진 시간에 출발하도록 설정
        for (int i = 0; i < notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}