USE [Series]
GO
/****** Object:  Table [dbo].[Episodes]    Script Date: 15-3-2018 21:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Episodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerieId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[Season] [int] NOT NULL,
	[Name] [nvarchar](75) NOT NULL,
	[Description] [ntext] NOT NULL,
	[Status] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Episodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Series]    Script Date: 15-3-2018 21:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Series](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](75) NOT NULL,
	[Description] [ntext] NOT NULL,
	[Status] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Series] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Episodes] ADD  CONSTRAINT [DF_Episodes_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Series] ADD  CONSTRAINT [DF_Series_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Episodes]  WITH CHECK ADD  CONSTRAINT [FK_Episodes_Series] FOREIGN KEY([SerieId])
REFERENCES [dbo].[Series] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Episodes] CHECK CONSTRAINT [FK_Episodes_Series]
GO
