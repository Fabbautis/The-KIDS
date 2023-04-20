using UnityEngine;

public class VisualScaler : MonoBehaviour
{
    public float scale;
    public float minXScale = .25f;
    public int bin;

    void Update()
    {
        var val = VisualizeSoundManager.Instance.bins[bin] * scale;
           transform.localScale = new Vector3(1,val, 1);
        //   transform.localScale = new Vector3(1, 1 / (val + minXScale), val);
        //transform.localEulerAngles = new Vector3(0, 0, val);
    }
}

