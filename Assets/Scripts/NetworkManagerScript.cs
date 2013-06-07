using UnityEngine;

public class NetworkManagerScript : MonoBehaviour
{
    private float buttonX;
    private float buttonY;
    private float buttonWidth;
    private float buttonHeight;

    public int incommingConnections = 32;
    public int port = 25001;

    // Use this for initialization
    void Start()
    {
        buttonX = Screen.width * 0.05f;
        buttonY = Screen.height * 0.05f;
        buttonWidth = Screen.width * 0.1f;
        buttonHeight = Screen.width * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartServer()
    {
        Network.InitializeServer(incommingConnections, port, !Network.HavePublicAddress());
        MasterServer.RegisterHost("CastleBomba", "SessionONE", "Awesome prototype");
    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized!");
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        Debug.Log("Things I do " + mse.ToString());
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered Server");
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Start Server"))
        {
            Debug.Log("Starting Server");
            StartServer();

        }

        if (GUI.Button(new Rect(buttonX, buttonY * 1.2f + buttonHeight, buttonWidth, buttonHeight), "Refresh Host"))
        {
            Debug.Log("Refreshing");
        }
    }
}
