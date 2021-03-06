USE [master]
GO
/****** Object:  Database [Permission]    Script Date: 11/26/2015 15:39:55 ******/
CREATE DATABASE [Permission] ON  PRIMARY 
( NAME = N'Permission', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\Permission.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Permission_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\Permission_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Permission] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Permission].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Permission] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Permission] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Permission] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Permission] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Permission] SET ARITHABORT OFF
GO
ALTER DATABASE [Permission] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Permission] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Permission] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Permission] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Permission] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Permission] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Permission] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Permission] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Permission] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Permission] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Permission] SET  DISABLE_BROKER
GO
ALTER DATABASE [Permission] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Permission] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Permission] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Permission] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Permission] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Permission] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Permission] SET  READ_WRITE
GO
ALTER DATABASE [Permission] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Permission] SET  MULTI_USER
GO
ALTER DATABASE [Permission] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Permission] SET DB_CHAINING OFF
GO
USE [Permission]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 11/26/2015 15:39:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[P_Code] [nvarchar](20) NOT NULL,
	[P_Name] [nvarchar](20) NULL,
	[P_Sort] [tinyint] NULL,
	[P_Visible] [bit] NULL,
 CONSTRAINT [PK_PERMISSIONS] PRIMARY KEY CLUSTERED 
(
	[P_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录模块名称、编码等模块基本数据。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions'
GO
INSERT [dbo].[Permissions] ([P_Code], [P_Name], [P_Sort], [P_Visible]) VALUES (N'100', N'浏览', 1, 1)
INSERT [dbo].[Permissions] ([P_Code], [P_Name], [P_Sort], [P_Visible]) VALUES (N'101', N'添加', 2, 1)
INSERT [dbo].[Permissions] ([P_Code], [P_Name], [P_Sort], [P_Visible]) VALUES (N'102', N'修改', 3, 1)
INSERT [dbo].[Permissions] ([P_Code], [P_Name], [P_Sort], [P_Visible]) VALUES (N'103', N'删除', 4, 1)
/****** Object:  Table [dbo].[UserRole]    Script Date: 11/26/2015 15:39:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UR_Id] [int] NOT NULL,
	[U_Id] [int] NULL,
	[UR_Code] [nvarchar](20) NULL,
 CONSTRAINT [PK_USERROLE] PRIMARY KEY CLUSTERED 
(
	[UR_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录每个用户对应的角色，可以是多个，但本例只设置对应一个，只要用户具有了某个角色，那么该用户就具有了和角色一样的权限。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRole'
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/26/2015 15:39:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[U_Id] [bigint] NOT NULL,
	[U_Name] [nvarchar](50) NULL,
	[U_Password] [nvarchar](50) NULL,
	[R_Code] [nvarchar](20) NULL,
	[R_Visible] [bit] NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[U_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录用户名等用户基本信息。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO
/****** Object:  View [dbo].[V_RolePermissions]    Script Date: 11/26/2015 15:39:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_RolePermissions]
AS
SELECT     dbo.RolePermissions.R_Code, dbo.V_ModulePermissions.Id
FROM         dbo.RolePermissions INNER JOIN
                      dbo.V_ModulePermissions ON dbo.RolePermissions.MP_Id = dbo.V_ModulePermissions.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RolePermissions"
            Begin Extent = 
               Top = 6
               Left = 21
               Bottom = 182
               Right = 180
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "V_ModulePermissions"
            Begin Extent = 
               Top = 6
               Left = 218
               Bottom = 173
               Right = 471
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_RolePermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_RolePermissions'
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/26/2015 15:39:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[R_Code] [nvarchar](20) NOT NULL,
	[R_Name] [nvarchar](20) NULL,
	[R_Sort] [int] NULL,
	[R_Visible] [bit] NULL,
 CONSTRAINT [PK_ROLES] PRIMARY KEY CLUSTERED 
(
	[R_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录角色名称、编码等角色基本数据。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles'
GO
INSERT [dbo].[Roles] ([R_Code], [R_Name], [R_Sort], [R_Visible]) VALUES (N'1', N'管理员', 1, 1)
INSERT [dbo].[Roles] ([R_Code], [R_Name], [R_Sort], [R_Visible]) VALUES (N'2', N'普通', 2, 1)
INSERT [dbo].[Roles] ([R_Code], [R_Name], [R_Sort], [R_Visible]) VALUES (N'22', N'测试', NULL, 1)
/****** Object:  Table [dbo].[Module]    Script Date: 11/26/2015 15:39:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[M_Code] [nvarchar](20) NOT NULL,
	[M_Name] [nvarchar](20) NULL,
	[M_ParentCode] [nvarchar](20) NULL,
	[M_LinkeUrl] [nvarchar](100) NULL,
	[M_Sort] [int] NULL,
	[M_Visible] [bit] NULL,
 CONSTRAINT [PK_MODULE] PRIMARY KEY CLUSTERED 
(
	[M_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限表，记录所有模块权限distinct之后的数据。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Module'
GO
INSERT [dbo].[Module] ([M_Code], [M_Name], [M_ParentCode], [M_LinkeUrl], [M_Sort], [M_Visible]) VALUES (N'10', N'新闻公告', N'0', NULL, 0, 1)
INSERT [dbo].[Module] ([M_Code], [M_Name], [M_ParentCode], [M_LinkeUrl], [M_Sort], [M_Visible]) VALUES (N'1000', N'企业公告', N'10', N'~/News/Index.aspx?category=1', 0, 1)
INSERT [dbo].[Module] ([M_Code], [M_Name], [M_ParentCode], [M_LinkeUrl], [M_Sort], [M_Visible]) VALUES (N'1005', N'企业新闻', N'10', N'~/News/Index.aspx?category=2', 5, 1)
INSERT [dbo].[Module] ([M_Code], [M_Name], [M_ParentCode], [M_LinkeUrl], [M_Sort], [M_Visible]) VALUES (N'1010', N'行业新闻', N'10', N'~/News/Index.aspx?category=3', 10, 1)
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 11/26/2015 15:39:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[R_Code] [nvarchar](20) NULL,
	[MP_Id] [int] NULL,
 CONSTRAINT [PK_ROLEPERMISSIONS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RolePermissions] ON
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (2, N'5', 1)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (3, N'5', 2)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (60, N'2', 1)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (61, N'3', 1)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (62, N'3', 2)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (63, N'3', 3)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (64, N'3', 4)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (69, N'1', 1)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (70, N'1', 4)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (71, N'1', 2)
INSERT [dbo].[RolePermissions] ([Id], [R_Code], [MP_Id]) VALUES (72, N'22', 1)
SET IDENTITY_INSERT [dbo].[RolePermissions] OFF
/****** Object:  Table [dbo].[ModulePermissions]    Script Date: 11/26/2015 15:39:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModulePermissions](
	[Id] [int] NOT NULL,
	[M_Code] [nvarchar](20) NULL,
	[P_Code] [nvarchar](20) NULL,
 CONSTRAINT [PK_MODULEPERMISSIONS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录每个模块对应的权限，一个模块可能存在多条数据，每条表示该模块的一个操作权限。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ModulePermissions'
GO
INSERT [dbo].[ModulePermissions] ([Id], [M_Code], [P_Code]) VALUES (1, N'1000', N'100')
INSERT [dbo].[ModulePermissions] ([Id], [M_Code], [P_Code]) VALUES (2, N'1000', N'101')
INSERT [dbo].[ModulePermissions] ([Id], [M_Code], [P_Code]) VALUES (3, N'1000', N'102')
INSERT [dbo].[ModulePermissions] ([Id], [M_Code], [P_Code]) VALUES (4, N'1000', N'103')
/****** Object:  View [dbo].[V_ModulePermissions]    Script Date: 11/26/2015 15:39:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ModulePermissions]
AS
SELECT     dbo.ModulePermissions.M_Code, dbo.ModulePermissions.P_Code, dbo.Permissions.P_Name, dbo.Module.M_Name, dbo.ModulePermissions.Id, 
                      dbo.Module.M_ParentCode
FROM         dbo.Module INNER JOIN
                      dbo.ModulePermissions ON dbo.Module.M_Code = dbo.ModulePermissions.M_Code INNER JOIN
                      dbo.Permissions ON dbo.ModulePermissions.P_Code = dbo.Permissions.P_Code
WHERE     (dbo.Module.M_Visible = 1) AND (dbo.Permissions.P_Visible = 1)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Module"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 170
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ModulePermissions"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 190
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Permissions"
            Begin Extent = 
               Top = 6
               Left = 416
               Bottom = 202
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_ModulePermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_ModulePermissions'
GO
/****** Object:  Default [DF_Roles_R_Visible]    Script Date: 11/26/2015 15:39:57 ******/
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_R_Visible]  DEFAULT ((1)) FOR [R_Visible]
GO
/****** Object:  ForeignKey [FK_MODULEPE_FK_MP_M_MODULE]    Script Date: 11/26/2015 15:39:57 ******/
ALTER TABLE [dbo].[ModulePermissions]  WITH CHECK ADD  CONSTRAINT [FK_MODULEPE_FK_MP_M_MODULE] FOREIGN KEY([M_Code])
REFERENCES [dbo].[Module] ([M_Code])
GO
ALTER TABLE [dbo].[ModulePermissions] CHECK CONSTRAINT [FK_MODULEPE_FK_MP_M_MODULE]
GO
