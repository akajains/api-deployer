
-- Not sure if these indexes already exist in the PEX tables.
CREATE INDEX IX_PEX_REF_ORG_ouID ON dbo.PEX_REF_ORG (ouID);
CREATE INDEX IX_PEX_REF_ORG_PARENTOuID ON dbo.PEX_REF_ORG (PARENTOuID);
CREATE INDEX IX_PEX_REF_ORG_startDate ON dbo.PEX_REF_ORG (startDate);

CREATE INDEX IX_PEX_REF_ORG_NAME_objid ON dbo.PEX_REF_ORG_NAME ([objid]);
CREATE INDEX IX_PEX_REF_ORG_NAME_startDate ON dbo.PEX_REF_ORG_NAME (startDate);
GO

IF OBJECT_ID('dbo.Employee', 'V') IS NOT NULL
    DROP VIEW dbo.Employee;
GO

CREATE VIEW dbo.Employee AS
SELECT ISNULL(e.empNo, '')AS empNo,         -- id
       e.userID,                    -- userName
       e.fname,                     -- firstName
       e.lname,                     -- lastName
       CAST(NULL AS nvarchar(255)) AS middleName, -- middleName
       e.preferredName,
       LTRIM(RTRIM(ISNULL(e.PreferredName, e.fname) + ' ' + e.lname)) AS FullName,
       e.mainWorkAddressStreet,     -- workStreetHouseNo
       e.mainWorkAddressPostCode,
       e.mainWorkAddresssuburbCity,
       e.mainWorkAddressStateTerritory,
       e.mainWorkAddressCountry,
       e.nominatedMailingAddressStreet,
       e.nominatedMailingAddressPostcode,
       e.nominatedMailingAddressSuburbCity,
       e.nominatedMailingAddressStateTerritory, -- WorkMailingState
       CAST(NULL AS nvarchar(255)) AS nominatedMailingAddressCountry,  -- WorkMailingCountryCode
       e.telephoneNo,                                                  -- WorkTelephoneNo
       e.mobileNo,                                                     -- WorkMobileNo
       e.emailAddress,                                                 -- WorkEmailAddress
       CAST(NULL AS nvarchar(255)) AS homeTelephoneNo,
       CAST(NULL AS nvarchar(255)) AS homeMobileNo,
       e.businessUnit,
       e.personnelArea,                                                -- WorkLocation
       e.personnelSubArea,                                             -- WorkSubLocation
       e.empGroup,                                                     -- Group
       e.empSubGroup,                                                  -- SubGroup
       CAST(NULL AS nvarchar(255)) AS contractType,
       e.employmentInstrument,                                         -- TimesheetScenarioCode
       e.costCentre,
       e.OneUpEmployeeNo,                                              -- ManagerId
       e.positionNo,                                                   -- PositionId
       e.dateCommencedInPosition,                                      -- PositionStartDate
       e.positionTitle,
       e.hrOrgUnit,
       e.jobCode,
       e.workCode,
       e.jobTitle,
       e.band
FROM   dbo.PEX_DAT_SSNSFull e
INNER  JOIN (SELECT empNo, MAX(reportingDate) AS reportingDate
            FROM   dbo.PEX_DAT_SSNSFull
            GROUP BY empNo) e1
ON     e.empNo = e1.empNo
AND    e.reportingDate = e1.reportingDate
WHERE  e.empNo IS NOT NULL;
GO

IF OBJECT_ID('dbo.OrgUnit', 'V') IS NOT NULL
    DROP VIEW dbo.OrgUnit;
GO

CREATE VIEW dbo.OrgUnit AS
SELECT ISNULL(ou.ouID, '') AS ouID, -- Id
       CAST(oun.name AS nvarchar(2000)) AS name, -- Title
       CAST(NULL AS nvarchar(50)) AS costCentre,
       ou.ssuStatus, -- SsuCode
       ou.[pathName], -- NameBasedHierarchy
       ou.pathID,     -- IdBasedHierarchy
       CAST(NULL AS nvarchar(50)) AS managerId,
       ou.PARENTOuID -- AS ParentId
FROM   dbo.PEX_REF_ORG ou
INNER  JOIN (SELECT ouID, MAX(startDate) AS startDate
            FROM   dbo.PEX_REF_ORG
            GROUP BY ouID) ou1
ON     ou.ouID = ou1.ouID
AND    ou.startDate = ou1.startDate
INNER  JOIN dbo.PEX_REF_ORG_NAME oun
ON     ou.ouID = oun.[objid]
INNER  JOIN (SELECT [objid], MAX(startDate) AS startDate
             FROM   dbo.PEX_REF_ORG_NAME
             GROUP  BY [objid]) oun1
ON     oun.[objid] = oun1.[objid]
AND    oun.startDate = oun1.startDate;
GO
