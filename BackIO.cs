using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Social;
using Terraria.Utilities;

namespace LargerInventory
{
	internal static class BackupIO
	{
		internal static bool IsArchiveOlder(DateTime time, TimeSpan thresholdAge)
		{
			return DateTime.Now - time > thresholdAge;
		}
		internal static string GetArchiveName(string name)
		{
			return name;
		}
		internal static string TodaysBackup(string name)
		{
			return string.Format("{0:yyyy-MM-dd}-{1}-lis.zip", DateTime.Now, GetArchiveName(name));
		}
		internal static DateTime GetTime(string file)
		{
			return Convert.ToDateTime(file.Substring(0, 10));
		}
		internal static void RunArchiving(Action<ZipFile, string> saveAction, string dir, string name, string path)
		{
			try
			{
				Directory.CreateDirectory(dir);
				DeleteOldArchives(dir, name);
				using (ZipFile zipFile = new(Path.Combine(dir, TodaysBackup(name)), Encoding.UTF8))
				{
					zipFile.UseZip64WhenSaving = Zip64Option.AsNecessary;
					zipFile.ZipErrorAction = 0;
					saveAction.Invoke(zipFile, path);
					zipFile.Save();
				}
			}
			catch (Exception ex)
			{
				LargerInventory.Instance.Logger.Error("A problem occurred when trying to create a backup file.", ex);
			}
		}
		internal static void AddZipEntry(this ZipFile zip, string path, bool isCloud = false)
		{
			zip.CompressionMethod = CompressionMethod.Deflate;
			zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level9;
			zip.Comment = string.Format("Archived on ${0} by tModLoader", DateTime.Now);
			if (!isCloud && (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				zip.AddFiles(Directory.GetFiles(path), false, Path.GetFileNameWithoutExtension(path));
			}
			else if (isCloud)
			{
				zip.AddEntry(Path.GetFileName(path), FileUtilities.ReadAllBytes(path, true));
			}
			else
			{
				zip.AddFile(path, "");
			}
		}
		internal static void DeleteOldArchives(string dir, string name)
		{
			string text = Path.Combine(dir, TodaysBackup(name));
			if (File.Exists(text))
			{
				DeleteArchive(text);
			}
			IEnumerable<FileInfo> arg_63_0 = new DirectoryInfo(dir).GetFiles("*" + GetArchiveName(name) + "*.zip", 0);
			Func<FileInfo, DateTime> arg_63_1 = new((f) => GetTime(f.Name));
			FileInfo[] array = Enumerable.ToArray(Enumerable.OrderBy(arg_63_0, arg_63_1));
			FileInfo fileInfo = null;
			FileInfo[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				FileInfo fileInfo2 = array2[i];
				bool flag2 = fileInfo == null;
				if (flag2)
				{
					fileInfo = fileInfo2;
				}
				else
				{
					DateTime time = GetTime(fileInfo2.Name);
					int num = IsArchiveOlder(time, TimeSpan.FromDays(30.0)) ? 30 : (IsArchiveOlder(time, TimeSpan.FromDays(7.0)) ? 7 : 1);
					bool flag3 = (time - GetTime(fileInfo.Name)).Days < num;
					if (flag3)
					{
						DeleteArchive(fileInfo.FullName);
					}
					fileInfo = fileInfo2;
				}
			}
		}
		internal static void DeleteArchive(string path)
		{
			try
			{
				File.Delete(path);
			}
			catch (Exception ex)
			{
				LargerInventory.Instance.Logger.Error("Problem deleting old archive file", ex);
			}
		}
		#region
		/*
        internal static class World
		{
			internal static void ArchiveWorld(string path)
			{
				RunArchiving(new Action<ZipFile, string>(WriteArchive), WorldBackupDir, Path.GetFileNameWithoutExtension(path), path);
			}
			internal static void WriteArchive(ZipFile zip, string path)
			{
				if (File.Exists(path))
				{
					zip.AddZipEntry(path);
				}
				path = Path.ChangeExtension(path, ".twld");
				if (File.Exists(path))
				{
					zip.AddZipEntry(path);
				}
			}
			internal static readonly string WorldDir = Path.Combine(Main.SavePath, "Worlds");
			internal static readonly string WorldBackupDir = Path.Combine(World.WorldDir, "Backups");
		}
		*/
		#endregion
		internal static class Player
		{
			internal static void ArchivePlayer(string path)
			{
				RunArchiving(new Action<ZipFile, string>(WriteArchive), PlayerBackupDir, Path.GetFileNameWithoutExtension(path), path);
			}
			internal static void WriteArchive(ZipFile zip, string path)
			{
				string p1 = path;
				if (File.Exists(p1))
				{
					zip.AddZipEntry(p1);
				}
				string p2 = Path.ChangeExtension(path, ".plr.bak");
				if (File.Exists(p2))
				{
					zip.AddZipEntry(p2);
				}
				string p3 = Path.ChangeExtension(path, ".tplr");
				if (File.Exists(p3))
				{
					zip.AddZipEntry(p3);
				}
				string p4 = Path.ChangeExtension(path, ".tplr.bak");
				if (File.Exists(p4))
				{
					zip.AddZipEntry(p4);
				}
				string p5 = Path.ChangeExtension(path, ".lis");
				if (File.Exists(p5))
				{
					zip.AddZipEntry(p5);
				}
				string p6 = Path.ChangeExtension(path, ".lisb");
				if (File.Exists(p6))
				{
					zip.AddZipEntry(p6);
				}
				WriteLocalFiles(zip, path);
			}
			internal static void WriteCloudFiles(ZipFile zip, string path)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
				path = Path.ChangeExtension(path, "");
				path = path[0..^1];
				IEnumerable<string> enumerable = Enumerable.Where<string>(SocialAPI.Cloud.GetFiles(), (string p) => p.StartsWith(path, StringComparison.CurrentCultureIgnoreCase) && (p.EndsWith(".map", StringComparison.CurrentCultureIgnoreCase) || p.EndsWith(".tmap", StringComparison.CurrentCultureIgnoreCase)));
				using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						zip.AddEntry(fileNameWithoutExtension + "/" + Path.GetFileName(current), FileUtilities.ReadAllBytes(current, true));
					}
				}
			}
			internal static void WriteLocalFiles(ZipFile zip, string path)
			{
				string text = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
				if (Directory.Exists(text))
				{
					zip.AddZipEntry(text, false);
				}
			}
			internal static readonly string PlayerDir = Path.Combine(Main.SavePath, "Players");
			internal static readonly string PlayerBackupDir = Path.Combine(PlayerDir, "Backups");
		}
	}
}