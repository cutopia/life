using UnityEngine;

public class CellHandler : MonoBehaviour
{
    public bool newState = false;
    private bool _state = false;

    public bool State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            if (_state)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }

    public void OnMouseDown()
    {
        State = !State;
    }

    public void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            State = !State;
        }
    }

    private void Start()
    {
        State = false;
    }
}
