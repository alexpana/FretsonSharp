using System.Collections;
using UnityEngine;

public class IdGenerator
{
    public static IdGenerator Default = new(64 * 1024);

    private readonly BitArray bitMask;

    public IdGenerator(int maxValue)
    {
        bitMask = new BitArray(maxValue, true);
    }

    public int Get()
    {
        for (var i = 0; i < bitMask.Length; ++i)
            if (bitMask[i])
            {
                bitMask[i] = false;
                return i;
            }

        Debug.Log("Could not assign ID, all ids already taken");
        return -1;
    }

    public void Release(int id)
    {
        bitMask[id] = true;
    }
}