using System;
using System.Data;

namespace Cerberus.Module.CMS.Data
{
	public class FolderRepository
	{
		public int AddFolder(Folder folder)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				INSERT INTO
					[Cerberus.CMS.Folder]
					(
						Name,
						SEOName,
						ParentId
					)
				VALUES
					(
						@Name,
						@SEOName,
						@ParentId
					);
				SELECT CAST(SCOPE_IDENTITY() AS INT)";

			SqlDbAccess.AddParameter(command, "@Name", SqlDbType.NVarChar, folder.Name);
			SqlDbAccess.AddParameter(command, "@SEOName", SqlDbType.NVarChar, folder.SEOName);
			SqlDbAccess.AddParameter(command, "@ParentId", SqlDbType.Int, folder.ParentId > 0 ? (object)folder.ParentId : DBNull.Value);

			return SqlDbAccess.ExecuteScalar<int>(command);
		}

		public Folder GetFolder(int folderId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				SELECT
					FolderId,
					Name,
					SEOName,
					ParentId
				FROM
					[Cerberus.CMS.Folder]
				WHERE
					FolderId=@FolderId";

			SqlDbAccess.AddParameter(command, "@FolderId", SqlDbType.Int, folderId);

			var data = SqlDbAccess.ExecuteSelect(command);

			return data.Rows.Count > 0 ? Folder.CreateFromData(data.Rows[0]) : null;
		}

		public FolderCollection GetFolders(int parentFolderId)
		{
			var command = SqlDbAccess.CreateTextCommand();
			var whereFilter = parentFolderId > 0 ? "ParentId=@ParentId" : "ParentId IS NULL";

			command.CommandText = string.Format(@"
				SELECT
					FolderId,
					Name,
					SEOName,
					ParentId
				FROM
					[Cerberus.CMS.Folder]
				WHERE
					{0}", whereFilter);

			SqlDbAccess.AddParameter(command, "@ParentId", SqlDbType.Int, parentFolderId);

			return FolderCollection.CreateFromData(SqlDbAccess.ExecuteSelect(command));
		}

		public bool UpdateFolder(Folder folder)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				UPDATE
					[Cerberus.CMS.Folder]
				SET
					Name=@Name,
					SEOName=@SEOName,
					ParentId=@ParentId
				WHERE
					FolderId=@FolderId";

			SqlDbAccess.AddParameter(command, "@FolderId", SqlDbType.Int, folder.Id);
			SqlDbAccess.AddParameter(command, "@Name", SqlDbType.NVarChar, folder.Name);
			SqlDbAccess.AddParameter(command, "@SEOName", SqlDbType.NVarChar, folder.SEOName);
			SqlDbAccess.AddParameter(command, "@ParentId", SqlDbType.Int, folder.ParentId > 0 ? (object)folder.ParentId : DBNull.Value);

			return SqlDbAccess.ExecuteNonQuery(command) > 0;
		}

		public bool RemoveFolder(int folderId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				DELETE
					[Cerberus.CMS.Folder]
				WHERE
					FolderId=@FolderId";

			SqlDbAccess.AddParameter(command, "@FolderId", SqlDbType.Int, folderId);

			return SqlDbAccess.ExecuteNonQuery(command) > 0;
		}

		public int[] GetFolderHierarchyAsIdArray(int folderId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				WITH FolderHierarchy AS
				(
				   SELECT ParentId, FolderId AS Path
				   FROM [Cerberus.CMS.Folder]
				   WHERE FolderId =  @FolderId
				UNION ALL
				   SELECT 
					 t.ParentId,
					 d.ParentId
				   FROM [Cerberus.CMS.Folder] t
				   INNER JOIN FolderHierarchy AS d
						ON t.FolderId = d.ParentId
				 )

				SELECT DISTINCT Path from FolderHierarchy";

			SqlDbAccess.AddParameter(command, "@FolderId", SqlDbType.Int, folderId);
			
			var data = SqlDbAccess.ExecuteSelect(command);
			var result = new int[data.Rows.Count];
			
			for (var i=0; i<data.Rows.Count; i++)
			{
				result[i] = Convert.ToInt32(data.Rows[i]["Path"]);
			}

			return result;
		}
	}
}
