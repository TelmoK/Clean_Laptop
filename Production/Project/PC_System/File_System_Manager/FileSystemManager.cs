using Godot;
using System;
using System.Collections.Generic;

namespace FileSystem
{
	public class FileSystemManager
	{
		private DirectoryTree directoryTree;

		private DirectoryTreeFolderNode currentDirectory;

		private List<DirectoryTreeFileNode> clipboardFiles;

		private List<DirectoryTreeNode> clipboardFolders;

		private bool clipboardCut = false; // Are the elements going to be moved or copied

		public FileSystemManager(DirectoryTree directoryTree)
		{
			this.directoryTree = directoryTree;
		}

		public void SetDirectoryTreeReference(DirectoryTree directoryTree)
		{
			this.directoryTree = directoryTree;
		}

		public void SetClipboardCut(bool value)
		{
			clipboardCut = value;
		}

		public void ClipboardElement(DirectoryTreeFileNode file)
		{
			clipboardFiles.Add(file);	
		}

		public void ClipboardElement(DirectoryTreeFolderNode folder)
		{
			clipboardFolders.Add(folder);	
		}

		public void ClearClipboard()
		{
			clipboardFiles.Clear();
			clipboardFolders.Clear();
		}

		public bool/*Status*/ TryPasteElementIn(DirectoryTreeFileNode file, DirectoryTreeFolderNode destinyFolder)
		{
			// if(root contains .zip extension)
			// 	   return false; // Status.ElementIsCompressed;

			if(new List<DirectoryTreeFileNode>(destinyFolder.ContainedFiles).Exists(f => f.Name == file.Name && f.Type == file.Type))
			{
				return false; // Status.FileNameExists
			}

			destinyFolder.AddFileNode(file);

			return true;
		}

		public bool/*Status*/ TryPasteElementIn(DirectoryTreeFolderNode folder, DirectoryTreeFolderNode destinyFolder)
		{
			// if(root contains .zip extension)
			// 	   return false; // Status.ElementIsCompressed;

			if(new List<DirectoryTreeFolderNode>(destinyFolder.Subfolders).Exists(f => f.Name == folder.Name))
			{
				return false; // Status.FolderNameExists
			}

			destinyFolder.AddFolderNode(folder);

			return true;
		}

		public bool TryRenameElementIn(DirectoryTreeFileNode file, String newName, DirectoryTreeFolderNode containerFolder)
		{
			if(new List<DirectoryTreeFileNode>(containerFolder.ContainedFiles).Exists(f => f.Name == newName && f.Type == file.Type))
			{
				return false; // Status.FileNameExists
			}

			file.SetName(newName);

			return true;
		}

		public bool TryRenameElementIn(DirectoryTreeFolderNode folder, String newName, DirectoryTreeFolderNode containerFolder)
		{
			if(new List<DirectoryTreeFolderNode>(containerFolder.Subfolders).Exists(f => f.Name == newName))
			{
				return false; // Status.FolderNameExists
			}

			folder.SetName(newName);

			return true;
		}

		private bool TryDeleteElementIn(DirectoryTreeFolderNode folder, DirectoryTreeFolderNode containerFolder)
		{
			if(false/*Folder is opened*/) return false;

			folder.SetParentAs(null);

			foreach(DirectoryTreeFileNode file in folder.ContainedFiles)
			{
				TryDeleteElement(file);
			}

			foreach(DirectoryTreeFolderNode subfolder in folder.Subfolders)
			{
				TryDeleteElementIn(subfolder, folder);
			}

			return true;
		}

		private bool TryDeleteElement(DirectoryTreeFileNode file)
		{
			if(false/*File is opened*/) return false;

			file.SetParentAs(null);

			return true;
		}
	}
}
