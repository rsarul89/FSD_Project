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
[category_name]
)
VALUES
(
'DemoCategory'
)
GO