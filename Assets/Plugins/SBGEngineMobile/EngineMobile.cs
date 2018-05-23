using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace StarburstGaming {

	public class EngineMobile {

#if UNITY_IOS
		[DllImport("__Internal")]
		private static extern bool StarburstEngineMobileStart(ref IntPtr err);
		[DllImport("__Internal")]
		private static extern void StarburstEngineMobileStop();

		public static void Start() {
			IntPtr errPtr = IntPtr.Zero;
			bool noErr = StarburstEngineMobileStart(ref errPtr);
			if (!noErr) {
				throw new ExternalException("Cannot start Starburst Engine on iOS. See log for more information");
			}
		}

		public static void Stop() {
			StarburstEngineMobileStop();
		}

#elif UNITY_ANDROID

		private static void call(string method, params object[] args) {
			AndroidJavaClass ajc = new AndroidJavaClass("com.starburstcomputing.engine.mobile.Mobile");
			ajc.CallStatic(method, args);
		}

		public static void Start() {
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
			call("start", context);
		}

		public static void Stop() {
			call("stop");
		}
#endif

	}
}
