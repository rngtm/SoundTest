using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Synthesizer : MonoBehaviour
{
    public const int fs =  48000; // サンプリング周波数 (1秒あたりのデータ数。 標本化周波数とも呼ばれています)
    int samplePos = 0; // サンプリング位置
    double freq; // 周波数
    IAudioEffect audioEffect = new DelayEffect(); 
    SoundTable soundTable = new SoundTable(); 
    bool playSound = false;
    float interval = 0.25f;

    IEnumerator Start()
    {
        while (true)
        {
            freq = soundTable.GetRandomFreq(); // randomize freq

            playSound = true;
            samplePos = 0; // reset time
            yield return new WaitForSeconds(interval);

            playSound = false;
            yield return new WaitForSeconds(interval);
        }

    }

    void OnAudioFilterRead(float[] data, int channels)
    {        
        // audio synthesis
        int dst = 0;
        while (dst < data.Length)
        {
            double time = (double)samplePos / fs;
            double soundValue = GetSound(time);
            for (int i = 0; i < channels; i++)
            {
                data[dst++] = (float)soundValue;
            }
            samplePos++;
        }

        // audio effect
        audioEffect.InOut(data, channels);
    }

    double GetSound(double time)
    {
        if (!playSound) { return 0.0; }

        double volume = System.Math.Sin(2 * Mathf.PI * freq * time);
        double div = time * 16.0 + 1.0;
        div *= div;

        return volume / div;
    }
}