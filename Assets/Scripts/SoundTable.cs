using UnityEngine;

public class SoundTable
{
    private float baseFreq = 440f;
    private float[] freq;
    int[] indexTable = new int[]
    {
        0, 2, 4, 7, 9, 11
    };

    public SoundTable()
    {
        freq = new float[12];
        for (int i = 0; i < freq.Length; i++)
        {
            freq[i] = baseFreq * Mathf.Pow(2f, i / 12f);
        }
    }

    public float GetRandomFreq()
    {
        int index = indexTable[Random.Range(0, indexTable.Length)];
        return freq[index];
    }
}