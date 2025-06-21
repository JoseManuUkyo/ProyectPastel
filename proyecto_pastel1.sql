-- Crear la base de datos
CREATE DATABASE IF NOT EXISTS proyecto_pastel;
USE proyecto_pastel;

-- Tabla de usuarios
CREATE TABLE usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    contraseña VARCHAR(255) NOT NULL,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    tipo_usuario ENUM('Administrador', 'Empleado', 'Cliente') NOT NULLs
);

-- Tabla de postres
CREATE TABLE postres (
    id_postre INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio_base DECIMAL(10,2), -- precio sugerido o base
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabla de inventario (ingredientes disponibles)
CREATE TABLE inventario (
    id_ingrediente INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    cantidad DECIMAL(10,2) NOT NULL, -- cantidad disponible
    unidad ENUM('g', 'kg', 'ml', 'l', 'unidad') NOT NULL,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Tabla de recetas (cada postre tiene una receta)
CREATE TABLE recetas (
    id_receta INT AUTO_INCREMENT PRIMARY KEY,
    id_postre INT NOT NULL,
    descripcion TEXT,
    FOREIGN KEY (id_postre) REFERENCES postres(id_postre)
);

-- Ingredientes por receta (qué y cuánto se usa)
CREATE TABLE ingredientes_receta (
    id_ingrediente_receta INT AUTO_INCREMENT PRIMARY KEY,
    id_receta INT NOT NULL,
    id_ingrediente INT NOT NULL,
    cantidad DECIMAL(10,2) NOT NULL,
    unidad ENUM('g', 'kg', 'ml', 'l', 'unidad') NOT NULL,
    FOREIGN KEY (id_receta) REFERENCES recetas(id_receta),
    FOREIGN KEY (id_ingrediente) REFERENCES inventario(id_ingrediente)
);

-- Tabla de ventas (registro de pedidos o compras)
CREATE TABLE ventas (
    id_venta INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT, -- opcional si hay usuario registrado
    fecha_venta TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario)
);

-- Detalle de cada postre vendido
CREATE TABLE detalle_venta (
    id_detalle INT AUTO_INCREMENT PRIMARY KEY,
    id_venta INT NOT NULL,
    id_postre INT NOT NULL,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) GENERATED ALWAYS AS (cantidad * precio_unitario) STORED,
    FOREIGN KEY (id_venta) REFERENCES ventas(id_venta),
    FOREIGN KEY (id_postre) REFERENCES postres(id_postre)
);

-- Datos de las tablas

INSERT INTO usuarios (nombre, correo, contraseña)
VALUES 
('Ana Pastelera', 'ana@pasteleria.com', 'clave123'),
('Luis Dulce', 'luis@correo.com', 'dulce456');

INSERT INTO postres (nombre, descripcion, precio_base)
VALUES
('Pastel de Chocolate', 'Delicioso pastel con cobertura de chocolate', 150.00),
('Tarta de Fresa', 'Tarta con crema y fresas naturales', 120.00);

INSERT INTO inventario (nombre, cantidad, unidad)
VALUES
('Harina', 20.0, 'kg'),
('Azúcar', 10.0, 'kg'),
('Huevos', 100.0, 'unidad'),
('Leche', 15.0, 'l'),
('Chocolate', 5.0, 'kg'),
('Fresas', 3.0, 'kg'),
('Crema', 4.0, 'l');


INSERT INTO recetas (id_postre, descripcion)
VALUES (1, 'Receta clásica de pastel de chocolate');

INSERT INTO recetas (id_postre, descripcion)
VALUES (2, 'Tarta con base de masa, crema y fresas');

-- Pastel de Chocolate (receta 1)
INSERT INTO ingredientes_receta (id_receta, id_ingrediente, cantidad, unidad)
VALUES
(1, 1, 0.5, 'kg'),  -- Harina
(1, 2, 0.3, 'kg'),  -- Azúcar
(1, 3, 4, 'unidad'),-- Huevos
(1, 4, 0.5, 'l'),   -- Leche
(1, 5, 0.4, 'kg');  -- Chocolate

-- Tarta de Fresa (receta 2)
INSERT INTO ingredientes_receta (id_receta, id_ingrediente, cantidad, unidad)
VALUES
(2, 1, 0.4, 'kg'),  -- Harina
(2, 2, 0.2, 'kg'),  -- Azúcar
(2, 3, 3, 'unidad'),-- Huevos
(2, 7, 0.5, 'l'),   -- Crema
(2, 6, 0.3, 'kg');  -- Fresas

-- Venta hecha por Ana
INSERT INTO ventas (id_usuario)
VALUES (1);

-- Detalle: compró 2 Pasteles de Chocolate a 150.00 c/u
INSERT INTO detalle_venta (id_venta, id_postre, cantidad, precio_unitario)
VALUES (1, 1, 2, 150.00);

-- Detalle: compró 1 Tarta de Fresa a 120.00
INSERT INTO detalle_venta (id_venta, id_postre, cantidad, precio_unitario)
VALUES (1, 2, 1, 120.00);

-- Agregar columna a la tabla postres
ALTER TABLE postres
ADD COLUMN cantidad_disponible INT NOT NULL DEFAULT 0;

-- Tabla para registrar movimientos del inventario
CREATE TABLE movimientos_inventario (
    id_movimiento INT AUTO_INCREMENT PRIMARY KEY,
    id_ingrediente INT NOT NULL,
    id_usuario INT NOT NULL,
    tipo_movimiento ENUM('entrada', 'salida') NOT NULL,
    motivo ENUM('ajuste', 'descompuesto', 'producción', 'otro') NOT NULL,
    cantidad DECIMAL(10,2) NOT NULL,
    fecha_movimiento TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    descripcion TEXT,
    FOREIGN KEY (id_ingrediente) REFERENCES inventario(id_ingrediente),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario)
);