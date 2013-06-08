using UnityEngine;

public class Base : MonoBehaviour
{

    public GameObject unitPref;
    public GameObject secondUnitPref;
    public Color baseColor = Color.blue;
    public bool myBase = false;

    private float lastSpawn = 0;

    public bool haseOwener = false;

    private GameController globalControl;
    private GameObject currentPref;
    // Use this for initialization
    void Awake()
    {
        currentPref = unitPref;
        globalControl = GameObject.FindGameObjectWithTag(Tags.gamecontroller).GetComponent<GameController>();
        transform.FindChild("pointlight").light.color = baseColor;
        GameObject[] bases = GameObject.FindGameObjectsWithTag(Tags.basis);


        if (this.gameObject == bases[0] && !bases[1].GetComponent<Base>().myBase)
        {
            myBase = true;
            haseOwener = true;
            SpawnUnit();
        }
        if (this.gameObject == bases[1] && !bases[0].GetComponent<Base>().myBase)
        {
            myBase = true;
            haseOwener = true;
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
        GameObject unit = (GameObject)Network.Instantiate(currentPref, transform.position, transform.rotation, 0);
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
