-- VolunManager - Diseño inicial de base de datos
-- Issue #2: Diseñar base de datos del sistema

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255)
);

CREATE TABLE Voluntarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL UNIQUE,
    Telefono NVARCHAR(20),
    RolId INT NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Voluntarios_Roles
        FOREIGN KEY (RolId) REFERENCES Roles(Id)
);

CREATE TABLE Jornadas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500),
    Fecha DATE NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    Lugar NVARCHAR(200) NOT NULL,
    Estado NVARCHAR(30) NOT NULL DEFAULT 'Planificada'
);

CREATE TABLE Tareas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500),
    JornadaId INT NOT NULL,
    VoluntarioId INT NULL,
    Estado NVARCHAR(30) NOT NULL DEFAULT 'Pendiente',

    CONSTRAINT FK_Tareas_Jornadas
        FOREIGN KEY (JornadaId) REFERENCES Jornadas(Id),

    CONSTRAINT FK_Tareas_Voluntarios
        FOREIGN KEY (VoluntarioId) REFERENCES Voluntarios(Id)
);

CREATE TABLE Asistencias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VoluntarioId INT NOT NULL,
    JornadaId INT NOT NULL,
    HoraEntrada DATETIME NULL,
    HoraSalida DATETIME NULL,
    HorasTrabajadas DECIMAL(5,2) NULL,
    Presente BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_Asistencias_Voluntarios
        FOREIGN KEY (VoluntarioId) REFERENCES Voluntarios(Id),

    CONSTRAINT FK_Asistencias_Jornadas
        FOREIGN KEY (JornadaId) REFERENCES Jornadas(Id)
);

CREATE TABLE ReportesHoras (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VoluntarioId INT NOT NULL,
    TotalHoras DECIMAL(6,2) NOT NULL DEFAULT 0,
    FechaGeneracion DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_ReportesHoras_Voluntarios
        FOREIGN KEY (VoluntarioId) REFERENCES Voluntarios(Id)
);