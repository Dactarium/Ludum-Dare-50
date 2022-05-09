using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsLine : MonoBehaviour
{
    public static NewsLine Instance{get; private set;}

    [SerializeField] private RectTransform _newsText;

    [SerializeField] private float textSpeed;

    private Queue<string> _newsQueue = new Queue<string>();

    private bool _live = false;
    private bool _showingNews = false;
    public bool StandBy = false;
    
    private Animator _animator;
    private void Awake() {
        Instance = this;

        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if(_showingNews) return;

        if(_newsQueue.Count <= 0){
            if(!_live) return;
            
            _live = false;
            _animator.SetTrigger("Out");
            _animator.ResetTrigger("In");
            return;
        }

        if(!_live){
            _live = true;
            _animator.SetTrigger("In");
            _animator.ResetTrigger("Out");
        }

        if(!StandBy) return;

        StartCoroutine(ShowNews(_newsQueue.Dequeue()));
    }

    public string EnqueueNews{
        set{
            _newsQueue.Enqueue(value);
        }
    }

    private IEnumerator ShowNews(string news){
        _showingNews = true;
        _newsText.GetComponent<TextMeshProUGUI>().text = news;

        yield return new WaitForEndOfFrame();
        
        Vector2 position = _newsText.anchoredPosition;

        do{

            position.x -= Time.deltaTime * 100f * textSpeed;
            _newsText.anchoredPosition = position;

            yield return new WaitForEndOfFrame();

        }while(position.x > -_newsText.sizeDelta.x);
        
        _newsText.anchoredPosition = new Vector2(1920f, 0f);

        _showingNews = false;
    }

    

}
