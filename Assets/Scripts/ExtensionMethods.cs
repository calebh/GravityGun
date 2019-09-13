using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class ExtensionMethods
{
	public static float MapInterval(this float value, float fromLow, float fromHigh, float toLow, float toHigh)
	{
		return Mathf.Lerp(toLow, toHigh, Mathf.InverseLerp(fromLow, fromHigh, value));
	}


	public static float MapIntervalWithClamp(this float value, float fromLow, float fromHigh, float toLow, float toHigh)
	{
		return Mathf.Clamp(value, fromLow, fromHigh).MapInterval(fromLow, fromHigh, toLow, toHigh);
	}
}