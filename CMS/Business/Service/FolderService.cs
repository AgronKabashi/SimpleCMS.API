using Cerberus.Module.CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus.Module.CMS.Business
{
	public class FolderService
	{
		internal bool AddFolder(Folder folder)
		{
			folder.Id = DataAccess.FolderRepository.AddFolder(folder.CreateDataObject());
			return folder.Id > 0;
		}

		public Folder GetFolder(int folderId)
		{
			var folder = DataAccess.FolderRepository.GetFolder(folderId);
			return folder != null ? Folder.CreateFromDataObject(folder) : null;
		}

		public FolderCollection GetFolders(int parentFolderId)
		{
			return FolderCollection.CreateFromDataObjectCollection(DataAccess.FolderRepository.GetFolders(parentFolderId));
		}

		internal bool UpdateFolder(Folder folder)
		{
			return DataAccess.FolderRepository.UpdateFolder(folder.CreateDataObject());
		}

		public bool RemoveFolder(int folderId)
		{
			return DataAccess.FolderRepository.RemoveFolder(folderId);
		}

		public int[] GetFolderHierarchyAsIdArray(int folderId)
		{
			return folderId > 0 ? DataAccess.FolderRepository.GetFolderHierarchyAsIdArray(folderId) : new int[0];
		}
	}
}
