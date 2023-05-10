using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    // �ړ���̍��W
    public Vector3 destination;

    // �ړ����x
    public float speed = 5.0f;

    // �����x
    public float acceleration = 1.0f;

    // �����x
    public float deceleration = 2.0f;

    // ��������
    public float stoppingDistance = 0.1f;

    // �ړ������ǂ���
    private bool isMoving = false;

    // �ړ��J�n���̍��W
    private Vector3 startPosition;

    // �ړ����̑��x
    private float moveSpeed = 0.0f;

    // �ړ������Ƌ���
    private Vector3 moveDirection;
    private float moveDistance;

    // �ړ��J�n
    public void StartMoving()
    {
        startPosition = transform.position;
        moveDirection = (destination - startPosition).normalized;
        moveDistance = Vector3.Distance(startPosition, destination);
        moveSpeed = 0.0f;
        isMoving = true;
    }

    // �ړ���~
    public void StopMoving()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // �����x
            moveSpeed = Mathf.MoveTowards(moveSpeed, speed, acceleration * Time.deltaTime);

            // �ړ���
            float moveAmount = moveSpeed * Time.deltaTime;

            // �c�苗��
            float remainingDistance = Vector3.Distance(transform.position, destination);

            if (remainingDistance < stoppingDistance)
            {
                // �����������~
                transform.position = destination;
                isMoving = false;
            }
            else if (remainingDistance < moveAmount)
            {
                // �ړ��ʂ��c�苗�����傫���ꍇ�A�c�苗���������ړ�
                transform.position += moveDirection * remainingDistance;
            }
            else
            {
                // �ʏ�̈ړ���
                transform.position += moveDirection * moveAmount;
            }

            // �����x
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