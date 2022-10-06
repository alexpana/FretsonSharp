using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public static class ColorEx
{
    public static Color A(this Color c, float alpha)
    {
        return new Color(c.r, c.g, c.b, alpha);
    }
}

public class ChordNode_Finger : MonoBehaviour
{
    public Image Image;
    public int String = 0;
    public TMP_Text Label;
    public Image StringImage;

    public void SetFret(int fretNo)
    {
        ((RectTransform)transform).localPosition = new Vector3(fretNo * 40,
            -String * 26);
    }

    public void SetString(Chord.ChordString chordString)
    {
        Debug.Log(chordString);
        if (chordString.Symbol == null)
        {
            gameObject.SetActive(false);
        }

        gameObject.SetActive(true);

        Image.enabled = !chordString.IsMuted;
        if (chordString.IsMuted)
        {
            StringImage.color = StringImage.color.A(0.14f);
            Label.fontStyle = FontStyles.Normal;
            Label.color = new Color(1, 1, 1, 0.4f);
        }
        else
        {
            StringImage.color = StringImage.color.A(1.0f);
            Label.fontStyle = FontStyles.Bold;
            Label.color = new Color(1, 1, 1, 1.0f);
        }

        SetFret(chordString.FretNo);
        Label.text = chordString.Symbol;
        Image.color = chordString.IsRootNote ? Colors.RootNote : Colors.Note;
    }
}