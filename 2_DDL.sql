USE [WorkoutTracker]
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[workout_category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](64) NOT NULL,
 CONSTRAINT [PK_workout_category] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[workout_collection](
	[workout_id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NOT NULL,
	[workout_title] [varchar](128) NOT NULL,
	[workout_note] [varchar](256) NOT NULL,
	[calories_burn_per_min] [float] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_workout_collection] PRIMARY KEY CLUSTERED 
(
	[workout_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[workout_collection]  WITH CHECK ADD  CONSTRAINT [FK_user_workout_collection_uid] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[workout_collection] CHECK CONSTRAINT [FK_user_workout_collection_uid]
GO

ALTER TABLE [dbo].[workout_collection]  WITH CHECK ADD  CONSTRAINT [FK_workout_collection_workout_category] FOREIGN KEY([category_id])
REFERENCES [dbo].[workout_category] ([category_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[workout_collection] CHECK CONSTRAINT [FK_workout_collection_workout_category]
GO

CREATE TABLE [dbo].[workout_active](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[workout_id] [int] NOT NULL,
	[start_time] [time](7) NULL,
	[start_date] [date] NULL,
	[end_date] [date] NULL,
	[end_time] [time](7) NULL,
	[comment] [varchar](64) NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[workout_active]  WITH CHECK ADD  CONSTRAINT [FK_workout_collection_workout_active] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout_collection] ([workout_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[workout_active] CHECK CONSTRAINT [FK_workout_collection_workout_active]
GO






