USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'agrovision_db')
BEGIN
    CREATE DATABASE agrovision_db;
END
GO

USE agrovision_db;
GO

-- 1. ORGANIZATIONS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[organizations]') AND type in (N'U'))
BEGIN
    CREATE TABLE organizations (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        name NVARCHAR(150) NOT NULL,
        type NVARCHAR(100) NOT NULL
    );
END
GO

-- 2. FARMS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[farms]') AND type in (N'U'))
BEGIN
    CREATE TABLE farms (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        organization_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES organizations(id),
        name NVARCHAR(150) NOT NULL
    );
END
GO

-- 3. PLOTS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[plots]') AND type in (N'U'))
BEGIN
    CREATE TABLE plots (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        farm_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES farms(id),
        code NVARCHAR(50) NOT NULL
    );
END
GO

-- 4. PROFILES (Usuarios de la API)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[profiles]') AND type in (N'U'))
BEGIN
    CREATE TABLE profiles (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        organization_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES organizations(id),
        full_name NVARCHAR(150) NOT NULL,
        role NVARCHAR(50) NOT NULL,
        email VARCHAR(100) NOT NULL UNIQUE,      -- Requerido para /api/auth/login
        password_hash VARCHAR(255) NOT NULL       -- Requerido para validación de acceso
    );
END
GO

-- 5. TELEMETRIES (Mantenida para soportar el Endpoint 3 offline /api/telemetry)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[telemetries]') AND type in (N'U'))
BEGIN
    CREATE TABLE telemetries (
        id INT IDENTITY(1,1) PRIMARY KEY,
        timestamp DATETIMEOFFSET NOT NULL,
        pest_type NVARCHAR(100) NOT NULL,
        confidence DECIMAL(5,4) NOT NULL
    );
END
GO

-- 6. INSPECTIONS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[inspections]') AND type in (N'U'))
BEGIN
    CREATE TABLE inspections (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        plot_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES plots(id),
        inspector_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES profiles(id),
        inspection_date DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
        sync_status VARCHAR(50) NOT NULL DEFAULT 'synced'
    );
END
GO

-- 7. IMAGES
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[images]') AND type in (N'U'))
BEGIN
    CREATE TABLE images (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        inspection_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES inspections(id),
        file_uri NVARCHAR(MAX) NOT NULL,
        mime_type VARCHAR(50) NOT NULL,
        width INT NOT NULL,
        height INT NOT NULL,
        device_id VARCHAR(100) NOT NULL
    );
END
GO

-- 8. DISEASE CATALOG
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[disease_catalog]') AND type in (N'U'))
BEGIN
    CREATE TABLE disease_catalog (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        common_name NVARCHAR(150) NOT NULL
    );
END
GO

-- 9. OBSERVATIONS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[observations]') AND type in (N'U'))
BEGIN
    CREATE TABLE observations (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        inspection_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES inspections(id),
        disease_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES disease_catalog(id),
        severity_level INT NOT NULL,
        incidence_percent DECIMAL(5,2) NOT NULL,
        source_type VARCHAR(50) NOT NULL
    );
END
GO

-- 10. INFERENCE RESULTS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[inference_results]') AND type in (N'U'))
BEGIN
    CREATE TABLE inference_results (
        id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        image_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES images(id),
        predicted_disease_id UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES disease_catalog(id),
        model_name VARCHAR(100) NOT NULL,
        model_version VARCHAR(50) NOT NULL,
        confidence DECIMAL(5,4) NOT NULL,
        top_k_json NVARCHAR(MAX) NULL
    );
END
GO

-- ==========================================================
-- SEMILLAS / DATOS DEMO (Sincronizados con backend_api_reference)
-- ==========================================================

-- Insertar Organización y Finca por defecto
DECLARE @OrgId UNIQUEIDENTIFIER = '11111111-1111-1111-1111-111111111111';
DECLARE @FarmId UNIQUEIDENTIFIER = '22222222-2222-2222-2222-222222222222';
DECLARE @PlotId UNIQUEIDENTIFIER = '33333333-3333-3333-3333-333333333333';

IF NOT EXISTS (SELECT 1 FROM organizations WHERE id = @OrgId)
    INSERT INTO organizations (id, name, type) VALUES (@OrgId, 'AgroVision Corp', 'Coffee Enterprise');

IF NOT EXISTS (SELECT 1 FROM farms WHERE id = @FarmId)
    INSERT INTO farms (id, organization_id, name) VALUES (@FarmId, @OrgId, 'Finca Central Demo');

IF NOT EXISTS (SELECT 1 FROM plots WHERE id = @PlotId)
    INSERT INTO plots (id, farm_id, code) VALUES (@PlotId, @FarmId, 'plot-001');

-- Insertar Usuarios con IDs fijos para pruebas de autenticación de la PWA
IF NOT EXISTS (SELECT 1 FROM profiles WHERE email = 'admin@agrovision.co')
    INSERT INTO profiles (id, organization_id, full_name, role, email, password_hash) 
    VALUES ('A0000000-0000-0000-0000-000000000000', @OrgId, 'Admin Demo', 'admin', 'admin@agrovision.co', 'admin2026');

IF NOT EXISTS (SELECT 1 FROM profiles WHERE email = 'inspector@agrovision.co')
    INSERT INTO profiles (id, organization_id, full_name, role, email, password_hash) 
    VALUES ('B0000000-0000-0000-0000-000000000000', @OrgId, 'Inspector Demo', 'inspector', 'inspector@agrovision.co', 'agro2026');

-- Insertar Catálogo de Enfermedades con IDs conocidos
IF NOT EXISTS (SELECT 1 FROM disease_catalog WHERE common_name = 'Broca del Café')
    INSERT INTO disease_catalog (id, common_name) VALUES ('C1111111-1111-1111-1111-111111111111', 'Broca del Café');

IF NOT EXISTS (SELECT 1 FROM disease_catalog WHERE common_name = 'Roya del Cafeto')
    INSERT INTO disease_catalog (id, common_name) VALUES ('C2222222-2222-2222-2222-222222222222', 'Roya del Cafeto');
GO
