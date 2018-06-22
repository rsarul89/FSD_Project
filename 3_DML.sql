USE [WorkoutTracker]
GO

INSERT INTO [dbo].[user]
           ([user_name]
           ,[password])
     VALUES
           ('DemoUser','DemoPassword')
GO

INSERT INTO [dbo].[workout_category]
(
category_id
,[category_name]
)
VALUES
(
NULL,
'DemoCategory'
)
GO