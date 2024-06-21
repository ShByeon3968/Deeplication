using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : Singleton<ServerManager>
{
    public static string localServerIP = "http://192.168.11.161:5000/upload";
    public static int localServerPort = 8080;

}
