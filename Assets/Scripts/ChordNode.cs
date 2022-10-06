using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChordNode : MonoBehaviour
{
    public List<ChordNode_Finger> Fingers;
    public List<Transform> Frets;

    public TMP_Text Title;
    public TMP_Text FirstFret;

    public Image Barre;
    public Image Nut;

    public String NoteName;
    public String ChordName;

    // Start is called before the first frame update
    void Start()
    {
        UpdateChord();
    }

    [EditorCools.Button]
    private void UpdateChord()
    {
        Enum.TryParse(NoteName.Replace("#", "Sharp"), out Note n);
        SetChord(n, ChordName);
    }
    
    private void SetChord(Note note, string chordName)
    {
        Chord chord = Chord.For(note, chordName);
        Title.text = $"{note}{chordName}".Replace("Sharp", "#");
        FirstFret.text = chord.FirstFret.ToString();

        int min1 = -1;
        int max1 = -1;
        for (int i = 0; i < 6; ++i)
        {
            if (chord.Strings[i][0].Symbol == "1")
            {
                if (min1 == -1)
                {
                    min1 = i;
                }

                max1 = i;
            }
        }

        var fretCount = 0;
        for (int i = 0; i < 6; ++i)
        {
            fretCount = Math.Max(fretCount, chord.Strings[i][0].FretNo);
        }

        for (int i = 0; i < 6; ++i)
        {
            Fingers[i].gameObject.SetActive(true);
            Fingers[i].StringImage.rectTransform.sizeDelta = new Vector2(
                10 + fretCount * Layout.FretWidth,
                Fingers[i].StringImage.rectTransform.sizeDelta.y
            );
            if (chord.Strings[i][0].Symbol == "1" && i > min1 && i < max1)
            {
                Fingers[i].gameObject.SetActive(false);
            }
            else
            {
                Fingers[i].SetString(chord.Strings[i][0]);
            }
        }

        for (var i = 0; i < Frets.Count; ++i)
        {
            Frets[i].gameObject.SetActive(i <= fretCount);
        }

        if (min1 != max1)
        {
            Barre.gameObject.SetActive(true);
            Barre.rectTransform.position = Fingers[min1].transform.position + Vector3.down * 11.0f;
            Barre.rectTransform.sizeDelta = new Vector2(22, 26 * (max1 - min1));
            Barre.color = chord.Strings[min1][0].IsRootNote ? Colors.RootBarre : Colors.Barre;
        }
        else
        {
            Barre.gameObject.SetActive(false);
        }

        Nut.gameObject.SetActive(chord.FirstFret == 1);

        ((RectTransform)transform).sizeDelta = new Vector2(
            80 + fretCount * Layout.FretWidth,
            200
        );
    }
}