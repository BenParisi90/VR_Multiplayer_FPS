using Bolt;
using UdpKit;
using UnityEngine;
using Process = System.Diagnostics.Process;

public partial class BoltDebugStart : BoltInternal.GlobalEventListenerBase
{
	private UdpEndPoint _serverEndPoint;
	private UdpEndPoint _clientEndPoint;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Application.targetFrameRate = 60;
	}

	void Start()
	{
#if UNITY_EDITOR_OSX
		Process p = new Process();
		p.StartInfo.FileName = "osascript";
		p.StartInfo.Arguments =

	@"-e 'tell application """ + UnityEditor.PlayerSettings.productName + @"""
  activate
end tell'";

		p.Start();
#endif

		BoltRuntimeSettings settings = BoltRuntimeSettings.instance;

		_serverEndPoint = new UdpEndPoint(UdpIPv4Address.Localhost, (ushort)settings.debugStartPort);
		_clientEndPoint = new UdpEndPoint(UdpIPv4Address.Localhost, 0);

		BoltConfig cfg;

		cfg = settings.GetConfigCopy();
		cfg.connectionTimeout = 60000000;
		cfg.connectionRequestTimeout = 500;
		cfg.connectionRequestAttempts = 1000;

		if (string.IsNullOrEmpty(settings.debugStartMapName) == false)
		{
			if (BoltDebugStartSettings.DebugStartIsServer)
			{
				BoltLog.Warn("Starting as SERVER");

				BoltLauncher.StartServer(_serverEndPoint, cfg);
			}
			else if (BoltDebugStartSettings.DebugStartIsClient)
			{
				BoltLog.Warn("Starting as CLIENT");

				BoltLauncher.StartClient(_clientEndPoint, cfg);
			}
			else if (BoltDebugStartSettings.DebugStartIsSinglePlayer)
			{
				BoltLog.Warn("Starting as SINGLE PLAYER");

				BoltLauncher.StartSinglePlayer(cfg);
			}

			BoltDebugStartSettings.PositionWindow();
		}
		else
		{
			BoltLog.Error("No map found to start from");
		}
	}

	public override void BoltStartFailed(UdpConnectionDisconnectReason disconnectReason)
	{
		BoltLog.Error("Failed to start debug mode");
	}

	public override void BoltStartDone()
	{
		if (BoltNetwork.IsServer || BoltNetwork.IsSinglePlayer)
		{
			BoltNetwork.LoadScene(BoltRuntimeSettings.instance.debugStartMapName);
		}
		else if (BoltNetwork.IsClient)
		{
			BoltNetwork.Connect((ushort)BoltRuntimeSettings.instance.debugStartPort);
		}
	}

	public override void SceneLoadLocalDone(string scene, IProtocolToken token)
	{
		Destroy(gameObject);
	}
}

