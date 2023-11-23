using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Yellow_Mirror : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> _greenMirrorSprites;
    [SerializeField] List<SpriteRenderer> _yellowMirrorSprites;

    [SerializeField] List<Collider2D> _greenMirrorCols;
    [SerializeField] List<Collider2D> _yellowMirrorCols;
    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        // 초록 레이저가 활성화 될때
        if (_player.GetComponent<SpriteRenderer>().flipX)
        {
            ActivateMirror(_greenMirrorSprites, _greenMirrorCols);
            DeActivateMirror(_yellowMirrorSprites, _yellowMirrorCols);
        }
        // 노란 레이저가 활성화 될때
        else
        {
            ActivateMirror(_yellowMirrorSprites, _yellowMirrorCols);
            DeActivateMirror(_greenMirrorSprites, _greenMirrorCols);
        }
    }

    void ActivateMirror(List<SpriteRenderer> sprites, List<Collider2D> colliders)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = true;
        }
        foreach (Collider2D col in colliders)
        {
            col.enabled = true;
        }
    }

    void DeActivateMirror(List<SpriteRenderer> sprites, List<Collider2D> colliders)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
    }
}
