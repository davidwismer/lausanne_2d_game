using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isMoving)
        {

            if (Input.GetKey("left"))
            {
                input.x = -1;
            }
            else if (Input.GetKey("right"))
            {
                input.x = 1;
            }
            else
            {
                input.x = 0;
            }

            if (Input.GetKey("down"))
            {
                input.y = -1;
            }
            else if (Input.GetKey("up"))
            {
                input.y = 1;
            }
            else
            {
                input.y = 0;
            }


            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
}
