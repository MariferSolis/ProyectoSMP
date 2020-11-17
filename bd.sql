USE [SMP]
GO
/****** Object:  Table [dbo].[AreaDeMaquina]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AreaDeMaquina](
	[IdArea] [int] NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[IdBitacora] [int] NOT NULL,
	[Controlador] [varchar](50) NOT NULL,
	[Metodo] [varchar](50) NOT NULL,
	[Mensaje] [varchar](50) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[IdBitacora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calendario]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calendario](
	[IdEvento] [int] NOT NULL,
	[Asunto] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[Inicia] [datetime] NULL,
	[Finaliza] [datetime] NULL,
	[TodoElDia] [bit] NULL,
	[Color] [nchar](10) NULL,
 CONSTRAINT [PK_Calendario] PRIMARY KEY CLUSTERED 
(
	[IdEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Canton]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Canton](
	[Provincia] [char](1) NOT NULL,
	[Canton] [char](2) NOT NULL,
	[Nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Provincia] ASC,
	[Canton] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cumplimiento]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cumplimiento](
	[IdCumplimiento] [int] NOT NULL,
	[IdMantenimiento] [int] NOT NULL,
	[Comienza] [datetime] NOT NULL,
	[Finaliza] [datetime] NOT NULL,
	[Fecha] [datetime] NULL,
	[Estado] [bit] NULL,
	[Detalles] [varchar](500) NULL,
	[Color] [varchar](100) NULL,
 CONSTRAINT [PK_Cumplimiento] PRIMARY KEY CLUSTERED 
(
	[IdCumplimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Distrito]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distrito](
	[Provincia] [char](1) NOT NULL,
	[Canton] [char](2) NOT NULL,
	[Distrito] [char](2) NOT NULL,
	[Nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Provincia] ASC,
	[Canton] ASC,
	[Distrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventarioDeRepuestos]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventarioDeRepuestos](
	[IdRepuesto] [int] NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Cantidad] [int] NULL,
	[Requisición] [int] NULL,
	[Maximos] [int] NULL,
	[Minimos] [int] NULL,
	[Tipo] [varchar](50) NULL,
	[Almacen] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRepuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mantenimiento]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mantenimiento](
	[IdMantenimiento] [int] NOT NULL,
	[IdMaquina] [int] NOT NULL,
	[Seccion] [varchar](50) NULL,
	[NumeroOperacion] [int] NULL,
	[NombreOperacion] [varchar](50) NULL,
	[Frecuencia] [int] NULL,
	[IdRol] [int] NULL,
	[IdUsuario] [int] NULL,
	[IdRepuesto] [int] NULL,
	[Detalles] [varchar](max) NULL,
	[URLArchivo] [varchar](max) NULL,
 CONSTRAINT [PK_Mantenimiento] PRIMARY KEY CLUSTERED 
(
	[IdMantenimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Maquina]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maquina](
	[IdMaquina] [int] NOT NULL,
	[NombreMaquina] [varchar](50) NOT NULL,
	[IdTipoSistema] [int] NULL,
	[IdArea] [int] NULL,
	[Codigo] [varchar](50) NULL,
	[Modelo] [varchar](50) NULL,
	[Proceso] [varchar](50) NULL,
	[Cadencia] [int] NULL,
	[Descripcion] [varchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Maquina] PRIMARY KEY CLUSTERED 
(
	[IdMaquina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParoDeMaquina]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParoDeMaquina](
	[IdParo] [int] NOT NULL,
	[IdMaquina] [int] NOT NULL,
	[IdMantenimiento] [int] NULL,
	[Tipo] [varchar](200) NOT NULL,
	[Descripcion] [varchar](max) NOT NULL,
	[FechaComienza] [datetime] NOT NULL,
	[FechaFin] [datetime] NULL,
 CONSTRAINT [PK_ParoDeMaquina] PRIMARY KEY CLUSTERED 
(
	[IdParo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincia]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincia](
	[Provincia] [char](1) NOT NULL,
	[Nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Provincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDeIdentificacion]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDeIdentificacion](
	[IdTipoIdentificacion] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoIdentificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDeSistemaDeMaquina]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDeSistemaDeMaquina](
	[IdTipoSistema] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoSistema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 17/11/2020 16:50:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[IdTipoDeIdentificacion] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Correo] [varchar](200) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[TipoCarga] [varchar](50) NOT NULL,
	[Provincia] [char](1) NULL,
	[Canton] [char](2) NULL,
	[Distrito] [char](2) NULL,
	[IdRol] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
	[token_recovery] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Canton]  WITH CHECK ADD FOREIGN KEY([Provincia])
REFERENCES [dbo].[Provincia] ([Provincia])
GO
ALTER TABLE [dbo].[Canton]  WITH CHECK ADD FOREIGN KEY([Provincia])
REFERENCES [dbo].[Provincia] ([Provincia])
GO
ALTER TABLE [dbo].[Cumplimiento]  WITH CHECK ADD  CONSTRAINT [FK_Cumplimiento_Mantenimiento] FOREIGN KEY([IdMantenimiento])
REFERENCES [dbo].[Mantenimiento] ([IdMantenimiento])
GO
ALTER TABLE [dbo].[Cumplimiento] CHECK CONSTRAINT [FK_Cumplimiento_Mantenimiento]
GO
ALTER TABLE [dbo].[Distrito]  WITH CHECK ADD FOREIGN KEY([Provincia], [Canton])
REFERENCES [dbo].[Canton] ([Provincia], [Canton])
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_InventarioDeRepuestos] FOREIGN KEY([IdRepuesto])
REFERENCES [dbo].[InventarioDeRepuestos] ([IdRepuesto])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_InventarioDeRepuestos]
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Maquina] FOREIGN KEY([IdMaquina])
REFERENCES [dbo].[Maquina] ([IdMaquina])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_Maquina]
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_Rol]
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_Usuario]
GO
ALTER TABLE [dbo].[Maquina]  WITH CHECK ADD  CONSTRAINT [FK_Maquina_AreaDeMaquina] FOREIGN KEY([IdArea])
REFERENCES [dbo].[AreaDeMaquina] ([IdArea])
GO
ALTER TABLE [dbo].[Maquina] CHECK CONSTRAINT [FK_Maquina_AreaDeMaquina]
GO
ALTER TABLE [dbo].[Maquina]  WITH CHECK ADD  CONSTRAINT [FK_Maquina_TipoDeSistemaDeMaquina] FOREIGN KEY([IdTipoSistema])
REFERENCES [dbo].[TipoDeSistemaDeMaquina] ([IdTipoSistema])
GO
ALTER TABLE [dbo].[Maquina] CHECK CONSTRAINT [FK_Maquina_TipoDeSistemaDeMaquina]
GO
ALTER TABLE [dbo].[ParoDeMaquina]  WITH CHECK ADD  CONSTRAINT [FK_ParoDeMaquina_Mantenimiento] FOREIGN KEY([IdMantenimiento])
REFERENCES [dbo].[Mantenimiento] ([IdMantenimiento])
GO
ALTER TABLE [dbo].[ParoDeMaquina] CHECK CONSTRAINT [FK_ParoDeMaquina_Mantenimiento]
GO
ALTER TABLE [dbo].[ParoDeMaquina]  WITH CHECK ADD  CONSTRAINT [FK_ParoDeMaquina_Maquina] FOREIGN KEY([IdMaquina])
REFERENCES [dbo].[Maquina] ([IdMaquina])
GO
ALTER TABLE [dbo].[ParoDeMaquina] CHECK CONSTRAINT [FK_ParoDeMaquina_Maquina]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_TipoDeIdentificacion] FOREIGN KEY([IdTipoDeIdentificacion])
REFERENCES [dbo].[TipoDeIdentificacion] ([IdTipoIdentificacion])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_TipoDeIdentificacion]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCalendario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarCalendario]
@IdEvento as int,
@Asunto as varchar(100),
@Descripcion as varchar(300),
@Comienza as Datetime,
@Fin as DateTime,
@Color as varchar(10),
@TodoDia as bit

AS
BEGIN
    Update Calendario set
            Asunto=@Asunto
           ,Descripcion=@Descripcion
           ,Comienza=@Comienza
           ,Fin=@Fin
           ,Color=@Color
           ,TodoDia=@TodoDia
		where IdEvento=@IdEvento
	
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarAreaDeMaquina]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AgregarAreaDeMaquina]
@Nombre varchar(20),
@Descripcion varchar(50),
@Estado bit
AS
BEGIN

Declare @Codigo int

Select @Codigo=isnull(MAX(IdArea),0)+1 from AreaDeMaquina

	INSERT INTO AreaDeMaquina
           (IdArea
           ,Nombre
           ,Descripcion
           ,Estado
		   )
     VALUES
           (@Codigo
           ,@Nombre
           ,@Descripcion
           ,@Estado
		   )

	Select @Codigo
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarBitacora]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AgregarBitacora]
@Controlador varchar(50),
@Metodo varchar(50),
@Mensaje varchar(50),
@IdUsuario int,
@Fecha datetime,
@Tipo varchar(50)
AS
BEGIN

Declare @Codigo int

Select @Codigo=isnull(MAX(IdBitacora),0)+1 from Bitacora

	INSERT INTO Bitacora
           (IdBitacora
           ,Controlador
           ,Metodo 
           ,Mensaje
           ,Tipo
           ,Fecha
           ,IdUsuario
		   )
     VALUES
           (@Codigo
           ,@Controlador
           ,@Metodo 
           ,@Mensaje
           ,@Tipo
           ,@Fecha
           ,@IdUsuario
		   )

	Select @Codigo
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarCalendario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarCalendario]
@Asunto as varchar(100),
@Descripcion as varchar(300),
@Inicia as Datetime,
@Fin as DateTime,
@Color as varchar(10),
@TodoDia as bit

AS
BEGIN
Declare @Codigo int
Select @Codigo=isnull(MAX(IdEvento),0)+1 from Calendario

    INSERT INTO Calendario
           (IdEvento
		   ,Asunto
           ,Descripcion
           ,Inicia
           ,Finaliza
           ,Color
           ,TodoElDia)
     VALUES
           (@Codigo
           ,@Asunto
           ,@Descripcion
		   ,@Inicia
           ,@Fin
           ,@Color
		   ,@TodoDia)
	
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarCumplimiento]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AgregarCumplimiento]
@IdMantenimiento int,
@Comienza datetime,
@Finaliza datetime,
@Color varchar(100)

AS
BEGIN

Declare @Codigo int

Select @Codigo=isnull(MAX(IdCumplimiento),0)+1 from Cumplimiento

	INSERT INTO Cumplimiento
           (IdCumplimiento
		   ,IdMantenimiento
           ,Comienza
           ,Finaliza 
           ,Color
		   )
     VALUES
           (@Codigo
           ,@IdMantenimiento
		   ,@Comienza
           ,@Finaliza          
           ,@Color
		   )
	Select @Codigo
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarInventarioDeRepuestos]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AgregarInventarioDeRepuestos]
@Nombre varchar(50),
@Cantidad int,
@Requisición int,
@Maximos int,
@Minimos int,
@Tipo varchar(50),
@Almacen varchar(50)

AS
BEGIN

Declare @Codigo int

Select @Codigo=isnull(MAX(IdRepuesto),0)+1 from InventarioDeRepuestos

	INSERT INTO InventarioDeRepuestos
           (IdRepuesto
           ,Nombre
           ,Cantidad 
           ,Requisición
		   ,Maximos
		   ,Minimos
		   ,Tipo
		   ,Almacen
		   )
     VALUES
           (@Codigo
           ,@Nombre
           ,@Cantidad
           ,@Requisición
		   ,@Maximos
		   ,@Minimos
		   ,@Tipo
		   ,@Almacen
		   )

	Select @Codigo
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarMantenimiento]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[AgregarMantenimiento]
@IdMaquina int,
@Seccion varchar(50),
@NumeroOperacion int,
@NombreOperacion varchar(50),
@Frecuencia int,
@IdRol int,
@IdUsuario int,
@IdRepuesto int,
@Detalles varchar(MAX),
@URLArchivo varchar(max)

AS
BEGIN

Declare @Codigo int

Select @Codigo=isnull(MAX(IdMantenimiento),0)+1 from Mantenimiento

	INSERT INTO Mantenimiento
           (IdMantenimiento
		   ,IdMaquina
		   ,Seccion
           ,NumeroOperacion
           ,NombreOperacion
		   ,Frecuencia
		   ,IdRol
		   ,IdUsuario
		   ,IdRepuesto
		   ,Detalles
		   ,URLArchivo
		   )
     VALUES
           (@Codigo
		   ,@IdMaquina
		   ,@Seccion
           ,@NumeroOperacion
           ,@NombreOperacion
           ,@Frecuencia
		   ,@IdRol
		   ,@IdUsuario
		   ,@IdRepuesto
		   ,@Detalles
		   ,@URLArchivo
		   )

	Select @Codigo
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarMaquina]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[AgregarMaquina]
@NombreMaquina varchar(50),
@IdTipoSistema int,
@IdArea int,
@Codigo varchar(50),
@Modelo varchar(50),
@Proceso varchar(50),
@Cadencia int,
@Descripcion varchar(50)

AS
BEGIN

Declare @Codigo1 int

Select @Codigo1=isnull(MAX(IdMaquina),0)+1 from Maquina

	INSERT INTO Maquina
           (IdMaquina
           ,NombreMaquina
           ,IdTipoSistema
           ,IdArea
		   ,Codigo
		   ,Modelo
		   ,Proceso
		   ,Cadencia
		   ,Descripcion
		   )
     VALUES
           (@Codigo1
           ,@NombreMaquina
           ,@IdTipoSistema
           ,@IdArea
		   ,@Codigo
		   ,@Modelo
		   ,@Proceso
		   ,@Cadencia
		   ,@Descripcion
		   )

	Select @Codigo1
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarParoDeMaquina]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[AgregarParoDeMaquina]
@IdMaquina int,
@IdMantenimiento int,
@Tipo varchar(200),
@Descripcion varchar(max),
@FechaComienza datetime,
@FechaFin datetime

AS
BEGIN

Declare @Codigo int

Select @Codigo=isnull(MAX(IdParo),0)+1 from ParoDeMaquina

	INSERT INTO ParoDeMaquina
           (IdParo
           ,IdMaquina
           ,IdMantenimiento
           ,Tipo
           ,Descripcion
		   ,FechaComienza
		   ,FechaFin
		   
		   
		   )
     VALUES
           (@Codigo
           ,@IdMaquina
           ,@IdMantenimiento
           ,@Tipo
           ,@Descripcion
		   ,@FechaComienza
		   ,@FechaFin
		   )

	Select @Codigo
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarPlanMantenimiento]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[AgregarPlanMantenimiento]
@IdMantenimiento int,
@Comienza datetime,
@Finaliza datetime,
@Color varchar(10)

AS
BEGIN

Declare @Codigo int

Select @Codigo=isnull(MAX(IdPlan),0)+1 from PlanMantenimiento

	INSERT INTO PlanMantenimiento
           (IdPlan
		   ,IdMantenimiento
		   ,Comienza
		   ,Finaliza
		   ,Color
		   )
     VALUES
           (@Codigo
		   ,@IdMantenimiento
		   ,@Comienza
		   ,@Finaliza
		   ,@Color
		   )

	Select @Codigo
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarTipoDeIdentificacion]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AgregarTipoDeIdentificacion]
@Descripcion varchar(50),
@Estado bit

AS
BEGIN

Declare @Codigo1 int

Select @Codigo1=isnull(MAX(IdTipoIdentificacion),0)+1 from TipoDeIdentificacion

	INSERT INTO TipoDeIdentificacion
           (IdTipoIdentificacion
           ,Descripcion
           ,Estado
		   )
     VALUES
           (@Codigo1
           ,@Descripcion
           ,@Estado
		   )

	Select @Codigo1
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarTipoDeSistemaDeMaquina]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AgregarTipoDeSistemaDeMaquina]
@Nombre varchar(50),
@Descripcion varchar(500),
@Estado bit

AS
BEGIN

Declare @Codigo1 int

Select @Codigo1=isnull(MAX(IdTipoSistema),0)+1 from TipoDeSistemaDeMaquina

	INSERT INTO TipoDeSistemaDeMaquina
           (IdTipoSistema
		   ,Nombre
           ,Descripcion
           ,Estado
		   )
     VALUES
           (@Codigo1
		   ,@Nombre
           ,@Descripcion
           ,@Estado
		   )

	Select @Codigo1
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarUsuario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[AgregarUsuario]
@Identificacion varchar(50),
@IdTipoDeIdentificacion int,
@Nombre varchar(50),
@Apellidos varchar(50),
@Correo varchar(200),
@Password varchar(50),
@TipoCarga varchar(50),
@Provincia char(1),
@Canton char(2),
@Distrito char(2),
@IdRol int,
@Estado bit

AS
BEGIN

Declare @Codigo1 int

Select @Codigo1=isnull(MAX(IdUsuario),0)+1 from Usuario

	INSERT INTO Usuario
           (IdUsuario
           ,Identificacion
           ,IdTipoDeIdentificacion
		   ,Nombre
		   ,Apellidos
		   ,Correo
		   ,Password
		   ,TipoCarga
		   ,Provincia
		   ,Canton
		   ,Distrito
		   ,IdRol
		   ,Estado
		   )
     VALUES
           (@Codigo1
           ,@Identificacion
           ,@IdTipoDeIdentificacion
		   ,@Nombre
		   ,@Apellidos
		   ,@Correo
		   ,@Password
		   ,@TipoCarga
		   ,@Provincia
		   ,@Canton
		   ,@Distrito
		   ,@IdRol
		   ,@Estado
		   )

	Select @Codigo1
END
GO
/****** Object:  StoredProcedure [dbo].[Cantones]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Cantones]
@Provincia char(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Canton where Provincia=@Provincia order by Provincia,Canton
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarCalendario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[ConsultarCalendario]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT * From Calendario
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarCumplixUsuario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ConsultarCumplixUsuario]
@IdUsuario int

AS
BEGIN

Select c.IdCumplimiento, ma.NombreMaquina, mt.Seccion, mt.NombreOperacion, mt.NumeroOperacion, mt.Frecuencia, 
ir.Nombre, mt.Detalles 'Detalles1', mt.URLArchivo, c.Comienza, c.Finaliza, c.Fecha,
c.Estado, c.Detalles 'Detalles2', c.Color, c.IdMantenimiento  
from Cumplimiento c, Mantenimiento mt, Maquina ma, InventarioDeRepuestos ir
where mt.IdUsuario = @IdUsuario 
and c.IdMantenimiento = mt.IdMantenimiento 
and mt.IdMaquina = ma.IdMaquina 
and mt.IdRepuesto = ir.IdRepuesto
and c.Estado is null

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarRolxUsuario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ConsultarRolxUsuario]
	@correo varchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	select r.Descripcion from Rol r
	inner join Usuario u on r.IdRol=u.IdRol
	where u.Correo=@correo	
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarUnUsuarios]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ConsultarUnUsuarios]
@Id int
AS
BEGIN

Select u.IdUsuario, u.Identificacion, tp.Descripcion 'Tipo Identificacion', u.Nombre +' '+u.Apellidos 'Nombre', u.Correo, u.TipoCarga,
p.Nombre 'Provincia', c.Nombre 'Canton', d.Nombre 'Distrito', r.Descripcion 'Rol', u.Estado
from Usuario u, TipoDeIdentificacion tp, Provincia p, Canton c, Distrito d, Rol r
where u.Provincia = p.Provincia and u.IdTipoDeIdentificacion = tp.IdTipoIdentificacion 
and u.Provincia = c.Provincia and u.Canton = c.Canton and d.Provincia = u.Provincia 
and d.Canton = u.Canton and d.Distrito = u.Distrito and u.IdRol = r.IdRol and @Id = u.IdUsuario

END


GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarios]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ConsultarUsuarios]

AS
BEGIN

Select u.IdUsuario, u.Identificacion, tp.Descripcion 'Tipo Identificacion', u.Nombre +' '+u.Apellidos 'Nombre', u.Correo, u.TipoCarga,
p.Nombre 'Provincia', c.Nombre 'Canton', d.Nombre 'Distrito', r.Descripcion 'Rol', u.Estado
from Usuario u, TipoDeIdentificacion tp, Provincia p, Canton c, Distrito d, Rol r
where u.Provincia = p.Provincia and u.IdTipoDeIdentificacion = tp.IdTipoIdentificacion 
and u.Provincia = c.Provincia and u.Canton = c.Canton and d.Provincia = u.Provincia 
and d.Canton = u.Canton and d.Distrito = u.Distrito and u.IdRol = r.IdRol

END


GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuariosxRol]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ConsultarUsuariosxRol]
@IdRol int
AS
BEGIN

SET NOCOUNT ON;
Select *
from Usuario
where IdRol = @IdRol

END
GO
/****** Object:  StoredProcedure [dbo].[CumpNoCump]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[CumpNoCump]
as
select top 1 (select count(IdCumplimiento) 
from Cumplimiento as a
where a.estado = '0') , (select count(IdCumplimiento) 
from Cumplimiento
where estado = '1') from Cumplimiento
GO
/****** Object:  StoredProcedure [dbo].[Distritos]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Distritos]
@Provincia char(1),
@Canton char(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Distrito where Provincia=@Provincia and Canton=@Canton
	order by Provincia,Canton,Distrito
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarCalendario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[EliminarCalendario]
@IdEvento as int

AS
BEGIN
   Delete Calendario
		where IdEvento=@IdEvento
	
END
GO
/****** Object:  StoredProcedure [dbo].[ExisteCodigo]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[ExisteCodigo]
@Codigo as varchar(50)

AS
BEGIN

    -- Insert statements for procedure here
	SELECT * from Maquina where Codigo=@Codigo
END


GO
/****** Object:  StoredProcedure [dbo].[ExisteCorreo]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ExisteCorreo]
@Correo as varchar(200)

AS
BEGIN

    -- Insert statements for procedure here
	SELECT * from Usuario where Correo=@Correo
END


GO
/****** Object:  StoredProcedure [dbo].[ExisteUsuario]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ExisteUsuario]
@Correo as varchar(200),
@Password as varchar(100)
AS
BEGIN

    -- Insert statements for procedure here
	SELECT * from Usuario where Correo=@Correo and Password=@Password
END


GO
/****** Object:  StoredProcedure [dbo].[InformesBasic]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[InformesBasic] 
@CantidadMaquina float out,
@CantidadMantenimiento float out,
@MantenimientoCumplido float out,
@CantidadParo float out

as
set @CantidadMaquina = (select count(idMaquina) as CantidadMaquina from Maquina) 
set @CantidadMantenimiento =(select count(idMantenimiento) as CantidadMantenimiento from MantenimientoDeMaquina)
set @MantenimientoCumplido =(select count(IdMantenimiento) as MantenimientoCumplido from CumplimientoMantenimiento where estado = 'true')
set @CantidadParo =(select count(idparo) as CantidadParo from ParoDeMaquina)



GO
/****** Object:  StoredProcedure [dbo].[MantenimientoxMaquina]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[MantenimientoxMaquina]
as
select count(man.IdMantenimiento) as Mantenimiento, maqu.NombreMaquina
from Mantenimiento as Man
join Maquina as Maqu on Maqu.IdMaquina = Man.IdMaquina
group by man.IdMaquina,maqu.NombreMaquina
order by count(2) desc

GO
/****** Object:  StoredProcedure [dbo].[ParoxMaquina]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[ParoxMaquina]
as
select count(man.IdParo) as Paro, maqu.NombreMaquina
from ParoDeMaquina as man
join Maquina as Maqu on Maqu.IdMaquina = Man.IdMaquina
group by man.IdMaquina,maqu.NombreMaquina
order by count(2) desc
GO
/****** Object:  StoredProcedure [dbo].[Provincias]    Script Date: 17/11/2020 16:50:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Provincias]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Provincia order by Provincia
END
GO
