using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CA.Utils
{
	internal class StampaUtility
	{
		public static void Lista<T>(List<T> lista)
		{
			foreach (T oggetto in lista)
			{
				if (oggetto is not null)
				{
					foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(oggetto))
					{
						/*
						The following if statement checks if the property type is not assignable from System.Collections.IEnumerable,
						which is a base interface for all non-generic collections, such as arrays, lists, etc.,
						that could be used for navigation properties. It also ensures that string properties
						are not excluded, as string also implements IEnumerable.
						 */
						if (!typeof(System.Collections.IEnumerable).IsAssignableFrom(descriptor.PropertyType) ||
							descriptor.PropertyType == typeof(string))
						{
							string name = descriptor.Name;
							object? value = descriptor.GetValue(oggetto);
							Console.WriteLine($"{name}={value}");
						}
					}
				}
			}
		}
		public static void Oggetto<T>(T oggetto)
		{
			if (oggetto is not null)
			{
				foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(oggetto))
				{
					string name = descriptor.Name;
					object? value = descriptor.GetValue(oggetto);
					Console.WriteLine($"{name}={value}");
				}
			}
		}
	}
}
