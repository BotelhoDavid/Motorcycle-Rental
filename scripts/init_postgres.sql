\c "Moto_Rental";

-- ============================
-- TABLE: Drivers
-- ============================
CREATE TABLE IF NOT EXISTS public."Drivers" (
    "Id" uuid NOT NULL,
    "Name" varchar(50) NOT NULL,
    "CNPJ" varchar(25) NOT NULL,
    "Birth" timestamptz NOT NULL,
    "CNHNumber" varchar(15) NOT NULL,
    "CNHtype" varchar(3) NOT NULL,
    "CNHImage" uuid NOT NULL,
    CONSTRAINT "PK_Drivers" PRIMARY KEY ("Id")
);

-- Indexes for Drivers
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Drivers_CNHNumber"
    ON public."Drivers" ("CNHNumber");

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Drivers_CNPJ"
    ON public."Drivers" ("CNPJ");


-- ============================
-- TABLE: Motos
-- ============================
CREATE TABLE IF NOT EXISTS public."Motos" (
    "Id" uuid NOT NULL,
    "Year" integer NOT NULL,
    "Model" varchar(25) NOT NULL,
    "Plate" varchar(10) NOT NULL,
    "CreatedDate" timestamptz NOT NULL,
    "ModifiedDate" timestamptz NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_Motos" PRIMARY KEY ("Id")
);

-- Indexes for Motos
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Motos_Plate"
    ON public."Motos" ("Plate");


-- ============================
-- TABLE: Rents
-- ============================
CREATE TABLE IF NOT EXISTS public."Rents" (
    "Id" uuid NOT NULL,
    "Driver_id" uuid NOT NULL,
    "Moto_id" uuid NOT NULL,
    "Initio_date" timestamptz NOT NULL,
    "End_date" timestamptz NOT NULL,
    "Forecast_end_date" timestamptz NOT NULL,
    "Return_date" timestamptz NOT NULL,
    "Plan" integer NOT NULL,
    "CreatedDate" timestamptz NOT NULL,
    "ModifiedDate" timestamptz NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_Rents" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Rents_Drivers_Driver_id"
        FOREIGN KEY ("Driver_id") REFERENCES public."Drivers" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Rents_Motos_Moto_id"
        FOREIGN KEY ("Moto_id") REFERENCES public."Motos" ("Id") ON DELETE RESTRICT
);

-- Indexes for Rents
CREATE INDEX IF NOT EXISTS "IX_Rents_Driver_id"
    ON public."Rents" ("Driver_id");

CREATE INDEX IF NOT EXISTS "IX_Rents_Moto_id"
    ON public."Rents" ("Moto_id");
