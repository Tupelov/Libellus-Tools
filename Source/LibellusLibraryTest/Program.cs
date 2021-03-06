﻿#define P3

using System;
using LibellusLibrary.PmdFile;
using LibellusLibrary.PmdFile.Common;
using System.IO;
using System.Collections.Generic;
using LibellusLibrary.PmdFile.DataTypes;
using System.Linq;

namespace LibellusLibraryTest
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("Starting Test!");

			if (Directory.Exists("./output"))
			{
				Directory.Delete("./output",true);
			}
			Directory.CreateDirectory("./output");


			//Yes I know I hardcoded it to my computer.
#if P3
			string eventFolder = "F:/Modding/Persona Modding/Persona 3/Files/data/EVENT";
#elif P4G
			string eventFolder = "F:/Modding/Persona Modding/Persona 4 Golden/Files/Data-cpk-p4g/data.cpk_unpacked/event";
#endif
			string[] files = Directory.GetFiles(eventFolder, "*.pm*", SearchOption.AllDirectories);
			//searchForObject(files);

			string testFilePath = "F:/Modding/Persona Modding/Persona 3/Files/data/EVENT/E400/E401_004.PM2";
			TestConversion(testFilePath);

			Console.WriteLine("Finished tests, exiting.");

		}

		static void TestConversion(string filePath)
		{
			Console.WriteLine("Testing: {0}", filePath);
			PmdFile pmdFile = new(filePath);
			string name = Path.GetFileName(filePath);
			File.Copy(filePath, "./output/" + name);
			pmdFile.Save("./output/" + name + ".lib" + Path.GetExtension(filePath));
		}

		static void searchForObject(string[] files)
		{
			using StreamWriter log = new(File.OpenWrite("output.txt"));

			foreach (string pmd in files)
			{
				PmdFile pmdFile = new(pmd);
				
				List<PmdTypeTable> typeTable = pmdFile.TypeTable;

				foreach (var type in typeTable.Where(x => x.Type == DataTypeID.Frame))
				{
					foreach (PmdDataFrame frame in type.DataTable)
					{
						if (frame.ObjectType == FrameObjectType.B_UP)
						{
							Console.WriteLine("Found use of B_UP!\ninside file: {0}", pmd);
							log.WriteLine("Found use of B_UP!\ninside file: {0}", pmd);
						}
					}
				}
			}

			log.Close();
		}
		
	}
}
