using UnityEngine;

public class MinMaxSliderAttribute : PropertyAttribute
{
    public float Max;
    public float Min;

    public MinMaxSliderAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}