using UnityEngine;

public class MapStartObject : MonoBehaviour
{
    public int mapIndex = 0; // 해당 맵의 인덱스 (사과맵: 0, 성맵: 1, 거울맵: 2 등)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 오브젝트에 닿았을 때
        {
            MapBackgroundMusic backgroundMusicScript = FindObjectOfType<MapBackgroundMusic>();
            backgroundMusicScript.ChangeBackgroundMusic(mapIndex);
        }
    }
}
