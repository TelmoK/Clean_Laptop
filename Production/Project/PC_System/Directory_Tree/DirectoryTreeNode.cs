using Godot;
using System;

namespace FileSystem
{
	public abstract class DirectoryTreeNode
	{
		protected String name;

		public String Name
		{
			get => name;
		}

		protected String root;

		public String Root
		{
			get => root;
		}

		protected DirectoryTreeNode parent;

		public DirectoryTreeNode Parent
		{
			get => parent;
		}

		public void SetParentAs(DirectoryTreeNode parent)
		{
			if(parent == this.parent) return;
			
			this.parent = parent;
			UpdateRootPath();
		}

		public DirectoryTreeNode(DirectoryTreeNode parent = null, String name = "-")
		{
			this.parent = parent;
			this.name = name;

			UpdateRootPath();
		}

		public void UpdateRootPath()
		{
			if (parent == null) return;

			root = parent.Root + "\\" + parent.Name;
		}

		public abstract DirectoryTreeNode GetCopy();
	}
}

