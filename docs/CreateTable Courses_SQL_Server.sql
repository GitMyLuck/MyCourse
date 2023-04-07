USE [MyCourse]
GO

/****** Object:  Table [dbo].[Courses] 
Author : Giovanni Avallone
Script Date: 06/04/2023 23:02:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Courses](
	[Id] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](8000) NULL,
	[ImagePath] [varchar](100) NULL,
	[Author] [varchar](100) NOT NULL,
	[Email] [varchar](80) NULL,
	[Rating] [real] NOT NULL,
	[FullPrice_Amount] [money] NOT NULL,
	[FullPrice_Currency] [varchar](3) NOT NULL,
	[CurrentPrice_Amount] [money] NOT NULL,
	[CurrentPrice_Currency] [varchar](3) NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Courses] ADD  DEFAULT ((0)) FOR [Rating]
GO

ALTER TABLE [dbo].[Courses] ADD  DEFAULT ((0)) FOR [FullPrice_Amount]
GO

ALTER TABLE [dbo].[Courses] ADD  DEFAULT ('EUR') FOR [FullPrice_Currency]
GO

ALTER TABLE [dbo].[Courses] ADD  DEFAULT ((0)) FOR [CurrentPrice_Amount]
GO

ALTER TABLE [dbo].[Courses] ADD  DEFAULT ('EUR') FOR [CurrentPrice_Currency]
GO


