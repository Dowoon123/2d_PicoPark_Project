using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchWall : InteractableObject
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
            
    }

    public Direction direction = Direction.LEFT;
    public float moveLength;
    public override void OnAction()
    {
        isAction = true;

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
            transform.Translate(dir*0.1f);
            if (timer > moveLength)
                break;


            yield return new WaitForSeconds(0.01f);
        }


        yield return null;
    }
}
