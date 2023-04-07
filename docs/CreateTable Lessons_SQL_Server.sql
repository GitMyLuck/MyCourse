USE [MyCourse]
GO

/****** Object:  Table [dbo].[Lessons]    
Author: Giovanni Avallone
Script Date: 06/04/2023 23:18:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Lessons](
	[Id] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](8000) NULL,
	[Duration] [varchar](8) NOT NULL,
 CONSTRAINT [PK_Lessons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lessons] ADD  DEFAULT ('00:00:00') FOR [Duration]
GO

ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO


