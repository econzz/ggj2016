using System;
using System.Linq;

public static class EnumCommon
{
	private static readonly Random mRandom = new Random ();

	public static T Random<T> ()
	{
		var allDataTypes = Enum.GetValues (typeof(T));
		int num = mRandom.Next (0, allDataTypes.Length);
		return (T)allDataTypes.GetValue (num);
	}

	public static int GetLength<T> ()
	{
		return Enum.GetValues (typeof(T)).Length;
	}

	public static T GetEnum<T> (int num)
	{
		var allDataTypes = Enum.GetValues (typeof(T));
		return (T)allDataTypes.GetValue (num);
	}
}