using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInformation  //전체 클래스에서 접근 시키기 위해
{
    public static int maxCombo { get; set;}
    public static float score { get; set; }
    public static string  selectedMusic { get; set; }
    public static string musicTitle { get; set; }
    public static string musicArtist { get; set; }
}
