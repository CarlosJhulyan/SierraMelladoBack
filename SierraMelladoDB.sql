USE [master]
GO
/****** Object:  Database [SierraMelladoDB]    Script Date: 7/20/2022 10:13:44 PM ******/
CREATE DATABASE [SierraMelladoDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SierraMelladoDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SierraMelladoDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SierraMelladoDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SierraMelladoDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SierraMelladoDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SierraMelladoDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SierraMelladoDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SierraMelladoDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SierraMelladoDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SierraMelladoDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SierraMelladoDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SierraMelladoDB] SET  MULTI_USER 
GO
ALTER DATABASE [SierraMelladoDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SierraMelladoDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SierraMelladoDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SierraMelladoDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SierraMelladoDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SierraMelladoDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SierraMelladoDB', N'ON'
GO
ALTER DATABASE [SierraMelladoDB] SET QUERY_STORE = OFF
GO
USE [SierraMelladoDB]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[id_admin] [int] IDENTITY(1,1) NOT NULL,
	[rol] [char](18) NULL,
	[id_usuario] [int] NULL,
	[estado] [int] NULL,
 CONSTRAINT [XPKAdmin] PRIMARY KEY CLUSTERED 
(
	[id_admin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articulo]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulo](
	[cod_articulo] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [varchar](100) NULL,
	[contenido] [text] NULL,
	[fecha_crea] [datetime] NULL,
	[imagen] [varchar](200) NULL,
	[autor] [varchar](50) NULL,
	[fecha_mod] [datetime] NULL,
	[id_admin] [int] NULL,
 CONSTRAINT [XPKArticulo] PRIMARY KEY CLUSTERED 
(
	[cod_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articulo_Etiqueta]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulo_Etiqueta](
	[cod_articulo] [int] NOT NULL,
	[cod_etiqueta] [int] NOT NULL,
 CONSTRAINT [XPKArticulo_Etiqueta] PRIMARY KEY CLUSTERED 
(
	[cod_articulo] ASC,
	[cod_etiqueta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cita]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cita](
	[id_cita] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[fecha] [datetime] NULL,
	[hora] [int] NULL,
	[medico] [int] NULL,
	[paciente] [int] NULL,
	[servicio] [int] NULL,
	[num_orden] [int] NULL,
 CONSTRAINT [XPKCita] PRIMARY KEY CLUSTERED 
(
	[id_cita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especialidad]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidad](
	[cod_especialidad] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](20) NULL,
	[abreviatura] [varchar](20) NULL,
 CONSTRAINT [XPKEspecialidad] PRIMARY KEY CLUSTERED 
(
	[cod_especialidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especialidad_Medico]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidad_Medico](
	[cod_especialidad] [int] NOT NULL,
	[id_medico] [int] NOT NULL,
 CONSTRAINT [XPKEspecialidad_Medico] PRIMARY KEY CLUSTERED 
(
	[cod_especialidad] ASC,
	[id_medico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etiqueta]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etiqueta](
	[cod_etiqueta] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [char](20) NULL,
 CONSTRAINT [XPKEtiqueta] PRIMARY KEY CLUSTERED 
(
	[cod_etiqueta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horario]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horario](
	[cod_horario] [int] IDENTITY(1,1) NOT NULL,
	[hora_inicio] [int] NULL,
	[hora_fin] [int] NULL,
	[dia] [varchar](20) NULL,
	[id_medico] [int] NULL,
 CONSTRAINT [XPKHorario] PRIMARY KEY CLUSTERED 
(
	[cod_horario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Informe]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Informe](
	[num_informe] [int] IDENTITY(1,1) NOT NULL,
	[asunto] [varchar](100) NULL,
	[archivo] [varchar](200) NULL,
	[fecha_emi] [datetime] NULL,
	[id_medico] [int] NULL,
 CONSTRAINT [XPKInforme] PRIMARY KEY CLUSTERED 
(
	[num_informe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Informe_Paciente]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Informe_Paciente](
	[num_informe] [int] IDENTITY(1,1) NOT NULL,
	[resumen] [varchar](200) NULL,
	[fecha_emi] [datetime] NULL,
	[archivo] [varchar](200) NULL,
	[medico] [int] NULL,
	[paciente] [int] NULL,
	[cita] [int] NULL,
 CONSTRAINT [XPKInforme_paciente] PRIMARY KEY CLUSTERED 
(
	[num_informe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medico]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medico](
	[id_medico] [int] IDENTITY(1,1) NOT NULL,
	[cod_colegiado] [varchar](5) NULL,
	[dni] [varchar](8) NULL,
	[id_usuario] [int] NULL,
	[celular] [varchar](9) NULL,
	[fecha_nac] [datetime] NULL,
	[avatar] [varchar](200) NULL,
	[estado] [int] NULL,
 CONSTRAINT [XPKMedico] PRIMARY KEY CLUSTERED 
(
	[id_medico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mensaje]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mensaje](
	[id_mensaje] [int] IDENTITY(1,1) NOT NULL,
	[contenido] [text] NULL,
	[fecha_emi] [datetime] NULL,
	[celular_rem] [varchar](9) NULL,
	[correo_rem] [varchar](100) NULL,
	[nombre_apellido_rem] [varchar](200) NULL,
	[tipo] [varchar](1) NULL,
	[id_admin] [int] NULL,
	[estado] [varchar](1) NULL,
 CONSTRAINT [XPKMensaje] PRIMARY KEY CLUSTERED 
(
	[id_mensaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metodo_Pago]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metodo_Pago](
	[cod_metodo] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](20) NULL,
	[abreviatura] [varchar](20) NULL,
 CONSTRAINT [XPKMetodo_pago] PRIMARY KEY CLUSTERED 
(
	[cod_metodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orden]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orden](
	[num_orden] [int] IDENTITY(1,1) NOT NULL,
	[monto_total] [float] NULL,
	[descripcion] [varchar](100) NULL,
	[cod_metodo] [int] NULL,
	[estado] [varchar](1) NULL,
	[vuelto] [float] NULL,
	[importe_total] [float] NULL,
 CONSTRAINT [XPKOrden] PRIMARY KEY CLUSTERED 
(
	[num_orden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[id_paciente] [int] IDENTITY(1,1) NOT NULL,
	[dni] [varchar](8) NULL,
	[id_usuario] [int] NULL,
	[celular] [varchar](9) NULL,
	[fecha_nac] [datetime] NULL,
	[avatar] [varchar](200) NULL,
	[estado] [int] NULL,
 CONSTRAINT [XPKPaciente] PRIMARY KEY CLUSTERED 
(
	[id_paciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicio](
	[id_servicio] [int] IDENTITY(1,1) NOT NULL,
	[abreviatura] [varchar](20) NULL,
	[descripcion] [varchar](100) NULL,
	[precio] [float] NULL,
 CONSTRAINT [XPKServicio] PRIMARY KEY CLUSTERED 
(
	[id_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 7/20/2022 10:13:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](50) NULL,
	[apellido_paterno] [varchar](50) NULL,
	[correo] [varchar](100) NULL,
	[fecha_crea] [datetime] NULL,
	[fecha_mod] [datetime] NULL,
	[usuario] [varchar](20) NULL,
	[clave] [varchar](200) NULL,
	[apellido_materno] [varchar](50) NULL,
 CONSTRAINT [XPKUsuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [R_8] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [R_8]
GO
ALTER TABLE [dbo].[Articulo]  WITH CHECK ADD  CONSTRAINT [R_25] FOREIGN KEY([id_admin])
REFERENCES [dbo].[Admin] ([id_admin])
GO
ALTER TABLE [dbo].[Articulo] CHECK CONSTRAINT [R_25]
GO
ALTER TABLE [dbo].[Articulo_Etiqueta]  WITH CHECK ADD  CONSTRAINT [R_21] FOREIGN KEY([cod_articulo])
REFERENCES [dbo].[Articulo] ([cod_articulo])
GO
ALTER TABLE [dbo].[Articulo_Etiqueta] CHECK CONSTRAINT [R_21]
GO
ALTER TABLE [dbo].[Articulo_Etiqueta]  WITH CHECK ADD  CONSTRAINT [R_23] FOREIGN KEY([cod_etiqueta])
REFERENCES [dbo].[Etiqueta] ([cod_etiqueta])
GO
ALTER TABLE [dbo].[Articulo_Etiqueta] CHECK CONSTRAINT [R_23]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [R_16] FOREIGN KEY([medico])
REFERENCES [dbo].[Medico] ([id_medico])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [R_16]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [R_17] FOREIGN KEY([paciente])
REFERENCES [dbo].[Paciente] ([id_paciente])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [R_17]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [R_18] FOREIGN KEY([servicio])
REFERENCES [dbo].[Servicio] ([id_servicio])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [R_18]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [R_19] FOREIGN KEY([num_orden])
REFERENCES [dbo].[Orden] ([num_orden])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [R_19]
GO
ALTER TABLE [dbo].[Especialidad_Medico]  WITH CHECK ADD  CONSTRAINT [R_30] FOREIGN KEY([cod_especialidad])
REFERENCES [dbo].[Especialidad] ([cod_especialidad])
GO
ALTER TABLE [dbo].[Especialidad_Medico] CHECK CONSTRAINT [R_30]
GO
ALTER TABLE [dbo].[Especialidad_Medico]  WITH CHECK ADD  CONSTRAINT [R_32] FOREIGN KEY([id_medico])
REFERENCES [dbo].[Medico] ([id_medico])
GO
ALTER TABLE [dbo].[Especialidad_Medico] CHECK CONSTRAINT [R_32]
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD  CONSTRAINT [R_33] FOREIGN KEY([id_medico])
REFERENCES [dbo].[Medico] ([id_medico])
GO
ALTER TABLE [dbo].[Horario] CHECK CONSTRAINT [R_33]
GO
ALTER TABLE [dbo].[Informe]  WITH CHECK ADD  CONSTRAINT [R_26] FOREIGN KEY([id_medico])
REFERENCES [dbo].[Medico] ([id_medico])
GO
ALTER TABLE [dbo].[Informe] CHECK CONSTRAINT [R_26]
GO
ALTER TABLE [dbo].[Informe_Paciente]  WITH CHECK ADD  CONSTRAINT [R_20] FOREIGN KEY([cita])
REFERENCES [dbo].[Cita] ([id_cita])
GO
ALTER TABLE [dbo].[Informe_Paciente] CHECK CONSTRAINT [R_20]
GO
ALTER TABLE [dbo].[Informe_Paciente]  WITH CHECK ADD  CONSTRAINT [R_28] FOREIGN KEY([medico])
REFERENCES [dbo].[Medico] ([id_medico])
GO
ALTER TABLE [dbo].[Informe_Paciente] CHECK CONSTRAINT [R_28]
GO
ALTER TABLE [dbo].[Informe_Paciente]  WITH CHECK ADD  CONSTRAINT [R_29] FOREIGN KEY([paciente])
REFERENCES [dbo].[Paciente] ([id_paciente])
GO
ALTER TABLE [dbo].[Informe_Paciente] CHECK CONSTRAINT [R_29]
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [R_7] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [R_7]
GO
ALTER TABLE [dbo].[Mensaje]  WITH CHECK ADD  CONSTRAINT [R_27] FOREIGN KEY([id_admin])
REFERENCES [dbo].[Admin] ([id_admin])
GO
ALTER TABLE [dbo].[Mensaje] CHECK CONSTRAINT [R_27]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [R_9] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [R_9]
GO
USE [master]
GO
ALTER DATABASE [SierraMelladoDB] SET  READ_WRITE 
GO
