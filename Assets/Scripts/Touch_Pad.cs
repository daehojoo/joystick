using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. 자기자신(터치패드)의 position 값을 스타트 포지션으로 정한다
//2. 조이스틱 바깥원의 반지름을 지정한다.
//3. 패드 버튼을 눌렀는 지 파악 불변수 지정 
//4. 모바일 터치시 원안에서 터치 되는지 안되는 지를 정수값으로 판단
public class Touch_Pad : MonoBehaviour
{
    [SerializeField][Tooltip("터치패드포지션")] private RectTransform touchPad;
    [SerializeField] private Vector3 _StartPos;
    [SerializeField][Tooltip("원의 반지름")] private float _dragRadius = 100f;
    [SerializeField]
    [Tooltip("플레이어 클래스 연결")] private Player player;

    bool IsBtnPressed = false;
    private int _touchId = -1;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        touchPad = GetComponent<RectTransform>();
        _StartPos = touchPad.position;
    }
    public void ButtonDown()
    {
        IsBtnPressed = true;
    }
    public void ButtonUp()
    {
        IsBtnPressed = false;
        HandleInput(_StartPos);
    }
    void FixedUpdate()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            HandleTouchInput();
        }
        if(Application.platform ==RuntimePlatform.WindowsEditor)
        {
            HandleInput(Input.mousePosition);
        }

    }
    void HandleTouchInput()
    {    
        // 위의 함수는 모바일에서 원안에서 터치 되는 지만 판단
        int i = 0;
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                i++;
                Vector3 touchPos = new Vector3(touch.position.x,
                    touch.position.y);
                if(touch.phase == TouchPhase.Began)
                {
                    if(touch.position.x <= (_StartPos.x +_dragRadius))
                    {    //터치한 x값 좌표가 원안에 있다면 
                        _touchId = i; ;
                    }
                    if (touch.position.y <= (_StartPos.y + _dragRadius))
                    {    //터치한 y값 좌표가 원안에 있다면 
                        _touchId = i; ;
                    }
                }
                if(touch.phase  == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {    //  터치 버튼이 움직이고 있거나  멈추어져 있다면 
                    if(_touchId == i) //원안에 있다면 
                    {
                        HandleInput(touchPos); //실제로 움직이는 함수 
                    }
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    if(_touchId == i)
                    {
                        _touchId = -1;
                    }
                }

            }



        }

    }
    void HandleInput(Vector3 input)
    {   
        //위의 함수는 실제로 조이스틱 패드를 움직이는 함수 
        if(IsBtnPressed) //터치패드가 누르고 있는 상태라면
        {
            Vector3 differVector = (input - _StartPos);
            if(differVector.sqrMagnitude > (_dragRadius * _dragRadius))
            {    //터치패드가 원 밖을 벗어나면
                differVector.Normalize(); // 방향을 정규화 하고 
                touchPad.position = _StartPos + differVector * _dragRadius;
                 //방향은 유지 한채 원끝에서 원밖을 벗어 나지 않는다.
            }
            else // 원 안이라면
            {
                touchPad.position = input;
            }


        }
        else  //누르고 있지 않타면 
        {
            touchPad.position = _StartPos;
        }
        Vector3 differ = touchPad.position - _StartPos;
        Vector3 NormalDiffer = new Vector3(differ.x / _dragRadius, differ.y / _dragRadius);
        if(player!=null)
        {
            player.OnStickPos(NormalDiffer);
        }

    }
}
