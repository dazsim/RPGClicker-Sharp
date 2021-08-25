
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
	
	public static boolean operator =(BigInt a, BigInt b)
	{
		if (a.value.length == b.value.length)
		{
			var result = true;
			for (var i = 0;i = a.value.length;i++)
			{
				if (a.value[i] != b.value[i])
				{
					result = false;
				}
			}
			return result;
		} else
		{
			return false;
		}
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
	
	// add integer b to BigInt. If offset is not 0, add integer at value[offset] (helper functionality)
	public static BigInt operator +(BigInt a, int b, int offset = 0)
	{
		int c = 0;
		int d = 0;
		int oo = 1;
		
		// try adding value[offset] to b
		
		try
		{
			c =  checked(a.value[offset]+b);
		}
		catch (System.OverflowException e)
		{
			c = abs(a.value[offset]-b);
			d = 1;
		}
		if (d > 0)
		{
			// if it overflows, trigger overflow loop until it doesn't overflow.
			// while a.value[oo] + d <0
			while (a.value[oo+offset] + d < 0)
			{
				a.value[oo+offset] = 0;
				oo++;
			}
			// increment value[oo];
			a.value[oo+offset] += d;
		}
		value[offset] = c;
	}
	
	// add 2 BigInt together.
	public static BigInt operator +(BigInt a, BigInt b)
	{
		for (var i=0;i<a.value.length;i++)
		{
			// this should work natively using the BigInt/int operator overload
			b = b+a.value[i];
		}
		
	}
	
	//TODO: make this safe.
	public static string[] toReadableString(int digits = 3, int fraction = 0, bool hasDelimeter = false, shortHand = false)
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
