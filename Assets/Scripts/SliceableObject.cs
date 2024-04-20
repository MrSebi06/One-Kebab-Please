using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    public int lives;

    private bool _isSliced;
    
    private string _tag;
    void Start()
    {
        _isSliced = false;
        _tag = gameObject.tag;
    }
    void Update()
    {
        if (_isSliced) return; 
        CheckLives();
    }

    private void CheckLives()
    {
        if (lives == 0) _isSliced = true;
    }
}
