using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pattern3_1 : MonoBehaviour
{
    GameObject _player;
    LineRenderer _lineRenderer;
    private AudioSource _audioSource;
    
    public LayerMask _layerMask;
    public Transform _appleGenPosition;
    public GameObject _apple;
    public int _reflectionsNum = 4;
    public int max, min;

    private bool _lookAtPlayer = true;
    private Vector3 fixedDir;
    static public bool isAppleDestroyed = true;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = gameObject.GetComponent<AudioSource>();

        _lineRenderer.startColor = _lineRenderer.endColor = new Color(1,0,0,0.7f);
        _lineRenderer.startWidth = _lineRenderer.endWidth = 0.25f;

        StartCoroutine(Pattern());
    }

    IEnumerator Pattern()
    {
        // int range = Random.Range(min, max);
        _appleGenPosition.localPosition = new Vector3(GetShooterRange(), _appleGenPosition.localPosition.y, 0);
        StartCoroutine(WaitPlayer());
        yield return new WaitForSeconds(3f);
    }

    private int GetShooterRange()
    {
        if (_player.transform.localPosition.x > 0)
        {
            return Random.Range(3, 8);
        }
        else
        {
            return Random.Range(-8, -3);
        }
    }

    IEnumerator WaitPlayer()
    {
        _lookAtPlayer = true;
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(_player.transform.position);
        StartCoroutine(SetTarget());

        yield return new WaitForSeconds(1f);
        DangerMarkerDeactive();
        yield return new WaitForSeconds(0.1f);
        ShootApple();
    }

    IEnumerator SetTarget()
    {
        while (true)
        {
            yield return null;
            if (!_lookAtPlayer) break;
            // transform.LookAt ( _player.transform.localPosition );
            DangerMarkerShoot();
        }
    }

    public void DangerMarkerShoot()
    {
        /**
        Vector3 newPosiiton = _appleGenPosition.localPosition;
        fixedDir = (_player.transform.localPosition - newPosiiton).normalized;
        Vector3 newDir = fixedDir;
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, _appleGenPosition.localPosition);

        for (int i = 1; i <= _reflectionsNum; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(newPosiiton + new Vector3(0.1f, 0.1f, 0f), newDir, 250f, _layerMask);
            if (hit.point == new Vector2(newPosiiton.x, newPosiiton.y))
            {
                hit = Physics2D.Raycast(newPosiiton + new Vector3(-0.1f, -0.1f, 0f), newDir, 250f, _layerMask);
            }
            newPosiiton = hit.point;
            newDir = Vector3.Reflect(newDir, hit.normal);
            newDir = new Vector3(newDir.x, newDir.y, 0).normalized;
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(i, newPosiiton);
        }
        **/
        _audioSource.Play();
        Vector3 newPosiiton = _appleGenPosition.localPosition;
        fixedDir = (_player.transform.localPosition - newPosiiton).normalized;
        Vector3 newDir = fixedDir;
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition ( 0, newPosiiton );
        for ( int i = 1 ; i < _reflectionsNum ; i++ )
        {
            RaycastHit2D hit = Physics2D.Raycast( newPosiiton + new Vector3(0.1f, 0.1f, 0f), newDir, 50f, _layerMask );
            if (hit.point == new Vector2(newPosiiton.x, newPosiiton.y))
            {
                hit = Physics2D.Raycast(newPosiiton + new Vector3(-0.1f, -0.1f, 0f), newDir, 250f, _layerMask);
            }
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition ( i, hit.point );

            newPosiiton = hit.point;
            newDir = Vector2.Reflect ( newDir, hit.normal );
        }
    }

    public void DangerMarkerDeactive()
    {
        _audioSource.Stop();
        _lookAtPlayer = false;
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.SetPosition(i, Vector3.zero);
        }
        _lineRenderer.positionCount = 0;
    }

    public void ShootApple()
    {
        GameObject apple = Instantiate(_apple, _appleGenPosition.position, Quaternion.Euler(fixedDir));
        apple.GetComponent<Pattern3_Apple>().newDir = fixedDir;
        _appleGenPosition.GetComponent<SpriteRenderer>().enabled = false;
    }
}