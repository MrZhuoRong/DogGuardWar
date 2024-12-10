
using UnityEngine;

public class FaceMouse : MonoBehaviour
{

    public Camera mainCamera;
    private Vector3 targetPosition;

    void Update()
    {
        FaceTo();
    }

    public void FaceTo()
    {
        // 发射一条射线，获取鼠标所指地面位置
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            targetPosition = hit.point;
            targetPosition.y = transform.position.y;

            // 计算人物将要面向的目标方向
            Vector3 directionToTarget = targetPosition - transform.position;

            if (directionToTarget.magnitude > 1f)
            {
                // 将人物的forward方向旋转至目标方向
                transform.rotation = Quaternion.LookRotation(directionToTarget);
            }

        }
    }
}