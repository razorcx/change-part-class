using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace ChangePartClass
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				ChangePartClass.Run();
			}
			catch(Exception ex)
			{
				Trace.WriteLine(ex.InnerException + ex.Message + ex.StackTrace);
			}
		}

		public static class ChangePartClass
		{
			private static readonly Dictionary<string, string> NameClassDictionary =
				new Dictionary<string, string>()
				{
					{ "BEAM", "3" },
					{ "COLUMN", "7" },
					{ "JOIST", "11" },
					{ "GIRT", "5" },
					{ "BRACE", "4" },
				};

			public static void Run()
			{
				var pickedObjects = new Picker()
					.PickObjects(Picker.PickObjectsEnum.PICK_N_PARTS);
				if (pickedObjects.GetSize() < 1) return;

				while (pickedObjects.MoveNext())
				{
					var part = pickedObjects.Current as Part;
					if(part == null)
						continue;

					var partName = part.Name.ToUpper();

					if (NameClassDictionary.ContainsKey(partName))
					{
						part.Class = NameClassDictionary[partName];
						part.Modify();
					}
				}

				new Model().CommitChanges();
			}
		}
	}
}
