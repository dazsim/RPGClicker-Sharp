
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
	public const int BIT_WIDTH = 63;

	public BigInt(int v)
	{
		value[0] = v;
	}
	
	public BigInt(BigInt v)
	{
		value = v.value;
	}
	
	public override bool Equals(Object obj)
	{
		//Check for null and compare run-time types.
		if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
		{
			return false;
		}
		else
		{
			BigInt i = (BigInt) obj;
			return (this == i);
		}
	
	}
	
	public override int GetHashCode()
	{
		int result = 0;
		for(var i = 0;i < value.Length;i++)
		{
			result = result ^ value[i];
		}
		return result;
	}
	
	public static bool operator ==(BigInt a, BigInt b)
	{
		if (a.value.Length == b.value.Length)
		{
			var result = true;
			for (int i = 0;i <= a.value.Length;i++)
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
	
	public static bool operator !=(BigInt a, BigInt b)
	{
		return !(a == b);
	}
	
	
	
	// TODO : delete when sure will never be needed.
	/*public static BigInt operator +(int a, int b)
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
	}*/
	
	// add integer b to BigInt. If offset is not 0, add integer at value[offset] (helper functionality)
	
	public static BigInt operator +(BigInt a, int b)
	{
		a = a.AddBigIntToInt(a,b);
		return a;
	}
	
	public BigInt AddBigIntToInt(BigInt a, int b, int offset = 0)
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
			c = Math.Abs(a.value[offset]-b);
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
		a.value[offset] = c;
		return a;
	}
	
	// add 2 BigInt together.
	public static BigInt operator +(BigInt a, BigInt b)
	{
		for (var i=0;i<a.value.Length;i++)
		{
			// this should work natively using the BigInt/int operator overload
			
			b = b.AddBigIntToInt(b,a.value[i],i);
		}
		return b;
		
	}
	
	// operator +(BigInt a)
	public static BigInt operator +(BigInt a)
	{
		return a;
	}
	
	
	
	// operator -(BigInt a)
	
	public static BigInt operator -(BigInt a)
	{
		a.value[0] = -a.value[0];
		return a;
	}
	
	public static BigInt operator -(BigInt a, BigInt b)
	{
		return a + (-b);
	}
	
	public static BigInt operator -(BigInt a, int b)
	{
		return a + (-b);
	}
	
	// operator >>
	
	public static BigInt operator >>(BigInt a, int b)
	{
		// a = 100. b = 5
		// 0001 1111 0100
		// 0001 1111 0000
		// 0001 1111 0111
		// 0001 1111 1111
		// 0000 0000 1111
		if (a.value.Length == 1)
		{
			// shortcut
			if (b >= BIT_WIDTH)
			{
				return new BigInt(0);
			} else
			{
				
				a.value[0] = a.value[0] >> b;
				return a;
			}
		} else
		{
			if (b > a.value.Length * BIT_WIDTH)
			{
				return new BigInt(0);
			} 
			// multiple integers need shifting.
			BigInt c = new BigInt(0);
			int destination_offset = 0;
			int source_offset = 0;
			int source_index = 0;
			int tmp = 0;
			for (var i = 0; i < a.value.Length;i++)
			{
				//loop through source array
				//a.val[i] SHR b
				c.value[i] =a.value[i] >> b;
				
				// if b >=BIT_WIDTH, starting with a clean copy
				if (b >= BIT_WIDTH)
				{
					destination_offset = 0;
				}
				else {
					destination_offset = BIT_WIDTH - b;
				}
				if (source_index > a.value.Length)
				{
					continue;
				}
				source_offset = b % BIT_WIDTH;
				source_index = (b-source_offset) / BIT_WIDTH;
				tmp = a.value[source_index];
				tmp = tmp >> source_offset;
				tmp = tmp << destination_offset;
				c.value[i] += tmp;
				if (source_offset + destination_offset < BIT_WIDTH)
				{
					if (source_index+1 > a.value.Length)
					{
						continue;
					}
					// copy remainder of data from next index.
					tmp = a.value[source_index + 1];
					tmp = tmp << BIT_WIDTH - (source_offset + destination_offset);
					c.value[i] += tmp;
				}
				
			}
			return c;
		}
		// TODO : harden this to handle passing in null BigInt
		
	}
	
	
	// operator <<
	
	public static BigInt operator <<(BigInt a, int b)
	{
		return a;
	}
	
	
	
	// operator *
	
	// operator /
	
	// operator a ^ y
	
	// operator >
	
	// operator <
	
	// operator >=
	
	// operator <=
	
	// operator ++
	
	// operator --
	
	// operator %
	
	// operator &
	
	// operator |
	
	
	
	
	
	
	
	
	//TODO: make this safe.
	//public string[] toReadableString(int digits = 3, int fraction = 0, bool hasDelimeter = false, bool shortHand = false)
	public string[] toReadableString(int digits = 3, int fraction = 0, bool hasDelimeter = false)
	{
		string v = "";
		// get string value for number
		for(var i=value.Length;i!=0;i--)
		{
			v = v + (string)value[i].ToString();
		}
		digits = digits - v.Length % 3;
		// index for correct numerator name
		int p = (v.Length - digits) /3;
		
		// output string
		string o = v.Substring(0,digits);
		//if hasDelimeter is set then we need to add comma's here.
		if (fraction>0)
		{
			//o = o + "." + v.SubString(digits,fraction);
		}
		//return result.
		string[] result = {o,NumeratorNames[p]};
		//result[0] = o;
		//result[1] = NumeratorNames[p];
		
		return result;
		
		//example. digits == 3, fraction = 3. value[]=1,250,000,000 : result is {"1.250","Billion"}
		
		
		
	}

}
