using UnityEngine;

public class NetworkManagerScript : MonoBehaviour
{
    private float buttonX;
    private float buttonY;
    private float buttonWidth;
    private float buttonHeight;

    private bool refreshing = false;

    private HostData[] hostData;
    public bool serverInitialized = false;

    private string gameName = "CastleBomba";

    public int incommingConnections = 32;
    public int port = 25122;

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
        if (refreshing)
        {
            if (MasterServer.PollHostList().Length > 0)
            {
                refreshing = false;
                Debug.Log(MasterServer.PollHostList().Length);
                hostData = MasterServer.PollHostList();
            }
        }

    }

    void StartServer()
    {
        Network.InitializeServer(incommingConnections, port, !Network.HavePublicAddress());
        MasterServer.RegisterHost(gameName, "SessionONE", "Awesome prototype");
    }

    void refreshHostList()
    {
        MasterServer.RequestHostList(gameName);
        refreshing = true;
    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized!");
        networkView.RPC("setServerInitialized", RPCMode.AllBuffered);
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered Server");
        }
    }

    [RPC]
    void setServerInitialized()
    {
        serverInitialized = true;
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Start Server"))
            {
                Debug.Log("Starting Server");
                StartServer();

            }

            if (GUI.Button(new Rect(buttonX, buttonY * 1.2f + buttonHeight, buttonWidth, buttonHeight), "Refresh Host"))
            {
                Debug.Log("Refreshing");
                refreshHostList();
            }

            if (hostData != null)
            {
                for (int i = 0; i < hostData.Length; i++)
                {
                    if (GUI.Button(new Rect(buttonX * 1.5f + buttonWidth, buttonY * 1.2f + (buttonHeight * 1f), buttonWidth * 3f, buttonHeight * 0.5f), hostData[i].gameName))
                    {
                        Network.Connect(hostData[i]);
                    }
                }
            }
        }
    }
}
