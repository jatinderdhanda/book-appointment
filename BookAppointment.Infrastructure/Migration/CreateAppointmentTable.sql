USE [BookingAppointment]
GO

/****** Object:  Table [dbo].[Appointment]    Script Date: 22/04/2024 3:42:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Appointment](
	[booking_id] [uniqueidentifier] NOT NULL,
	[booking_date] [datetime] NOT NULL,
	[booking_time] [time](7) NOT NULL,
	[created_by] [nvarchar](50) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[modified_by] [nvarchar](50) NOT NULL,
	[modified_date] [datetime] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[booking_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


