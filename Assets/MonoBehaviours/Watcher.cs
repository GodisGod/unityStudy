using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//附加组件，此漫游脚本附加到任何GameObject都将自动添加所需的组件
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Watcher : MonoBehaviour
{
    public float pursuitSpeed;//敌人追击玩家的速度
    public float wanderSpeed;//漫游速度
    float currentSpeed;//当前速度，为上面两个速度之一

    public float directionChangeInterval;

    public bool followPlayer;//打开和关闭追逐玩家行为（有的敌人只漫游不追逐）
    Coroutine moveCoroutine;//移动协程
    Rigidbody2D rb2d;
    Animator animator;

    Transform targetTransform = null;//玩家坐标

    Vector3 endPosition;//新的漫游目的地

    float currentAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine()) ;
    }

    private IEnumerator WanderRoutine()
    {
        while (true) {
            ChooseNewEndpoint();//选择新地点
            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);//如果敌人正在移动则先停下再往新的地点移动
            }
            moveCoroutine = StartCoroutine(Move(rb2d,currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);

        }
    }

    private IEnumerator Move(Rigidbody2D rb2d, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;//当前位置和目的地之间的距离(向量的模)
        while (remainingDistance > float.Epsilon) {
            if (targetTransform != null) {
                endPosition = targetTransform.position;//用玩家的位置代替原本要去的新地点的位置，假如敌人要从A-->B,过程中发现了玩家，则把目的地B的位置替换成玩家的位置，从而向玩家开始移动
            }
            if (rb2d != null) {
                animator.SetBool("isWalking",true);//敌人移动动画
                Vector3 newPosition = Vector3.MoveTowards(rb2d.position, endPosition, speed * Time.deltaTime);//当前位置，结束位置，移动距离
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;//更新剩余距离
            }
            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("isWalking",false);//等待选择新方向
    }

    private void ChooseNewEndpoint()
    {
        currentAngle += UnityEngine.Random.Range(0, 360);//随机旋转一个角度
        currentAngle = Mathf.Repeat(currentAngle,360);//角度保持在0-360之间
        endPosition += Vector3FromAngle(currentAngle);//获取新的位置
    }

    /**
     *角度转弧度并获取新方向的标准化向量
     */
    private Vector3 Vector3FromAngle(float currentAngle)
    {
        float inputAngleRadians = currentAngle * Mathf.Deg2Rad;//角度转弧度
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians),0);//新方向的标准化向量
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer) {//是否碰到了玩家，是否是主动追逐型的敌人
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d,currentSpeed));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentSpeed = wanderSpeed;
            targetTransform = null;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
            animator.SetBool("isWalking", false);//等待选择新方向
        }
    }

}
