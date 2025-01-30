using Godot;
using System;

namespace FileSystem
{
	public partial class DirectoryTree
	{
		private DirectoryTreeFolderNode root;

		private DirectoryTreeNode clipboardElement;

		public DirectoryTree(DirectoryTreeFolderNode root = null)
		{
			this.root = root;
		}

		public void Print()
		{ 
			String dir = root.Name + "\n";
			GD.Print(dir);
			PrintFolder(root, 1);
		}

		private void PrintFolder(DirectoryTreeFolderNode folder, int depth)
		{
			String spaces = "";
			for(int i = 0; i < depth; ++i) spaces += "  |";

			foreach(DirectoryTreeFolderNode subfolder in folder.Subfolders)
			{
				String dir = spaces + "-" + subfolder.Name + "\n";
				GD.Print(dir);
				PrintFolder(subfolder, depth + 1);
			}

			foreach(DirectoryTreeFileNode file in folder.ContainedFiles)
			{
				String dir = spaces + "-" + file.Name + "\n";
				GD.Print(dir);
			}
		}
	}
}

