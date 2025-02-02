using Godot;
using System;
using FileSystem;

public partial class FileTreeTest : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DirectoryTreeFolderNode c = new DirectoryTreeFolderNode(null, "C://");
		c.AddFileNode(new DirectoryTreeFileNode(c, "file.txt", "txt"));
		c.AddFileNode(new DirectoryTreeFileNode(c, "file2.txt", "txt"));
		DirectoryTreeFolderNode folder = new DirectoryTreeFolderNode(c, "Folder");
		folder.AddFileNode(new DirectoryTreeFileNode(folder, "file3.txt", "txt"));
		folder.AddFileNode(new DirectoryTreeFileNode(folder, "file4.txt", "txt"));

		DirectoryTreeFolderNode folder2 = new DirectoryTreeFolderNode(folder, "Folder 2");
		folder2.AddFileNode(new DirectoryTreeFileNode(folder2, "file5.txt", "txt"));
		folder.AddFolderNode(folder2);

		c.AddFolderNode(folder);

		DirectoryTreeFolderNode folder2_cpy = folder.GetCopy();
		folder2_cpy.SetParentAs(c);
		c.AddFolderNode(folder2_cpy);

		DirectoryTree d_tree = new DirectoryTree(c);
		d_tree.Print();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
