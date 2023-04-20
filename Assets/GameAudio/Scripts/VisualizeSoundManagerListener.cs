using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;  // Must add this!

[RequireComponent(typeof(AudioSource))]
public class VisualizeSoundManagerListener : Singleton<VisualizeSoundManagerListener> {    
    public int binNo;    
    public int sampleNo = 512;
    public FFTWindow FFTWindow;
    public float[] binRange;
    public float[] bins;    
    public float[] samples;
    public float decay = .35f;
   
    private float minFreq;
    private AudioSource audioSource;
    public AudioMixer audioMixer;      // Must add this line!
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
      
        samples = new float[sampleNo];
       
      //  audioSource.GetOutputData(512, 0);
        CreateFrequencyBins();


        //Load AudioMixer
        audioMixer = Resources.Load<AudioMixer>("Audio/MusicMix");
      //  Debug.Log(audioMixer);
        //Find AudioMixerGroup you want to load
        AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");
     //   AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("MusicMix/Master")
        //Assign the AudioMixerGroup to AudioSource (Use first index)
         audioSource.outputAudioMixerGroup = audioMixGroup[0];

      // audioSource.Play(); // Play the audio source

    }
	
	// Update is called once per frame
	void Update () {

       // audioSource.Play(); // Play the audio source


        audioSource.GetSpectrumData(samples, 0, FFTWindow);
        BinSamples();
        //audioSource.GetSpectrumData(samples, 1, FFTWindow);
        //BinSamples();        
    }
    
    private void BinSamples()
    {
        for (int i = 0; i < binNo; i++)
        {
            bins[i] *= decay;
        }
        var bin = 0;
        for (int i = 0; i < sampleNo; i++)
        {            
            bin = BinSample(bin, i);
        }
    }

    private int BinSample(int bin, int i)
    {
        if (i * minFreq < binRange[bin])
        {
            bins[bin] += samples[i];
            return bin;
        }
        else
        {
            return BinSample(bin + 1, i);
        }
    }

    private void CreateFrequencyBins()
    {
        var maxFreq = AudioSettings.outputSampleRate / 2.0f;
        minFreq = maxFreq / sampleNo; 
        binRange = new float[binNo];
        bins = new float[binNo];
        for(int i = 0; i < binNo; i++)
        {
            binRange[i] = LogSpace(minFreq, maxFreq, i, binNo);
        }
    }

    // start - start frequency
    // stop - stop frequency
    // n - the point which you wish to compute (zero based)
    // N - the number of points over which to divide the frequency
    // range.
    float LogSpace(float start, float stop, int n, int N)
    {
        return start * Mathf.Pow(stop / start, n / (float)(N - 1));
    }
}
