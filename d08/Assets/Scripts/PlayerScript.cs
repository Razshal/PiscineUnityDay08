using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class PlayerScript : CharacterScript{    private RaycastHit clickHit;    new private Camera camera;    private int lastAttack = 0;    private int frameCount;
    // Allows the animation to communicate his attack frame
    public bool attackFrame = false;    public Slider lifeSlider;    public Slider XpSlider;    new void Start()    {        base.Start();        camera = Camera.main;        lifeSlider.maxValue = life;        XpSlider.maxValue = requieredXp;    }    private void OnTriggerStay(Collider other)
    {
        if (state != State.ATTACKING            && !enemyTarget            && !other.isTrigger            && other.gameObject.CompareTag("Enemy")
            && other.gameObject.GetComponent<CharacterScript>().state != State.DEAD            && !prioritaryWaypoint)        {
            enemyTarget = other.gameObject;
        }
    }    public void AttackEnnemyForAnimation()    {
        if (state == State.ATTACKING && enemyTarget && attackFrame && lastAttack + 5 < frameCount)
        {            lastAttack = frameCount;            enemyTarget.GetComponent<EnemyScript>()                       .ReceiveDamages(agility, minDamage, maxDamage);
            attackFrame = false;
        }
    }    public void ReceiveExperience(int newXp)    {        experience += newXp;        if (experience > requieredXp)        {            experience -= requieredXp;            level += 1;        }    }    new void Update()    {        base.Update();
        // Updating UI
        lifeSlider.value = life;        XpSlider.value = life;        // Will attack if animation communicated the right frame
        AttackEnnemyForAnimation();
        // Check if the right attack frame is passed
        frameCount = Time.frameCount;

        // Sets player click movement instructions
        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit)
            && !clickHit.collider.gameObject.CompareTag("Enemy"))
        {
            navMeshAgent.SetDestination(clickHit.point);
            prioritaryWaypoint = true;
            enemyTarget = null;
        }    }}