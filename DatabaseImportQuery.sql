USE [master]
GO
/****** Object:  Database [NET23School]    Script Date: 2023-12-22 18:36:20 ******/
CREATE DATABASE [NET23School]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NET23School', FILENAME = N'C:\Users\lefud\NET23School.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NET23School_log', FILENAME = N'C:\Users\lefud\NET23School_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NET23School] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NET23School].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NET23School] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NET23School] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NET23School] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NET23School] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NET23School] SET ARITHABORT OFF 
GO
ALTER DATABASE [NET23School] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NET23School] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NET23School] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NET23School] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NET23School] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NET23School] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NET23School] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NET23School] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NET23School] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NET23School] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NET23School] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NET23School] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NET23School] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NET23School] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NET23School] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NET23School] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NET23School] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NET23School] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NET23School] SET  MULTI_USER 
GO
ALTER DATABASE [NET23School] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NET23School] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NET23School] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NET23School] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NET23School] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NET23School] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [NET23School] SET QUERY_STORE = OFF
GO
USE [NET23School]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](30) NOT NULL,
	[FK_EmployeeID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeCourses]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeCourses](
	[FK_EmployeeID] [int] NULL,
	[FK_CourseID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeRoles]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRoles](
	[FK_EmployeeID] [int] NULL,
	[FK_RoleID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[SocialSecurityNumber] [nvarchar](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[GradeID] [int] IDENTITY(1,1) NOT NULL,
	[Grade] [nvarchar](5) NOT NULL,
	[GradeDate] [date] NULL,
	[FK_CourseID] [int] NULL,
	[FK_StudentID] [int] NULL,
	[FK_EmployeeID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GradeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentCourses]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentCourses](
	[FK_StudentID] [int] NULL,
	[FK_CourseID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2023-12-22 18:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[SocialSecurityNr] [nvarchar](12) NULL,
	[FK_ClassID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 
GO
INSERT [dbo].[Classes] ([ClassID], [ClassName], [FK_EmployeeID]) VALUES (1, N'NET23', 3)
GO
INSERT [dbo].[Classes] ([ClassID], [ClassName], [FK_EmployeeID]) VALUES (2, N'NET24', 4)
GO
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 
GO
INSERT [dbo].[Courses] ([CourseID], [CourseName]) VALUES (1, N'Programming')
GO
INSERT [dbo].[Courses] ([CourseID], [CourseName]) VALUES (2, N'Procrastination')
GO
INSERT [dbo].[Courses] ([CourseID], [CourseName]) VALUES (3, N'Burp-singing')
GO
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
INSERT [dbo].[EmployeeCourses] ([FK_EmployeeID], [FK_CourseID]) VALUES (3, 1)
GO
INSERT [dbo].[EmployeeCourses] ([FK_EmployeeID], [FK_CourseID]) VALUES (3, 2)
GO
INSERT [dbo].[EmployeeCourses] ([FK_EmployeeID], [FK_CourseID]) VALUES (4, 3)
GO
INSERT [dbo].[EmployeeCourses] ([FK_EmployeeID], [FK_CourseID]) VALUES (4, 1)
GO
INSERT [dbo].[EmployeeRoles] ([FK_EmployeeID], [FK_RoleID]) VALUES (1, 3)
GO
INSERT [dbo].[EmployeeRoles] ([FK_EmployeeID], [FK_RoleID]) VALUES (2, 4)
GO
INSERT [dbo].[EmployeeRoles] ([FK_EmployeeID], [FK_RoleID]) VALUES (3, 1)
GO
INSERT [dbo].[EmployeeRoles] ([FK_EmployeeID], [FK_RoleID]) VALUES (4, 1)
GO
INSERT [dbo].[EmployeeRoles] ([FK_EmployeeID], [FK_RoleID]) VALUES (5, 2)
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (1, N'Karl-Göran', N'Burtz', N'155012241234')
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (2, N'Inga', N'Pengar', N'199911111223')
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (3, N'Bingus', N'Bongus', N'201201012255')
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (4, N'Francinne', N'Mcman', N'192103183568')
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (5, N'Prissy', N'Principallina', N'200001010101')
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (1005, N'Sven', N'Svensson', N'185601021132')
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Grades] ON 
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (1, N'5', CAST(N'2023-12-01' AS Date), 1, 1, 3)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (2, N'3', CAST(N'2023-12-01' AS Date), 2, 1, 3)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (3, N'1', CAST(N'2023-08-08' AS Date), 3, 2, 4)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (4, N'4', CAST(N'2023-08-08' AS Date), 3, 3, 4)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (5, N'4', CAST(N'2023-08-08' AS Date), 3, 4, 4)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (6, N'2', CAST(N'2023-12-01' AS Date), 2, 3, 3)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (7, N'5', CAST(N'2023-12-01' AS Date), 1, 2, 4)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (8, N'4', CAST(N'2023-07-08' AS Date), 1, 3, 3)
GO
INSERT [dbo].[Grades] ([GradeID], [Grade], [GradeDate], [FK_CourseID], [FK_StudentID], [FK_EmployeeID]) VALUES (9, N'3', CAST(N'2023-07-08' AS Date), 1, 4, 3)
GO
SET IDENTITY_INSERT [dbo].[Grades] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([RoleId], [Title]) VALUES (1, N'Teacher')
GO
INSERT [dbo].[Roles] ([RoleId], [Title]) VALUES (2, N'Principal')
GO
INSERT [dbo].[Roles] ([RoleId], [Title]) VALUES (3, N'Janitor')
GO
INSERT [dbo].[Roles] ([RoleId], [Title]) VALUES (4, N'Administrator')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (1, 1)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (1, 2)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (1, 3)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (2, 1)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (2, 3)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (3, 1)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (3, 2)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (4, 1)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (4, 2)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (4, 3)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (5, 2)
GO
INSERT [dbo].[StudentCourses] ([FK_StudentID], [FK_CourseID]) VALUES (6, 3)
GO
SET IDENTITY_INSERT [dbo].[Students] ON 
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (1, N'Daniel', N'Frykman', N'198504267892', 1)
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (2, N'Lena', N'Lingon', N'154312241122', 1)
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (3, N'Herbert', N'Woodson', N'200001010111', 1)
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (4, N'Arga', N'Karja', N'200001010121', 2)
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (5, N'Ping', N'McLag', N'200001010101', 2)
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (6, N'Robin', N'Hardcastle', N'200001010131', 2)
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (1002, N'Karl', N'Buttz', N'198502030102', NULL)
GO
INSERT [dbo].[Students] ([StudentID], [FirstName], [LastName], [SocialSecurityNr], [FK_ClassID]) VALUES (1003, N'Test', N'McTester', N'111122334455', NULL)
GO
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD FOREIGN KEY([FK_EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeCourses]  WITH CHECK ADD FOREIGN KEY([FK_CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[EmployeeCourses]  WITH CHECK ADD FOREIGN KEY([FK_EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD FOREIGN KEY([FK_EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD FOREIGN KEY([FK_RoleID])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Courses] FOREIGN KEY([FK_CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Courses]
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Employees] FOREIGN KEY([FK_EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Employees]
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Students] FOREIGN KEY([FK_StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Students]
GO
ALTER TABLE [dbo].[StudentCourses]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourses_Courses] FOREIGN KEY([FK_CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[StudentCourses] CHECK CONSTRAINT [FK_StudentCourses_Courses]
GO
ALTER TABLE [dbo].[StudentCourses]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourses_Students] FOREIGN KEY([FK_StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[StudentCourses] CHECK CONSTRAINT [FK_StudentCourses_Students]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([FK_ClassID])
REFERENCES [dbo].[Classes] ([ClassID])
GO
USE [master]
GO
ALTER DATABASE [NET23School] SET  READ_WRITE 
GO
