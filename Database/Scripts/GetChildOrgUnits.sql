CREATE PROCEDURE [dbo].[GetChildOrgUnits] (@OrgUnitId [nvarchar](50))
AS
BEGIN

    SET NOCOUNT ON;

    WITH ChildOrgUnits (PARENTOuID, ouID, name, pathID, Level)
    AS
    (
    -- Anchor member definition
        SELECT ou.PARENTOuID, ou.ouID, ou.name, ou.pathID, 0 AS Level
        FROM   dbo.OrgUnit AS ou
        WHERE  ou.ouID = @OrgUnitId
        UNION ALL
    -- Recursive member definition
        SELECT ou.PARENTOuID, ou.ouID, ou.name, ou.pathID, Level + 1
        FROM   dbo.OrgUnit AS ou
        INNER  JOIN ChildOrgUnits AS c
        ON     ou.PARENTOuID = c.ouID
    )
    -- Statement that executes the CTE
    SELECT PARENTOuID AS ParentId, ouID AS Id, name AS Title, pathID AS IdBasedHierarchy, Level
    FROM   ChildOrgUnits;

    SET NOCOUNT OFF;

END;
GO

/*
DECLARE @OrgUnitId nvarchar(50) = N'90041159';
EXECUTE dbo.GetChildOrgUnits @OrgUnitId;
GO
*/
