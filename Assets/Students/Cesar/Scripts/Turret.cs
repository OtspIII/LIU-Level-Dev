using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyNormal
{
    [SerializeField] private Transform headPos;
    [SerializeField] private GameObject eyePos;
    [SerializeField] private Transform barrels;
    [SerializeField] private Transform[] bulletPos;
    [SerializeField] private AudioClip[] clips;
    private float barrelSpin = 750,shootDelay = .15f, pastShoot, wakeDelay =.3f;
    private bool wakeUp;
    private int bulletInt;
    private AudioSource audio;
    
    void Awake()
    {
        pastShoot = shootDelay;
        audio = GetComponent<AudioSource>();
    }

   
    void Update()
    {


        if (SeekPlayer(eyePos.transform, ~(1 << 10), sightDistance))
        {
            Attack();
            FacePlayer(headPos);
        }
        else
        {
            eyePos.GetComponent<MeshRenderer>().material.color = Color.green;
            wakeUp = false;
        }

    }

    public override void Attack()
    {
        if(!wakeUp)StartCoroutine(WakeUpNoise());
        else StartCoroutine(TurrentSpin());
    }

    IEnumerator WakeUpNoise()
    {
        yield return new WaitForSeconds(wakeDelay);
        eyePos.GetComponent<MeshRenderer>().material.color = Color.red;
        if(!audio.isPlaying)audio.PlayOneShot(clips[0]);
        wakeUp = true;
    }
    IEnumerator TurrentSpin()
    {
        yield return new WaitForSeconds(.1f);
       ShootBullet();
        barrels.eulerAngles += new Vector3(0,0, barrelSpin * Time.deltaTime);
    }

    void  ShootBullet()
    {
        if (shootDelay <= 0)
        {
            audio.PlayOneShot(clips[1]);
            if (bulletInt > bulletPos.Length - 1) bulletInt = 0;
            GameObject boolet = Instantiate(bulletPrefab, bulletPos[bulletInt++].position, Quaternion.Euler(headPos.rotation.eulerAngles));
            shootDelay = pastShoot;
            bulletInt++;
        }

        else shootDelay -= Time.deltaTime;

    }
}
