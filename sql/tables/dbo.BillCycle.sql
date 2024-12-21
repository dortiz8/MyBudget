SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillCycle](
	[BillCycleId] [int] IDENTITY(1,1) NOT NULL,
	[DateCycleStart] [datetime] NOT NULL,
	[DateCycleEnd] [datetime] NULL,
	[Interval] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BillCycle] ADD  CONSTRAINT [PK_BillCycleId] PRIMARY KEY CLUSTERED 
(
	[BillCycleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BillCycle] ADD  CONSTRAINT [DEFAULT_BillCycle_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[UpdateDateCycleEnd]
ON [dbo].[BillCycle]
AFTER INSERT
AS
BEGIN
    UPDATE t
    SET t.DateCycleEnd = DATEADD(DAY, i.Interval, i.DateCycleStart)
    FROM dbo.BillCycle t
    INNER JOIN Inserted i ON t.BillCycleId = i.BillCycleId;
END; 
GO
ALTER TABLE [dbo].[BillCycle] ENABLE TRIGGER [UpdateDateCycleEnd]
GO
