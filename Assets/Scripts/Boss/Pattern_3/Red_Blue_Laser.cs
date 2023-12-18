using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Red_Blue_Laser : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> _redMirrorSprites;
    [SerializeField] List<SpriteRenderer> _blueMirrorSprites;

    [SerializeField] List<Collider2D> _redMirrorCols;
    [SerializeField] List<Collider2D> _blueMirrorCols;
    Player _playerScript;

    private void Start()
    {
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        // 빨강 레이저가 활성화 될 때
        if (_playerScript.is_jump)
        {
            ActivateMirror(_redMirrorSprites, _redMirrorCols);
            DeActivateMirror(_blueMirrorSprites, _blueMirrorCols);
        }
        // 파란 레이저가 활성화 될 때
        else
        {
            ActivateMirror(_blueMirrorSprites, _blueMirrorCols);
            DeActivateMirror(_redMirrorSprites, _redMirrorCols);
        }
    }

    void ActivateMirror(List<SpriteRenderer> sprites, List<Collider2D> colliders)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
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
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        }
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
    }
}