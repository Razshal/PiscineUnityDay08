using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class PlayerScript : CharacterScript{    private RaycastHit clickHit;    new private Camera camera;    private int frameCount;

    // Player ui references
    public Slider lifeSlider;    public Slider XpSlider;    public Text lifeText;    public Text xpText;    public Text lvlText;    public GameObject enemyInfosPanel;    public Slider enemyLifeSlider;    public Text enemyName;    public Text enemyLevel;    public GameObject enemyHover;    new void Start()    {        base.Start();        camera = Camera.main;        lifeSlider.maxValue = life;        XpSlider.maxValue = requieredXp;        displayName = "Maya";    }    private void OnTriggerStay(Collider other)
    {
        if (state != State.ATTACKING            && !enemyTarget            && !other.isTrigger            && other.gameObject.CompareTag("Enemy")
            && other.gameObject.GetComponent<CharacterScript>().state != State.DEAD            && !prioritaryWaypoint)        {
            enemyTarget = other.gameObject;
        }
    }    public void ReceiveExperience(int newXp)    {        experience += newXp;        if (experience > requieredXp)        {            experience -= requieredXp;            level += 1;        }        XpSlider.value = experience;        lvlText.text = "LVL " + level;    }    private void UpdateUi()    {        CharacterScript enemyToDisplay = null;

        // Determine wich value needs to be displayed
        if (enemyTarget)            enemyToDisplay = enemyTarget.GetComponent<CharacterScript>();        else if (enemyHover)            enemyToDisplay = enemyHover.GetComponent<CharacterScript>();                // Then if enemy, display his infos        if (enemyToDisplay)        {
            enemyInfosPanel.SetActive(true);
            enemyLifeSlider.maxValue = enemyToDisplay.maxLife;            enemyLifeSlider.value = enemyToDisplay.life;            enemyName.text = enemyToDisplay.displayName;            enemyLevel.text = "LVL " + enemyToDisplay.level;
        }        else            enemyInfosPanel.SetActive(false);        // Update the player ones        lifeSlider.value = life;        lifeText.text = life + "/" + lifeSlider.maxValue;    }    new void Update()    {        base.Update();

        // Updating UI
        UpdateUi();

        // Sets player click movement instructions
        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit)
            && !clickHit.collider.gameObject.CompareTag("Enemy"))
        {
            navMeshAgent.SetDestination(clickHit.point);
            prioritaryWaypoint = true;
            enemyTarget = null;
        }    }}