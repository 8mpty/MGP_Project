using UnityEngine;
using TMPro;


public class FpsScripts : MonoBehaviour
{
    public float timer, refresh, frame;
    public string display = "{0} FPS";
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        FPSCounter();
    }

    private void FPSCounter()
    {
        float timeLapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timeLapse;

        if(timer <= 0)
        {
            frame = (int) (1f / timeLapse);
        }
        text.text = string.Format(display, frame.ToString());
    }
}
