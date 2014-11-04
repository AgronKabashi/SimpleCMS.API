using Cerberus.Module.CMS.Common;
using System;
using System.Data;

namespace Cerberus.Module.CMS.Data
{
	public class ArticleRepository
	{
		public int AddArticle(Article article)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				DECLARE @ArticleId INT;
				INSERT INTO
					[Cerberus.CMS.Article]
					(
						Name,
						Description,
						SEOName,
						FolderId,
						TemplateId,
						Image,
						Thumbnail
					)
				VALUES
					(
						@Name,
						@Description,
						@SEOName,
						@FolderId,
						@TemplateId,
						@Image,
						@Thumbnail
					);

				SET @ArticleId = CAST(SCOPE_IDENTITY() AS INT)

				INSERT INTO
					[Cerberus.CMS.ArticlePublication]
					(
						ArticleId,
						IsPublished,
						PublishStartDate,
						PublishEndDate
					)
				VALUES
					(
						@ArticleId,
						@IsPublished,
						@PublishStartDate,
						@PublishEndDate
					)

				SELECT @ArticleId";

			SqlDbAccess.AddParameter(command, "@Name", SqlDbType.NVarChar, article.Name);
			SqlDbAccess.AddParameter(command, "@Description", SqlDbType.NVarChar, article.Description);
			SqlDbAccess.AddParameter(command, "@SEOName", SqlDbType.NVarChar, article.SEOName);
			SqlDbAccess.AddParameter(command, "@FolderId", SqlDbType.Int, article.FolderId == 0 ? (object)DBNull.Value : (object)article.FolderId);
			SqlDbAccess.AddParameter(command, "@TemplateId", SqlDbType.Int, article.TemplateId);
			SqlDbAccess.AddParameter(command, "@Image", SqlDbType.NVarChar, article.Image);
			SqlDbAccess.AddParameter(command, "@Thumbnail", SqlDbType.NVarChar, article.Thumbnail);

			SqlDbAccess.AddParameter(command, "@IsPublished", SqlDbType.Bit, article.Publication.IsPublished);
			//SqlDbAccess.AddParameter(command, "@PublishStartDate", SqlDbType.DateTime, article.Publication.PublishStartDate);
			//SqlDbAccess.AddParameter(command, "@PublishEndDate", SqlDbType.DateTime, article.Publication.PublishEndDate);
			SqlDbAccess.AddParameter(command, "@PublishStartDate", SqlDbType.DateTime, new DateTime(1901, 1, 1));
			SqlDbAccess.AddParameter(command, "@PublishEndDate", SqlDbType.DateTime, new DateTime(2050, 1, 1));

			return SqlDbAccess.ExecuteScalar<int>(command);
		}

		public Article GetArticle(int articleId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				SELECT
					A.ArticleId,
					A.Name,
					A.Description,
					A.SEOName,
					A.FolderId,
					A.TemplateId,
					A.Image,
					A.Thumbnail,
					AP.IsPublished,
					AP.PublishStartDate,
					AP.PublishEndDate
				FROM
					[Cerberus.CMS.Article] A
					JOIN [Cerberus.CMS.ArticlePublication] AP ON A.ArticleId=AP.ArticleId
				WHERE
					A.ArticleId=@ArticleId";

			SqlDbAccess.AddParameter(command, "@ArticleId", SqlDbType.Int, articleId);

			var data = SqlDbAccess.ExecuteSelect(command);

			return data.Rows.Count > 0 ? Article.CreateFromData(data.Rows[0]) : null;
		}

		public ArticleCollection GetArticles(int folderId, ArticleSearchOptions searchOptions)
		{
			var command = SqlDbAccess.CreateTextCommand();

			var folderQuery = "FolderId IS NULL";
			if (folderId > 0)
			{
				folderQuery = "FolderId=@FolderId";
				SqlDbAccess.AddParameter(command, "@FolderId", SqlDbType.Int, folderId);
			}

			var publicationQuery = string.Empty;
			if (searchOptions.Publication != PublicationType.All)
			{
				publicationQuery = string.Format(@"
						AND AP.IsPublished=@IsPublished
						{0}",
					searchOptions.Publication == PublicationType.Published ? "AND AP.PublishStartDate <= GetDate() AND AP.PublishEndDate > GetDate()" : string.Empty);
					
				SqlDbAccess.AddParameter(command, "@IsPublished", SqlDbType.Int, (int)searchOptions.Publication);
			}

			command.CommandText = string.Format(@"
				SELECT
					A.ArticleId,
					A.Name,
					A.Description,
					A.SEOName,
					A.FolderId,
					A.TemplateId,
					A.Image,
					A.Thumbnail,
					AP.IsPublished,
					AP.PublishStartDate,
					AP.PublishEndDate
				FROM
					[Cerberus.CMS.Article] A
					JOIN [Cerberus.CMS.ArticlePublication] AP ON A.ArticleId = AP.ArticleId
						{0}
				WHERE
					{1}
				ORDER BY {2} {3}",
					publicationQuery,
					folderQuery,
					searchOptions.OrderBy,
					searchOptions.OrderByDirection);

			return ArticleCollection.CreateFromData(SqlDbAccess.ExecuteSelect(command));
		}

		public bool UpdateArticle(Article article)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				UPDATE
					[Cerberus.CMS.Article]
				SET
					Name=@Name,
					Description=@Description,
					SEOName=@SEOName,
					Image=@Image,
					Thumbnail=@Thumbnail
				WHERE
					ArticleId=@ArticleId;
					
				UPDATE
					[Cerberus.CMS.ArticlePublication]
				SET
					IsPublished=@IsPublished,
					PublishStartDate=@PublishStartDate,
					PublishEndDate=@PublishEndDate
				WHERE
					ArticleId=@ArticleId";

			SqlDbAccess.AddParameter(command, "@ArticleId", SqlDbType.Int, article.Id);
			SqlDbAccess.AddParameter(command, "@Name", SqlDbType.NVarChar, article.Name);
			SqlDbAccess.AddParameter(command, "@Description", SqlDbType.NVarChar, article.Description);
			SqlDbAccess.AddParameter(command, "@SEOName", SqlDbType.NVarChar, article.SEOName);
			SqlDbAccess.AddParameter(command, "@Image", SqlDbType.NVarChar, article.Image);
			SqlDbAccess.AddParameter(command, "@Thumbnail", SqlDbType.NVarChar, article.Thumbnail);

			SqlDbAccess.AddParameter(command, "@IsPublished", SqlDbType.Bit, article.Publication.IsPublished);
			//SqlDbAccess.AddParameter(command, "@PublishStartDate", SqlDbType.DateTime, article.Publication.PublishStartDate);
			//SqlDbAccess.AddParameter(command, "@PublishEndDate", SqlDbType.DateTime, article.Publication.PublishEndDate);
			SqlDbAccess.AddParameter(command, "@PublishStartDate", SqlDbType.DateTime, new DateTime(1901, 1, 1));
			SqlDbAccess.AddParameter(command, "@PublishEndDate", SqlDbType.DateTime, new DateTime(2050, 1, 1));

			return SqlDbAccess.ExecuteNonQuery(command) > 0;
		}

		public bool RemoveArticle(int articleId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				DELETE
					[Cerberus.CMS.Article]
				WHERE
					ArticleId=@ArticleId";

			SqlDbAccess.AddParameter(command, "@ArticleId", SqlDbType.Int, articleId);

			return SqlDbAccess.ExecuteNonQuery(command) > 0;
		}

		public Article GetStartPage()
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				SELECT TOP 1
					A.ArticleId,
					A.Name,
					A.Description,
					A.SEOName,
					A.FolderId,
					A.TemplateId,
					A.Image,
					A.Thumbnail,
					AP.IsPublished,
					AP.PublishStartDate,
					AP.PublishEndDate
				FROM
					[Cerberus.CMS.Article] A
					JOIN [Cerberus.CMS.ArticlePublication] AP ON A.ArticleId=AP.ArticleId
				WHERE
					FolderId IS NULL
					AND AP.IsPublished=1
					AND AP.PublishStartDate <= GetDate() 
					AND AP.PublishEndDate > GetDate()";

			var data = SqlDbAccess.ExecuteSelect(command);

			return data.Rows.Count > 0 ? Article.CreateFromData(data.Rows[0]) : null;
		}

		public int CloneArticle(int articleId)
		{
			var command = SqlDbAccess.CreateTextCommand();

			command.CommandText = @"
				INSERT INTO
					[Cerberus.CMS.Article]
					(
						Name,
						Description,
						SEOName,
						FolderId,
						TemplateId,
						Image,
						Thumbnail
					)
				SELECT
					Name,
					Description,
					SEOName,
					FolderId,
					TemplateId,
					Image,
					Thumbnail
				FROM 
					[Cerberus.CMS.Article]
				WHERE
					ArticleId = @ArticleId;

				DECLARE @NewArticleId INT;
				SET @NewArticleId = SCOPE_IDENTITY();

				INSERT INTO
					[Cerberus.CMS.ArticlePublication]
					(
						ArticleId,
						IsPublished,
						PublishStartDate,
						PublishEndDate
					)
				SELECT
					@NewArticleId,
					IsPublished,
					PublishStartDate,
					PublishEndDate
				FROM
					[Cerberus.CMS.ArticlePublication]
				WHERE
					ArticleId = @ArticleId;

				SELECT @NewArticleId;";

			SqlDbAccess.AddParameter(command, "@ArticleId", SqlDbType.Int, articleId);

			return SqlDbAccess.ExecuteScalar<int>(command);
		}
	}
}
