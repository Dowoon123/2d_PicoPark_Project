using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchWallTrue : InteractableObject
{
    
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,

    }
    private void Start()
    {

      
    }

    private void Update()
    {
       
    }

    public Direction direction = Direction.LEFT;
    public float moveLength;
    public override void OnAction()
    {
        //isAction�� true�� false ��� ����ؾ���.
        //true�ϰ�� �����̰� false�� ��� ���ڸ��� ���ƿ;���.
        //�� ���� ������.
        //false�� ��� �ݴ� �������� ȣ�� �Ǵ� ���ڸ��� ���ư��� �ϸ��.
        //�ٽ� ¥�ߵǳ� �ʹ�.

        gameObject.SetActive(true);
        StartCoroutine(MoveToDirection());

    }

    public IEnumerator MoveToDirection()
    {
        float timer = 0;
        var dir = Vector2.zero;
        while(true)
        {
            switch(direction)
            {

                case Direction.LEFT:
                    dir = Vector2.left;
                    break;
                case Direction.RIGHT:
                    dir = Vector2.right;
                    break;
                case Direction.UP:
                    dir = Vector2.up;
                    break;
                case Direction.DOWN:
                    dir = Vector2.down;
                    break;

                default:
                    break;
            }

            timer += Time.deltaTime;

            if (isAction == true)
            {
                transform.Translate(dir * 0.1f);
                if (timer > moveLength)
                    break;
            }
            if(isAction== false)
            {
                transform.Translate(-dir * 0.1f);
                if (timer > moveLength)
                    break;
            }


            yield return new WaitForSeconds(0.01f);
        }


        yield return null;
    }
}
