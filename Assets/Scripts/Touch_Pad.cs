using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. �ڱ��ڽ�(��ġ�е�)�� position ���� ��ŸƮ ���������� ���Ѵ�
//2. ���̽�ƽ �ٱ����� �������� �����Ѵ�.
//3. �е� ��ư�� ������ �� �ľ� �Һ��� ���� 
//4. ����� ��ġ�� ���ȿ��� ��ġ �Ǵ��� �ȵǴ� ���� ���������� �Ǵ�
public class Touch_Pad : MonoBehaviour
{
    [SerializeField][Tooltip("��ġ�е�������")] private RectTransform touchPad;
    [SerializeField] private Vector3 _StartPos;
    [SerializeField][Tooltip("���� ������")] private float _dragRadius = 100f;
    [SerializeField]
    [Tooltip("�÷��̾� Ŭ���� ����")] private Player player;

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
        // ���� �Լ��� ����Ͽ��� ���ȿ��� ��ġ �Ǵ� ���� �Ǵ�
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
                    {    //��ġ�� x�� ��ǥ�� ���ȿ� �ִٸ� 
                        _touchId = i; ;
                    }
                    if (touch.position.y <= (_StartPos.y + _dragRadius))
                    {    //��ġ�� y�� ��ǥ�� ���ȿ� �ִٸ� 
                        _touchId = i; ;
                    }
                }
                if(touch.phase  == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {    //  ��ġ ��ư�� �����̰� �ְų�  ���߾��� �ִٸ� 
                    if(_touchId == i) //���ȿ� �ִٸ� 
                    {
                        HandleInput(touchPos); //������ �����̴� �Լ� 
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
        //���� �Լ��� ������ ���̽�ƽ �е带 �����̴� �Լ� 
        if(IsBtnPressed) //��ġ�е尡 ������ �ִ� ���¶��
        {
            Vector3 differVector = (input - _StartPos);
            if(differVector.sqrMagnitude > (_dragRadius * _dragRadius))
            {    //��ġ�е尡 �� ���� �����
                differVector.Normalize(); // ������ ����ȭ �ϰ� 
                touchPad.position = _StartPos + differVector * _dragRadius;
                 //������ ���� ��ä �������� ������ ���� ���� �ʴ´�.
            }
            else // �� ���̶��
            {
                touchPad.position = input;
            }


        }
        else  //������ ���� ��Ÿ�� 
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
