#include <iostream>
#include <vector>
#include <list>

class DirectoryTreeNode
{
protected:
	std::string name;
	std::string root;

public:

	DirectoryTreeNode* parent;
	DirectoryTreeNode(DirectoryTreeNode* _parent = nullptr, std::string _name = "") : parent(_parent), name(_name)
	{
		update_root_path();
	}

	DirectoryTreeNode* get_parenth() { return parent; }
	std::string get_root() const { return root; }
	std::string get_name() const { return name; }

	void update_root_path()
	{
		if(parent != nullptr)
			root = parent->root + "/" + parent->name;
	}

	virtual DirectoryTreeNode* get_copy() = 0;
};

class DirectoryTreeFolderNode : public DirectoryTreeNode
{
	std::list<DirectoryTreeNode*> folders;
	std::list<DirectoryTreeNode*> files;

public:

	DirectoryTreeFolderNode(DirectoryTreeNode* _parent = nullptr, std::string _name = "New folder") 
		: DirectoryTreeNode(_parent, _name) {}

	DirectoryTreeNode* get_copy() override 
	{
		DirectoryTreeFolderNode* copy = new DirectoryTreeFolderNode(nullptr, "cpy-"+name);

		std::list<DirectoryTreeNode*> cpy_folder_sons;

		for(DirectoryTreeNode* folder : folders)
			cpy_folder_sons.push_back(folder->get_copy());

		std::list<DirectoryTreeNode*> cpy_file_sons;

		for(DirectoryTreeNode* file : files)
			cpy_file_sons.push_back(file->get_copy());

		copy->folders = cpy_folder_sons;
		copy->files = cpy_file_sons;

		return copy;
	}

	const std::list<DirectoryTreeNode*>& get_folders() const
	{
		return folders;
	}

	const std::list<DirectoryTreeNode*>& get_files() const
	{
		return files;
	}

	void addFolder(DirectoryTreeNode* folder)
	{
		folders.push_back(folder);
	}

	void addFile(DirectoryTreeNode* file)
	{
		files.push_back(file);
	}
};

class DirectoryTreeFileNode : public DirectoryTreeNode
{
	std::string file_content;
	std::string file_type;

public:

	DirectoryTreeFileNode(DirectoryTreeNode* _parent = nullptr, std::string _name = "New File", std::string _file_type = "blank") 
		: DirectoryTreeNode(_parent, _name), file_type(_file_type) {}

	DirectoryTreeNode* get_copy() override
	{
		DirectoryTreeFileNode* copy = new DirectoryTreeFileNode(nullptr, "cpy-"+name, file_type);
		copy->file_content = file_content;

		return copy;
	}

	std::string get_file_type() { return file_type; }
	
};

class DirectoryTree
{
	DirectoryTreeFolderNode* root;

	DirectoryTreeNode* clipboard_dir_node;

public:

	DirectoryTree(DirectoryTreeFolderNode* _root = nullptr) : root(_root) {}

	void print(std::ostream& out)
	{
		out << root->get_name() << "\n";
		print_folder(out, root, 1);
	}

private:

	void print_folder(std::ostream& out, const DirectoryTreeFolderNode* folder, int depth)
	{
		std::string spaces = "";
		for(int i = 0; i < depth; ++i) spaces += "  |";

		for(const DirectoryTreeNode* subfolder : folder->get_folders())
		{
			out << spaces << "-" << subfolder->get_name() << "\n";
			const DirectoryTreeFolderNode* f = dynamic_cast<const DirectoryTreeFolderNode*>(subfolder);
			print_folder(out, f, depth + 1);
		}

		for(const DirectoryTreeNode* file : folder->get_files())
		{
			out << spaces << "-" << file->get_name() << "\n";
		}
	}
};

int main()
{
	DirectoryTreeFolderNode* c = new DirectoryTreeFolderNode(nullptr, "C://");
	c->addFile(new DirectoryTreeFileNode(c, "file.txt", "txt"));
	c->addFile(new DirectoryTreeFileNode(c, "file2.txt", "txt"));
	DirectoryTreeFolderNode* folder = new DirectoryTreeFolderNode(c, "Folder");
	folder->addFile(new DirectoryTreeFileNode(folder, "file3.txt", "txt"));
	folder->addFile(new DirectoryTreeFileNode(folder, "file4.txt", "txt"));

	DirectoryTreeFolderNode* folder2 = new DirectoryTreeFolderNode(folder, "Folder 2");
	folder2->addFile(new DirectoryTreeFileNode(folder2, "file5.txt", "txt"));
	folder->addFolder(folder2);

	c->addFolder(folder);

	DirectoryTreeNode* folder2_cpy = folder->get_copy();
	folder2_cpy->parent = c;
	c->addFolder(folder2_cpy);

	DirectoryTree d_tree(c);
	d_tree.print(std::cout);

	std::cin.get();
}