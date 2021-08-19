
using System;

/**
*
*	BigInt - Class to handle obscenely big numbers that can't be stored in 64bit integers
*
**/

public class BigInt 
{
	public int[] value;

	public BigInt(int v)
	{
		value[0] = v;
	}
	
	public BigInt(BigInt v)
	{
		value = v.value;
	}
	
	public static BigInt operator +(int a, int b)
	{
		int c = 0;
		int d = 0;
		try
		{
			c =  checked(a+b)
		}
		catch (System.OverflowException e)
		{
			c = abs(a-b);
			d = 1;
		}
		if (d > 0)
		{
			value[1]=d;
		}
		value[0] = c;
	}
	
	public static BigInt operator +(BigInt a, int b)
	{
		int c = 0;
		int d = 0;
		int oo = 1;
		
		// try adding value[0] to b
		
		try
		{
			c =  checked(a.value[0]+b);
		}
		catch (System.OverflowException e)
		{
			c = abs(a.value[0]-b);
			d = 1;
		}
		if (d > 0)
		{
			// if it overflows, trigger overflow loop until it doesn't overflow.
			// while a.value[oo] + d <0
			while (a.value[oo] + d < 0)
			{
				a.value[oo] = 0;
				oo++;
			}
			// increment value[oo];
			a.value[oo] += d;
		}
		value[0] = c;
	}

}
