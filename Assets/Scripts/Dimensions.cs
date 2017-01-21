using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Dimensions {
	int[] dimension;
	public Dimensions(params int[] dim)
	{
		dimension = new int[dim.Length];
		for (int i = 0; i < dim.Length; i++)
			dimension[i] = dim[i];
	}

	int GetSize()
	{
		int size = 1;
		for (int i = 0; i < dimension.Length; i++)
			size *= dimension[i];
		return size;
	}

	int[] GetPosition(int index)
	{
		int[] ret = new int[dimension.Length];
		ret[dimension.Length - 1] = index / dimension[dimension.Length - 1];
		for(int i = dimension.Length - 2; i > 0; i--)
		{
			ret[i] = (index % dimension[i + 1]) / dimension[i];
		}
		ret[0] = index % dimension[1];
		return ret;
	}

	int GetIndex(params int[] pos)
	{
		Debug.Assert(pos.Length == dimension.Length);
		//assume x,y,z so least significant is first
		int index = 0;
		int mult = 1;
		for(int i = 0; i < dimension.Length; i++)
		{
			mult *= dimension[i];
			index += pos[i] * mult;
		}
		return index;
	}

	public IEnumerable<int> Index()
	{
		int size = GetSize();
		for(int i = 0; i < size; i++)
		{
			yield return i;
		}
	}
}
