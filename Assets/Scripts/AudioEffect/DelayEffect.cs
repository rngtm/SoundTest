
/// <summary>
/// ディレイエフェクト
/// </summary>
public class DelayEffect : IAudioEffect
{
    const int bufferSize = Synthesizer.fs;
    const int delayTime = Synthesizer.fs / 2;
    
    float[] buffer = new float[bufferSize];
    int inputPos = delayTime;
    int outputPos = 0;
    float feedbackVolume = 0.5f;

    public void InOut(float[] data, int channels)
    {
        for (int src = 0; src < data.Length; src++)
        {
            for (int i = 0; i < channels; i++)
            {
                buffer[inputPos++] += data[src];
                buffer[outputPos] *= feedbackVolume;
                data[src] += buffer[outputPos++];

                if (inputPos == bufferSize) inputPos = 0;
                if (outputPos == bufferSize) outputPos = 0;
            }
        }
    }
}