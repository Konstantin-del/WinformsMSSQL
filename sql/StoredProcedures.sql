
CREATE PROCEDURE dbo.usp_status_list
AS
BEGIN
  SET NOCOUNT ON;

  SELECT [id], [name]
  FROM dbo.[status]
  ORDER BY [name];
END
GO


CREATE PROCEDURE dbo.usp_deps_list
AS
BEGIN
  SET NOCOUNT ON;

  SELECT [id], [name]
  FROM dbo.[deps]
  ORDER BY [name];
END
GO


CREATE PROCEDURE dbo.usp_posts_list
AS
BEGIN
  SET NOCOUNT ON;

  SELECT [id], [name]
  FROM dbo.[posts]
  ORDER BY [name];
END
GO


ALTER PROCEDURE dbo.usp_persons_search
  @status          INT = NULL,
  @id_dep          INT = NULL,
  @id_post         INT = NULL,
  @date_from       DATETIME = NULL,
  @date_to         DATETIME = NULL,
  @last_name_like  VARCHAR(100) = NULL
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    p.[id],
    p.[first_name],
    p.[second_name],
    p.[last_name],
    p.[date_employ],
    p.[date_uneploy],    
    s.[name],
    d.[name],
    po.[name],      
    CONCAT(p.[last_name], ' ', p.[first_name], ' ', p.[second_name]) AS [full_name]
  FROM dbo.[persons] p
  INNER JOIN dbo.[status] s ON s.[id] = p.[status_id]
  INNER JOIN dbo.[deps] d   ON d.[id] = p.[dep_id]
  INNER JOIN dbo.[posts] po ON po.[id] = p.[post_id]
  WHERE
    (@status IS NULL OR p.[status_id] = @status)
    AND (@id_dep IS NULL OR p.[dep_id] = @id_dep)
    AND (@id_post IS NULL OR p.[post_id] = @id_post)
    AND (@date_from IS NULL OR p.[date_employ] >= @date_from)
    AND (
      @date_to IS NULL OR p.[date_employ] < DATEADD(DAY, 1, @date_to)
    )
    AND (
      @last_name_like IS NULL OR p.[last_name] LIKE '%' + @last_name_like + '%'
    )
  ORDER BY p.[last_name], p.[first_name], p.[second_name];
END
GO

 



