using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    // 移動先の座標
    public Vector3 destination;

    // 移動速度
    public float speed = 5.0f;

    // 加速度
    public float acceleration = 1.0f;

    // 減速度
    public float deceleration = 2.0f;

    // 到着距離
    public float stoppingDistance = 0.1f;

    // 移動中かどうか
    private bool isMoving = false;

    // 移動開始時の座標
    private Vector3 startPosition;

    // 移動中の速度
    private float moveSpeed = 0.0f;

    // 移動方向と距離
    private Vector3 moveDirection;
    private float moveDistance;

    // 移動開始
    public void StartMoving()
    {
        startPosition = transform.position;
        moveDirection = (destination - startPosition).normalized;
        moveDistance = Vector3.Distance(startPosition, destination);
        moveSpeed = 0.0f;
        isMoving = true;
    }

    // 移動停止
    public void StopMoving()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // 加速度
            moveSpeed = Mathf.MoveTowards(moveSpeed, speed, acceleration * Time.deltaTime);

            // 移動量
            float moveAmount = moveSpeed * Time.deltaTime;

            // 残り距離
            float remainingDistance = Vector3.Distance(transform.position, destination);

            if (remainingDistance < stoppingDistance)
            {
                // 到着したら停止
                transform.position = destination;
                isMoving = false;
            }
            else if (remainingDistance < moveAmount)
            {
                // 移動量が残り距離より大きい場合、残り距離分だけ移動
                transform.position += moveDirection * remainingDistance;
            }
            else
            {
                // 通常の移動量
                transform.position += moveDirection * moveAmount;
            }

            // 減速度
            if (!isMoving)
            {
                moveSpeed = Mathf.MoveTowards(moveSpeed, 0.0f, deceleration * Time.deltaTime);
            }
        }
    }

    public bool GetIsMoving()
	{
        return isMoving;

    }
}