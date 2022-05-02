using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class Control : MonoBehaviour
{
    public string[] keywords = new string[] { "up", "down", "left", "right" };
    //public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public float speed = 0.1f;

    protected PhraseRecognizer recognizer;
    protected string word = "right";

    private void Start()
    {
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        Debug.Log(args.text + args.confidence);    
    }

    private void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        switch (word)
        {
            case "up":
                y += speed*Time.deltaTime;
                break;
            case "down":
                y -= speed * Time.deltaTime;
                break;
            case "left":
                x -= speed * Time.deltaTime;
                break;
            case "right":
                x += speed * Time.deltaTime;
                break;
        }

        transform.position = new Vector3(x,y,0);
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
