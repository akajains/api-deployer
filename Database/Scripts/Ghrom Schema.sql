--
-- Clean up: Drop all tables and start again
--

/*
DROP TABLE dbo.Ghrom_Recruitment_Direct_External;
DROP TABLE dbo.Ghrom_Recruitment_Direct_Employee;
DROP TABLE dbo.Ghrom_Recruitment_Request;
DROP TABLE dbo.Ghrom_Voluntary_Redundancy_Request;
DROP TABLE dbo.Ghrom_Remuneration_Increase_Request;
DROP TABLE dbo.Ghrom_Approval;
DROP TABLE dbo.Ghrom_Org_Unit_Change_Request_Item;
DROP TABLE dbo.Ghrom_Org_Unit_Change_Request;
DROP TABLE dbo.Ghrom_Position_Change_Request_Item;
DROP TABLE dbo.Ghrom_Position_Change_Request;
*/

--
-- Request: template for common fields in request tables only (do not actually create)
--
/*
CREATE TABLE [Ghrom_Request] (
  [Id]                    [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Requester_Id]          [uniqueidentifier]  NOT NULL, -- FK to Employee Id?
  [Requester_Full_Name]   [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Email]       [nvarchar](255)     NOT NULL, -- Check field name/type
  [Status]                [nvarchar](50)      NOT NULL,
  [Status_Comment]        [nvarchar](255)     NULL,
  [Tracked_By_Ticket_Id]  [nvarchar](50)      NOT NULL, -- From ZenDesk?
  [Tracked_By_Ticket_Uri] [nvarchar](255)     NOT NULL, -- From ZenDesk?
  [Created_On]            [datetimeoffset](7) NOT NULL,
  [Created_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Updated_On]            [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Enabled]               [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Request] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Request] ADD CONSTRAINT [DF_Ghrom_Request_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO
*/

--
-- Ghrom_Position_Change_Request
--

CREATE TABLE [Ghrom_Position_Change_Request] (
  [Id]                    [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Requester_Id]          [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Requester_Full_Name]   [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Email]       [nvarchar](255)     NOT NULL, -- Check field name/type
  [Status]                [nvarchar](50)      NOT NULL,
  [Status_Comment]        [nvarchar](255)     NULL,
  [Tracked_By_Ticket_Id]  [nvarchar](50)      NOT NULL, -- From ZenDesk?
  [Tracked_By_Ticket_Uri] [nvarchar](255)     NOT NULL, -- From ZenDesk?
  [Created_On]            [datetimeoffset](7) NOT NULL,
  [Created_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Updated_On]            [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Enabled]               [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Position_Change_Request] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Position_Change_Request] ADD CONSTRAINT [DF_Ghrom_Position_Change_Request_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

ALTER TABLE [Ghrom_Position_Change_Request] ADD CONSTRAINT [DF_Ghrom_Position_Change_Request_Status]
  DEFAULT (N'NEW') FOR [Status];
GO

ALTER TABLE [Ghrom_Position_Change_Request] ADD CONSTRAINT [DF_Ghrom_Position_Change_Request_Enabled]
  DEFAULT (1) FOR [Enabled];
GO

--
-- Ghrom_Position_Change_Request_Item
--

CREATE TABLE [Ghrom_Position_Change_Request_Item] (
  [Id]                                [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Ghrom_Position_Change_Request_Id]  [uniqueidentifier]  NOT NULL,
  [Effective_From]                    [date]              NOT NULL,
  [Comment]                           [nvarchar](255)     NULL,
  [Action]                            [nvarchar](255)     NOT NULL,
  [Position_Id]                       [nvarchar](50)      NOT NULL, -- FK to Position?
  [Position_Title]                    [nvarchar](50)      NOT NULL, -- Check field name/type
  [Is_Position_Managerial]            [bit]               NOT NULL, -- Check field name/type
  [Position_Org_Unit_Id]              [nvarchar](50)      NOT NULL, -- FK to Org Unit?
  [Position_Job_Profile_Id]           [nvarchar](50)      NOT NULL, -- FK to Job profile?
  [Status]                            [nvarchar](50)      NOT NULL,
  [Status_Comment]                    [nvarchar](255)     NULL,
  [Created_On]                        [datetimeoffset](7) NOT NULL,
  [Created_By]                        [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Updated_On]                        [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]                        [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Enabled]                           [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Position_Change_Request_Item] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Position_Change_Request_Item] ADD CONSTRAINT [DF_Ghrom_Position_Change_Request_Item_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

ALTER TABLE [Ghrom_Position_Change_Request_Item] ADD CONSTRAINT [DF_Ghrom_Position_Change_Request_Item_Status]
  DEFAULT (N'NEW') FOR [Status];
GO

ALTER TABLE [Ghrom_Position_Change_Request_Item] ADD CONSTRAINT [DF_Ghrom_Position_Change_Request_Item_Enabled]
  DEFAULT (1) FOR [Enabled];
GO

CREATE NONCLUSTERED INDEX [IX_Ghrom_Position_Change_Request_Item_Ghrom_Position_Change_Request_Id] ON [Ghrom_Position_Change_Request_Item]
(
  [Ghrom_Position_Change_Request_Id] ASC
);
GO

ALTER TABLE [Ghrom_Position_Change_Request_Item] ADD CONSTRAINT [FK_Ghrom_Position_Change_Request_Item_Ghrom_Position_Change_Request]
  FOREIGN KEY ([Ghrom_Position_Change_Request_Id])
  REFERENCES [Ghrom_Position_Change_Request] ([Id]);
GO

--
-- Ghrom_Org_Unit_Change_Request
--

CREATE TABLE [Ghrom_Org_Unit_Change_Request] (
  [Id]                    [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Requester_Id]          [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Requester_Full_Name]   [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Email]       [nvarchar](255)     NOT NULL, -- Check field name/type
  [Status]                [nvarchar](50)      NOT NULL,
  [Status_Comment]        [nvarchar](255)     NULL,
  [Tracked_By_Ticket_Id]  [nvarchar](50)      NOT NULL, -- From ZenDesk?
  [Tracked_By_Ticket_Uri] [nvarchar](255)     NOT NULL, -- From ZenDesk?
  [Created_On]            [datetimeoffset](7) NOT NULL,
  [Created_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Updated_On]            [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Enabled]               [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Org_Unit_Change_Request] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Org_Unit_Change_Request] ADD CONSTRAINT [DF_Ghrom_Org_Unit_Change_Request_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

ALTER TABLE [Ghrom_Org_Unit_Change_Request] ADD CONSTRAINT [DF_Ghrom_Org_Unit_Change_Request_Status]
  DEFAULT (N'NEW') FOR [Status];
GO

ALTER TABLE [Ghrom_Org_Unit_Change_Request] ADD CONSTRAINT [DF_Ghrom_Org_Unit_Change_Request_Enabled]
  DEFAULT (1) FOR [Enabled];
GO

--
-- Ghrom_Org_Unit_Change_Request_Item
--

CREATE TABLE [Ghrom_Org_Unit_Change_Request_Item] (
  [Id]                                [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Ghrom_Org_Unit_Change_Request_Id]  [uniqueidentifier]  NOT NULL,
  [Effective_From]                    [date]              NOT NULL,
  [Comment]                           [nvarchar](255)     NULL,
  [Action]                            [nvarchar](255)     NOT NULL,
  [Org_Unit_Id]                       [nvarchar](50)      NOT NULL, -- FK to Org Unit?
  [Org_Unit_Title]                    [nvarchar](50)      NOT NULL, -- Check field name/type
  [Is_Position_Managerial]            [bit]               NOT NULL, -- Check field name/type
  [Parent_Org_Unit_Id]                [nvarchar](50)      NOT NULL, -- FK to Org Unit?
  [Org_Unit_Manager_Id]               [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Status]                            [nvarchar](50)      NOT NULL,
  [Status_Comment]                    [nvarchar](255)     NULL,
  [Created_On]                        [datetimeoffset](7) NOT NULL,
  [Created_By]                        [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Updated_On]                        [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]                        [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Enabled]                           [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Org_Unit_Change_Request_Item] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Org_Unit_Change_Request_Item] ADD CONSTRAINT [DF_Ghrom_Org_Unit_Change_Request_Item_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

ALTER TABLE [Ghrom_Org_Unit_Change_Request_Item] ADD CONSTRAINT [DF_Ghrom_Org_Unit_Change_Request_Item_Status]
  DEFAULT (N'NEW') FOR [Status];
GO

ALTER TABLE [Ghrom_Org_Unit_Change_Request_Item] ADD CONSTRAINT [DF_Ghrom_Org_Unit_Change_Request_Item_Enabled]
  DEFAULT (1) FOR [Enabled];
GO

CREATE NONCLUSTERED INDEX [IX_Ghrom_Org_Unit_Change_Request_Item_Ghrom_Org_Unit_Change_Request_Id] ON [Ghrom_Org_Unit_Change_Request_Item]
(
  [Ghrom_Org_Unit_Change_Request_Id] ASC
);
GO

ALTER TABLE [Ghrom_Org_Unit_Change_Request_Item] ADD CONSTRAINT [FK_Ghrom_Org_Unit_Change_Request_Item_Ghrom_Org_Unit_Change_Request]
  FOREIGN KEY ([Ghrom_Org_Unit_Change_Request_Id])
  REFERENCES [Ghrom_Org_Unit_Change_Request] ([Id]);
GO

--
-- Ghrom_Jobcode_Change_Request
--

CREATE TABLE [Ghrom_Jobcode_Change_Request] (
  [Id]                    [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Requester_Id]          [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Requester_Full_Name]   [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Email]       [nvarchar](255)     NOT NULL, -- Check field name/type
  [Status]                [nvarchar](50)      NOT NULL,
  [Status_Comment]        [nvarchar](255)     NULL,
  [Tracked_By_Ticket_Id]  [nvarchar](50)      NOT NULL, -- From ZenDesk?
  [Tracked_By_Ticket_Uri] [nvarchar](255)     NOT NULL, -- From ZenDesk?
  [Effective_From]        [date]              NOT NULL,
  [Comment]               [nvarchar](255)     NULL,
  [Action]                [nvarchar](50)      NOT NULL,
  [Position_Id]           [nvarchar](50)      NOT NULL, -- FK to Position
  [Created_On]            [datetimeoffset](7) NOT NULL,
  [Created_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Updated_On]            [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Enabled]               [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Jobcode_Change_Request] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Jobcode_Change_Request] ADD CONSTRAINT [DF_Ghrom_Jobcode_Change_Request_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

ALTER TABLE [Ghrom_Jobcode_Change_Request] ADD CONSTRAINT [DF_Ghrom_Jobcode_Change_Request_Status]
  DEFAULT (N'NEW') FOR [Status];
GO

ALTER TABLE [Ghrom_Jobcode_Change_Request] ADD CONSTRAINT [DF_Ghrom_Jobcode_Change_Request_Enabled]
  DEFAULT (1) FOR [Enabled];
GO

--
-- Ghrom_Approval
--

CREATE TABLE [Ghrom_Approval] (
  [Id]                 [uniqueidentifier] ROWGUIDCOL NOT NULL,
  [Request_Id]         [uniqueidentifier] NOT NULL, -- FK (not enforced) to one of the request tables.
  [Request_Type]       [nvarchar](50)     NOT NULL,
  [Approval_Seq]       [int]              NOT NULL,
  [Approval_Type]      [nvarchar](50)     NOT NULL,
  [Approval_Status]    [nvarchar](50)     NOT NULL,
  [Approver_Id]        [nvarchar](50)     NOT NULL, -- FK to Employee?
  [Approver_Full_Name] [nvarchar](255)    NOT NULL, -- Check field name/type?
  [Approver_Email]     [nvarchar](255)    NOT NULL, -- Check field name/type
  CONSTRAINT [PK_Ghrom_Approval] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Approval] ADD CONSTRAINT [DF_Ghrom_Approval_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

  --
-- Remuneration_Increase_Request
--

CREATE TABLE [Ghrom_Remuneration_Increase_Request] (
  [Id]                           [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Current_Rem_Fixed_Amount]     [decimal](19,4)     NULL, -- Should this be nullable?
  [Current_Rem_Risk_Percentage]  [decimal](5,2)      NULL, -- Should this be nullable?
  [Current_Rem_Currency_Code]    [nvarchar](50)      NULL, -- Should this be nullable?
  [Proposed_Rem_Fixed_Amount]    [decimal](19,4)     NULL, -- Should this be nullable?
  [Proposed_Rem_Risk_Percentage] [decimal](5,2)      NULL, -- Should this be nullable?
  [Proposed_Rem_Currency_Code]   [nvarchar](50)      NULL, -- Should this be nullable?
  [Employee_Id]                  [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Employee_Full_Name]           [nvarchar](255)     NOT NULL, -- Check field name/type
  [Employee_Email]               [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Id]                 [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Requester_Full_Name]          [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Email]              [nvarchar](255)     NOT NULL, -- Check field name/type
  [Status]                       [nvarchar](50)      NOT NULL,
  [Status_Comment]               [nvarchar](255)     NULL,
  [Tracked_By_Ticket_Id]         [nvarchar](50)      NOT NULL, -- From ZenDesk?
  [Tracked_By_Ticket_Uri]        [nvarchar](255)     NOT NULL, -- From ZenDesk?
  [Created_On]                   [datetimeoffset](7) NOT NULL,
  [Created_By]                   [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Updated_On]                   [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]                   [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Enabled]                      [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Remuneration_Increase_Request] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Remuneration_Increase_Request] ADD CONSTRAINT [DF_Ghrom_Remuneration_Increase_Request_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

--
-- Voluntary_Redundancy_Request
--

CREATE TABLE [Ghrom_Voluntary_Redundancy_Request] (
  [Id]                                [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [Is_Interested_In_Redeployment]     [bit]               NOT NULL,
  [Is_Manager_Consulted]              [bit]               NOT NULL,
  [Is_Participating_To_Job_Reduction] [bit]               NOT NULL,
  [Supporting_Comment]                [nvarchar](255)     NULL,
  [Employee_Id]                       [nvarchar](50)      NOT NULL, -- FK to Employee?
  [Employee_Full_Name]                [nvarchar](255)     NOT NULL, -- Check field name/type
  [Employee_Email]                    [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Id]                      [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Requester_Full_Name]               [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Email]                   [nvarchar](255)     NOT NULL, -- Check field name/type
  [Status]                            [nvarchar](50)      NOT NULL,
  [Status_Comment]                    [nvarchar](255)     NULL,
  [Tracked_By_Ticket_Id]              [nvarchar](50)      NOT NULL, -- From ZenDesk?
  [Tracked_By_Ticket_Uri]             [nvarchar](255)     NOT NULL, -- From ZenDesk?
  [Created_On]                        [datetimeoffset](7) NOT NULL,
  [Created_By]                        [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Updated_On]                        [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]                        [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Enabled]                           [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Voluntary_Redundancy_Request] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Voluntary_Redundancy_Request] ADD CONSTRAINT [DF_Ghrom_Voluntary_Redundancy_Request_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

--
-- Recruitment_Request
--

CREATE TABLE [Ghrom_Recruitment_Request] (
  [Id]                    [uniqueidentifier]  ROWGUIDCOL NOT NULL,
  [No_Of_Positions]       [int]               NOT NULL,
  [Role_Name]             [nvarchar](50)      NOT NULL,
  [Role_Description]      [nvarchar](255)     NOT NULL,
  [Request_Type]          [nvarchar](50)      NOT NULL,
  [Role_Type]             [nvarchar](50)      NOT NULL,
  [Start_Date]            [date]              NULL,
  [End_Date]              [date]              NULL,
  [Is_Fte]                [bit]               NOT NULL,
  [Location]              [nvarchar](255)     NOT NULL,
  [Requester_Id]          [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Requester_Full_Name]   [nvarchar](255)     NOT NULL, -- Check field name/type
  [Requester_Email]       [nvarchar](255)     NOT NULL, -- Check field name/type
  [Status]                [nvarchar](50)      NOT NULL,
  [Status_Comment]        [nvarchar](255)     NULL,
  [Tracked_By_Ticket_Id]  [nvarchar](50)      NOT NULL, -- From ZenDesk?
  [Tracked_By_Ticket_Uri] [nvarchar](255)     NOT NULL, -- From ZenDesk?
  [Created_On]            [datetimeoffset](7) NOT NULL,
  [Created_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Updated_On]            [datetimeoffset](7) NOT NULL, -- Initially same as [Created_On]
  [Updated_By]            [nvarchar](50)      NOT NULL, -- FK to Employee Id?
  [Enabled]               [bit]               NOT NULL,
  CONSTRAINT [PK_Ghrom_Recruitment_Request] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Recruitment_Request] ADD CONSTRAINT [DF_Ghrom_Recruitment_Request_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

--
-- Recruitment_Direct_Employee
--

CREATE TABLE [Ghrom_Recruitment_Direct_Employee] (
  [Id]                           [uniqueidentifier] ROWGUIDCOL NOT NULL,
  [Ghrom_Recruitment_Request_Id] [uniqueidentifier] NOT NULL,
  [Employee_Id]                  [nvarchar](50)     NOT NULL, -- FK to Employee?
  [Employee_Full_Name]           [nvarchar](255)    NOT NULL, -- Check field name/type
  [Employee_Email]               [nvarchar](255)    NOT NULL, -- Check field name/type
  CONSTRAINT [PK_Ghrom_Recruitment_Direct_Employee] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Recruitment_Direct_Employee] ADD CONSTRAINT [DF_Ghrom_Recruitment_Direct_Employee_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

CREATE NONCLUSTERED INDEX [IX_Ghrom_Recruitment_Direct_Employee_Ghrom_Recruitment_Request_Id] ON [Ghrom_Recruitment_Direct_Employee]
(
  [Ghrom_Recruitment_Request_Id] ASC
);
GO

ALTER TABLE [Ghrom_Recruitment_Direct_Employee] ADD CONSTRAINT [FK_Ghrom_Recruitment_Direct_Employee_Ghrom_Recruitment_Request]
  FOREIGN KEY ([Ghrom_Recruitment_Request_Id])
  REFERENCES [Ghrom_Recruitment_Request] ([Id]);
GO

--
-- Recruitment_Direct_External
--

CREATE TABLE [Ghrom_Recruitment_Direct_External] (
  [Id]                           [uniqueidentifier] ROWGUIDCOL NOT NULL,
  [Ghrom_Recruitment_Request_Id] [uniqueidentifier] NOT NULL,
  [Candidate_Full_Name]          [nvarchar](255)    NOT NULL,
  [Candidate_Email]              [nvarchar](255)    NOT NULL, -- Check field name/type
  [Candidate_Contact_No]         [nvarchar](50)     NOT NULL, -- Check field name/type
  [Candidate_Agency_Name]        [nvarchar](100)    NOT NULL, -- Check field name/type
  CONSTRAINT [PK_Ghrom_Recruitment_Direct_External] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
);
GO

ALTER TABLE [Ghrom_Recruitment_Direct_External] ADD CONSTRAINT [DF_Ghrom_Recruitment_Direct_External_Id]
  DEFAULT (newsequentialid()) FOR [Id];
GO

CREATE NONCLUSTERED INDEX [IX_Ghrom_Recruitment_Direct_External_Ghrom_Recruitment_Request_Id] ON [Ghrom_Recruitment_Direct_External]
(
  [Ghrom_Recruitment_Request_Id] ASC
);
GO

ALTER TABLE [Ghrom_Recruitment_Direct_External] ADD CONSTRAINT [FK_Ghrom_Recruitment_Direct_External_Ghrom_Recruitment_Request]
  FOREIGN KEY ([Ghrom_Recruitment_Request_Id])
  REFERENCES [Ghrom_Recruitment_Request] ([Id]);
GO
