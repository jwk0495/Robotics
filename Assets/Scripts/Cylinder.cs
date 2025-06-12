using System.Collections;
using UnityEngine;

// 실린더 Rod를 minRange 만큼 후진, maxRange 만큼 전진
// 속성 : 실린더 Rod 의 transform, minRange Pos, maxRange Pos, 속도
public class Cylinder : MonoBehaviour
{
    public enum SolenoidType
    {
        단방향솔레노이드,
        양방향솔레노이드
    }
    SolenoidType type = SolenoidType.양방향솔레노이드;

    public Transform cylinderRod;
    public float speed; // 공압밸브 조절
    public float minPosY;
    public float maxPosY;
    public bool isForward;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 back = new Vector3(0, minPosY, 0);
            Vector3 front = new Vector3(0, maxPosY, 0);
            StartCoroutine(MoveCylinder(back, front,isForward));  
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 back = new Vector3(0, minPosY, 0);
            Vector3 front = new Vector3(0, maxPosY, 0);
            StartCoroutine(MoveCylinder(front, back,!isForward));
        }
    }

    IEnumerator MoveCylinder (Vector3 from, Vector3 to,bool isForward)
    {
        Vector3 direction;
        
        while (true)
        {
            if (isForward)
                direction = to - cylinderRod.localPosition;
            
            else
                direction = from - cylinderRod.localPosition;

            Vector3 normalizedDir = Vector3.Normalize(direction);
            float distance = direction.magnitude;

            if (distance < 0.1f)
            {
                cylinderRod.localPosition = to;
                break;
            }
            cylinderRod.localPosition += normalizedDir * (speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

    }

}