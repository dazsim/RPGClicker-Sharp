
using System;

/**
*
*	BigInt - Class to handle obscenely big numbers that can't be stored in 64bit integers
*
**/

public class BigInt 
{
	public int[] value;
	public static string[] NumeratorNames = {"Thousand","Million","Billion","Trillion","Quadrillion","Quintillion","Sextillion","Septillion","Octillion","Nonillion","Decillion"};

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
	
	//TODO: make this safe.
	public static string[] toReadableString(int digits = 3, int fraction = 0, bool hasDelimeter = false)
	{
		string v = "";
		// get string value for number
		for(var i=value.length;i!=0;i--)
		{
			v = v + (string)value[i];
		}
		digits = digits - v.length % 3;
		// index for correct numerator name
		int p = (v.length - digits) /3;
		
		// output string
		string o = v.Substring(0,digits);
		//if hasDelimeter is set then we need to add comma's here.
		if (fraction>0)
		{
			o = o + "." + v.SubString(digits,fraction);
		}
		//return result.
		return {o,NumeratorNames[p]};
		
		//example. digits == 3, fraction = 3. value[]=1,250,000,000 : result is {"1.250","Billion"}
		
	}

}
