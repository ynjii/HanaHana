using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPattern : MonoBehaviour
{
    [SerializeField] private List<Transform> targetPositions;
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> birdSpawn;
    void Start()
    {
        startEvent();
    }

    // Update is called once per frame
    private void startEvent()
    {
        StartCoroutine(Pattern1Coroutine());
    }

    IEnumerator Pattern1Coroutine() //2초(fade) + 2초(이동) + 6초 (새 initiate) 
    {
        yield return new WaitForSeconds(3.0f); //3초 대기 후 (fadeouteffect 2초, 1초 후에 움직이기 시작)
        this.GetComponent<CapsuleCollider2D>().enabled = true;
        yield return StartCoroutine(SetActiveGhostEffect(true));//잔상 키기
        yield return StartCoroutine(movetToTarget(1f, targetPositions[0].position));//오른쪽으로 이동, 1초동안 움직임
        yield return StartCoroutine(SetActiveGhostEffect(false));//잔상 끄기
        birdSpawn[0].SetActive(true); //새 initiate 하기. (5초동안 새를 내뿜는다)
        yield return StartCoroutine(Pattern2Coroutine());
    }


    IEnumerator Pattern2Coroutine() //6 + 7초 사용해야함 
    {
        yield return new WaitForSeconds(6.0f);
        yield return StartCoroutine(SetActiveGhostEffect(true));//잔상 키기
        yield return StartCoroutine(movetToTarget(0.5f, targetPositions[1].position));//왼쪽으로 이동, 0.5초동안 움직임
        yield return StartCoroutine(SetActiveGhostEffect(false));//잔상 끄기
        yield return new WaitForSeconds(0.5f);
        birdSpawn[1].SetActive(true); //새 initiate 하기. (5초동안 새를 내뿜는다)
    }

    IEnumerator SetActiveGhostEffect(bool isOn)
    {
        GhostEffect ghostEffect = gameObject.GetComponent<GhostEffect>();
        if (ghostEffect != null)
        {
            ghostEffect.enabled = isOn;
        }
        yield return null;
    }

    IEnumerator SetActiveSerialBirds(bool isOn)
    {
        SerialInitiate birds = gameObject.GetComponent<SerialInitiate>();
        if (birds != null)
        {
            birds.enabled = isOn;
        }
        yield return null;
    }

    IEnumerator movetToTarget(float duration, Vector3 targetPos)
    {
        //파라미터 함수 위치로 움직임
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }


}
