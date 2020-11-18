-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 19-11-2020 a las 00:27:30
-- Versión del servidor: 10.4.14-MariaDB
-- Versión de PHP: 7.4.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `tienda`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `PeliculaAddOrEdit` (`_idpelicula` INT, `_nombre` VARCHAR(45), `_genero` VARCHAR(45), `_descripcion` VARCHAR(250), `_año` VARCHAR(45))  BEGIN
IF _idpelicula = 0 THEN
    INSERT INTO Pelicula (nombre, genero, descripcion, año)
    VALUES (_nombre, _genero, _descripcion, _año);
ELSE
    UPDATE pelicula
	SET
       nombre = _nombre,
       genero = _genero,
       descripcion = _descripcion,
       año = _año
	WHERE idpelicula = _idpelicula;
END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `PeliculaDeleteById` (`_peliculaid` INT)  BEGIN
	DELETE FROM pelicula
    WHERE idpelicula = _peliculaid;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `PeliculaSearchByValue` (`_SearchValue` VARCHAR(45))  BEGIN
	SELECT *
    FROM pelicula
    WHERE nombre LIKE concat('%',_SearchValue,'%'); 

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `PeliculaViewAll` ()  BEGIN
	select *
    from pelicula;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `PeliculaViewById` (`_peliculaid` INT)  BEGIN
	SELECT *
    FROM pelicula
    WHERE peliculaid = _peliculaid;

END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pelicula`
--

CREATE TABLE `pelicula` (
  `idpelicula` int(11) NOT NULL,
  `nombre` varchar(45) DEFAULT NULL,
  `genero` varchar(45) DEFAULT NULL,
  `descripcion` varchar(45) DEFAULT NULL,
  `año` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `pelicula`
--

INSERT INTO `pelicula` (`idpelicula`, `nombre`, `genero`, `descripcion`, `año`) VALUES
(1, 'tenet', 'ciencia ficcion', 'una peli de nolan', '2020'),
(2, 'hereditary', 'terror', 'una familia sufre suscesos extraños despues d', '2018'),
(3, 'midsommar', 'terror', 'culto asesino', '2019'),
(4, 'ted', 'comedia', 'osito ted grosero', '2012'),
(6, 'django sin cadenas', 'western', 'esclavo y aleman', '2012');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `pelicula`
--
ALTER TABLE `pelicula`
  ADD PRIMARY KEY (`idpelicula`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `pelicula`
--
ALTER TABLE `pelicula`
  MODIFY `idpelicula` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
