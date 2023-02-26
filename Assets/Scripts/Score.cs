using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _daggerImage; //1
    [SerializeField] private Image _bowImage; //1
    [SerializeField] private Image _staffImage; //2
    public static int score = 0;
    public static int items = 0;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Dagger"))
        {
            var tempColor0 = _daggerImage.color;
            tempColor0.a = 1f;
             _daggerImage.color = tempColor0;
            items++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bow"))
        {
            var tempColor1 = _bowImage.color;
            tempColor1.a = 1f;
             _bowImage.color = tempColor1;
            items++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Staff"))
        {
            var tempColor2 = _staffImage.color;
            tempColor2.a = 1f;
             _staffImage.color = tempColor2;
            items++;
            Destroy(other.gameObject);
        }
    }
}
