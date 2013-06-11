using UnityEngine;

public class NetworkManagerScript : MonoBehaviour
{
    private float buttonX;
    private float buttonY;
    private float buttonWidth;
    private float buttonHeight;

    private bool refreshing = false;

    public bool serverInitialized = false;

    private string gameName = "CastleBomba";

    public int incommingConnections = 32;
    public int port = 25122;


    public string connectionIP = "127.0.0.1";

    // Use this for initialization
    void Start()
    {
        buttonX = Screen.width * 0.05f;
        buttonY = Screen.height * 0.05f;
        buttonWidth = Screen.width * 0.1f;
        buttonHeight = Screen.width * 0.1f;
    }

    void StartServer()
    {
        Network.InitializeServer(incommingConnections, port, !Network.HavePublicAddress());

    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized!");
        networkView.RPC("setServerInitialized", RPCMode.AllBuffered);
    }

    [RPC]
    void setServerInitialized()
    {
        serverInitialized = true;
    }



    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Start Server"))
            {
                Debug.Log("Starting Server");
                StartServer();

            }

            if (GUI.Button(new Rect(buttonX, buttonY * 1.2f + buttonHeight, buttonWidth, buttonHeight), "Connect to Host"))
            {
                Debug.Log("Refreshing");
                Network.Connect(connectionIP, port);
            }
        }
        else if (Network.peerType == NetworkPeerType.Client)
        {
            GUI.Label(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Status: Connected as Client");
            if (GUI.Button(new Rect(buttonX, buttonY * 1.2f + buttonHeight, buttonWidth, buttonHeight), "Disconnect"))
            {
                Network.Disconnect(200);
            }
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            GUI.Label(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Status: Connected as Server");
            if (GUI.Button(new Rect(buttonX, buttonY * 1.2f + buttonHeight, buttonWidth, buttonHeight), "Disconnect"))
            {
                Network.Disconnect(200);
            }
        }
    }
}
