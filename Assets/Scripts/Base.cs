using UnityEngine;

public class Base : MonoBehaviour
{

    public GameObject unitPref;
    public GameObject secondUnitPref;
    public Color baseColor = Color.blue;

    private float lastSpawn = 0;
    public bool myBase = false;
    private GameController globalControl;
    private GameObject currentPref;
    // Use this for initialization
    void Awake()
    {
        currentPref = unitPref;
        globalControl = GameObject.FindGameObjectWithTag(Tags.gamecontroller).GetComponent<GameController>();
        transform.FindChild("pointlight").light.color = baseColor;

        if (this.gameObject == GameObject.FindGameObjectWithTag(Tags.basis))
        {
            myBase = true;
            SpawnUnit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myBase && Input.GetButtonDown("ChangeUnit") && SpawningEnabled())
        {
            SpawnUnit();
        }
        if (myBase)
            SwitchClass();

    }

    bool SpawningEnabled()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
        bool farEnoughAway = (transform.position - player.transform.position).magnitude > globalControl.noSpawnRadius;
        bool noCoolDown = Time.time - lastSpawn >= globalControl.respawnTime;
        return farEnoughAway && noCoolDown;
    }

    public void SpawnUnit(bool changePlayer = true)
    {
        lastSpawn = Time.time;
        GameObject unit = (GameObject)GameObject.Instantiate(currentPref, transform.position, transform.rotation);
        if (changePlayer)
            unit.GetComponent<PlayerController>().onLive = true;
    }

    void SwitchClass()
    {
        if (Input.GetButtonDown("ChangeClass"))
        {
            GameObject bunny = transform.FindChild("bunny").gameObject;
            GameObject turtle = transform.FindChild("turtle").gameObject;
            bunny.SetActive(!bunny.activeSelf);
            turtle.SetActive(!turtle.activeSelf);
            if (bunny.activeSelf)
                currentPref = secondUnitPref;
            else
                currentPref = unitPref;
        }
    }
}
