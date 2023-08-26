using UnityEngine;

public class MapBackgroundMusic : MonoBehaviour
{
    public AudioClip[] musicTracks; // 배경음악 트랙 배열
    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private bool firstMusicPlayed = false; // 첫 번째 배경음악 재생 여부

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // 루프 활성화
    }

    private void Update()
    {
        // 첫 번째 배경음악 재생
        if (!firstMusicPlayed)
        {
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();
            firstMusicPlayed = true;
        }
    }

    // 배경음악 변경 함수
    public void ChangeBackgroundMusic(int trackIndex)
    {
        if (trackIndex < musicTracks.Length)
        {
            currentTrackIndex = trackIndex;
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();
        }
    }
}
