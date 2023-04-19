using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookShotScript : MonoBehaviour
{
    private FirstPersonController fps;
    [SerializeField] private GameObject debugCube;
    [SerializeField] private GameObject hook;
    [SerializeField] private float flySpeed, hookRange;
    [SerializeField] private AudioClip[] hookNoise;
    [SerializeField] private Slider hookSlider;
    private AudioSource _hookSource;
    private Rigidbody rb;
    private GameObject _gameObject;
    private Vector3 _hitPoint, _hookMomentum;
    private HookState _hookState;
    private float _hookDetect = 2f, _hookSize, _coolRate = .2f;
    private bool _hookCoolDown;
    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        fps = GetComponentInParent<FirstPersonController>();
        _hookSource = GetComponent<AudioSource>();
        hook.SetActive(false);
    }

    void Update()
    {

        if (fps.HP < 1)
        {
            _hookState = HookState.NotHooked;
            rb.velocity = Vector3.zero;
        }
        SwitchState();
    }

    private void SwitchState()
    {
        switch (_hookState)
        {
            case HookState.NotHooked:
                {
                    hook.SetActive(false);
                    rb.velocity += _hookMomentum;
                    if (_hookMomentum.magnitude >= .1)
                    {
                        float drag = 80;
                        _hookMomentum -= _hookMomentum * (drag * Time.deltaTime);
                        if (_hookMomentum.magnitude < .1)
                        {
                            _hookMomentum = Vector3.zero;
                        }
                    }
                    if (fps.OnGround()) fps.InControl = true;

                    if (!_hookCoolDown) StartHookShot();
                    else HookCoolDown();
                    break;
                }

            case HookState.Hooked:
                {
                    ThrowHook();
                    break;
                }

            case HookState.Flying:
                {
                    fps.InControl = false;
                    FlyToHook();
                    break;
                }

        }
    }

    private void StartHookShot()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(fps.Eyes.transform.position, fps.Eyes.transform.forward, out RaycastHit hit, hookRange, ~(1 << 10 | 1 << 2)))
            {
                _hitPoint = hit.point;
                _gameObject = hit.collider.gameObject;
                _hookSize = 0;
                hook.transform.localScale = new Vector3(0, 0, 0);
                hook.SetActive(true);
                _hookSource.PlayOneShot(hookNoise[1]);
                _hookState = HookState.Hooked;
                return;
            }
            _hookSource.PlayOneShot(hookNoise[0]);
        }
    }

    private void ThrowHook()
    {

        float hookSpeed = 70;
        hook.transform.LookAt(_hitPoint);
        _hookSize += hookSpeed * Time.deltaTime;
        hook.transform.localScale = new Vector3(.15f, .15f, _hookSize);

        if (_hookSize >= Vector3.Distance(transform.position, _hitPoint))
        {
            _hookSource.PlayOneShot(hookNoise[2]);
            _hookState = HookState.Flying;
        }
    }

    private void FlyToHook()
    {
        hook.transform.LookAt(_hitPoint);
        Vector3 dir = _hitPoint - fps.transform.position;
        float maxSpeed = 40;
        float minSpeed = 10;
        float speedMulti = 80;
        fps.RB.useGravity = false;
        flySpeed = Mathf.Clamp(Vector3.Distance(fps.transform.position, _hitPoint), minSpeed, maxSpeed);
        fps.RB.velocity = (dir.normalized * flySpeed) * (speedMulti * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float extraSpeed = 7;
            _hookMomentum = dir.normalized * (flySpeed * Time.deltaTime);
            float jumpSpeed = 9;
            Vector3 jumpForce = Vector3.up * jumpSpeed;

            _hookMomentum += jumpForce;
            float speed = 5.15f;
            _coolRate = speed;
            Unhook();
            return;
        }

        float dist = Vector3.Distance(fps.transform.position, _hitPoint);

        if (dist <= .4f)
        {
            fps.RB.velocity = Vector3.zero;
            float speed = .85f;
            _coolRate = speed;
            Unhook();
            return;
        }

        if (Physics.Raycast(fps.Eyes.transform.position, dir, out RaycastHit hit, _hookDetect))
        {
            if (hit.collider != null)
            {
                fps.RB.velocity = Vector3.zero;
                float speed = .85f;
                _coolRate = speed;
                Unhook();
            }
        }

    }

    private void HookCoolDown()
    {
        if (hookSlider.value >= 1)
        {
            _hookCoolDown = false;
        }
        else
        {
            hookSlider.value += _coolRate * Time.deltaTime;
        }
    }

    private void Unhook()
    {
        fps.RB.useGravity = true;
        _hookSource.PlayOneShot(hookNoise[0]);
        hookSlider.value = 0;
        _hookCoolDown = true;
        _hookState = HookState.NotHooked;
    }
    private enum HookState
    {
        NotHooked,
        Hooked,
        Flying,

    }
}
