-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Mar 14, 2022 at 11:33 AM
-- Server version: 5.7.31
-- PHP Version: 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mediatek86`
--

-- --------------------------------------------------------

--
-- Table structure for table `abonnement`
--

DROP TABLE IF EXISTS `abonnement`;
CREATE TABLE IF NOT EXISTS `abonnement` (
  `id` varchar(5) NOT NULL,
  `dateFinAbonnement` date DEFAULT NULL,
  `idRevue` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idRevue` (`idRevue`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `abonnement`
--

INSERT INTO `abonnement` (`id`, `dateFinAbonnement`, `idRevue`) VALUES
('10000', '2022-03-31', '10002'),
('10001', '2022-04-10', '10002');

-- --------------------------------------------------------

--
-- Table structure for table `commande`
--

DROP TABLE IF EXISTS `commande`;
CREATE TABLE IF NOT EXISTS `commande` (
  `id` varchar(5) NOT NULL,
  `dateCommande` date DEFAULT NULL,
  `montant` double DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `commande`
--

INSERT INTO `commande` (`id`, `dateCommande`, `montant`) VALUES
('00002', '2022-03-14', 10),
('00003', '2022-03-14', 20),
('10000', '2022-03-14', 10),
('10001', '2022-03-14', 12);

-- --------------------------------------------------------

--
-- Table structure for table `commandedocument`
--

DROP TABLE IF EXISTS `commandedocument`;
CREATE TABLE IF NOT EXISTS `commandedocument` (
  `id` varchar(5) NOT NULL,
  `nbExemplaire` int(11) DEFAULT NULL,
  `idLivreDvd` varchar(10) NOT NULL,
  `idEtatCommande` int(2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idLivreDvd` (`idLivreDvd`),
  KEY `fk_etatcommande` (`idEtatCommande`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `commandedocument`
--

INSERT INTO `commandedocument` (`id`, `nbExemplaire`, `idLivreDvd`, `idEtatCommande`) VALUES
('00002', 2, '00007', 3),
('00003', 5, '00019', 2);

--
-- Triggers `commandedocument`
--
DROP TRIGGER IF EXISTS `updCommandeDocument`;
DELIMITER $$
CREATE TRIGGER `updCommandeDocument` AFTER UPDATE ON `commandedocument` FOR EACH ROW BEGIN
DECLARE nb INTEGER;
DECLARE dateAchat DATE;
DECLARE photo VARCHAR(100);
DECLARE maxNum int(11);

IF(NEW.idEtatCommande = 2) THEN
	SET nb = 0;
	WHILE nb < NEW.nbExemplaire DO
		SELECT dateCommande into dateAchat FROM commande where commande.id = NEW.id;
        SELECT image INTO photo FROM document WHERE document.id = NEW.idLivreDvd;
        SELECT MAX(numero) INTO maxNum FROM exemplaire WHERE id = NEW.idLivreDvd;
        	IF(maxNum IS NULL) THEN
		SET maxNum = 0;
	END IF;
		INSERT INTO exemplaire VALUES (NEW.idLivreDvd, maxNum+1, dateAchat, photo, '00001');
        set nb = nb + 1;
	END WHILE;
END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `document`
--

DROP TABLE IF EXISTS `document`;
CREATE TABLE IF NOT EXISTS `document` (
  `id` varchar(10) NOT NULL,
  `titre` varchar(60) DEFAULT NULL,
  `image` varchar(100) DEFAULT NULL,
  `idRayon` varchar(5) NOT NULL,
  `idPublic` varchar(5) NOT NULL,
  `idGenre` varchar(5) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idRayon` (`idRayon`),
  KEY `idPublic` (`idPublic`),
  KEY `idGenre` (`idGenre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `document`
--

INSERT INTO `document` (`id`, `titre`, `image`, `idRayon`, `idPublic`, `idGenre`) VALUES
('00001', 'Quand sort la recluse', '', 'LV003', '00002', '10014'),
('00002', 'Un pays à l\'aube', '', 'LV001', '00002', '10004'),
('00003', 'Et je danse aussi', '', 'LV002', '00003', '10013'),
('00004', 'L\'armée furieuse', '', 'LV003', '00002', '10014'),
('00005', 'Les anonymes', '', 'LV001', '00002', '10014'),
('00006', 'La marque jaune', '', 'BD001', '00003', '10001'),
('00007', 'Dans les coulisses du musée', '', 'LV001', '00003', '10006'),
('00008', 'Histoire du juif errant', '', 'LV002', '00002', '10006'),
('00009', 'Pars vite et reviens tard', '', 'LV003', '00002', '10014'),
('00010', 'Le vestibule des causes perdues', '', 'LV001', '00002', '10006'),
('00011', 'L\'île des oubliés', '', 'LV002', '00003', '10006'),
('00012', 'La souris bleue', '', 'LV002', '00003', '10006'),
('00013', 'Sacré Pêre Noël', '', 'JN001', '00001', '10001'),
('00014', 'Mauvaise étoile', '', 'LV003', '00003', '10014'),
('00015', 'La confrérie des téméraires', '', 'JN002', '00004', '10014'),
('00016', 'Le butin du requin', '', 'JN002', '00004', '10014'),
('00017', 'Catastrophes au Brésil', '', 'JN002', '00004', '10014'),
('00018', 'Le Routard - Maroc', '', 'DV005', '00003', '10011'),
('00019', 'Guide Vert - Iles Canaries', '', 'DV005', '00003', '10011'),
('00020', 'Guide Vert - Irlande', '', 'DV005', '00003', '10011'),
('00021', 'Les déferlantes', '', 'LV002', '00002', '10006'),
('00022', 'Une part de Ciel', '', 'LV002', '00002', '10006'),
('00023', 'Le secret du janissaire', '', 'BD001', '00002', '10001'),
('00024', 'Pavillon noir', '', 'BD001', '00002', '10001'),
('00025', 'L\'archipel du danger', '', 'BD001', '00002', '10001'),
('00026', 'La planète des singes', '', 'LV002', '00003', '10002'),
('10001', 'Arts Magazine', '', 'PR002', '00002', '10016'),
('10002', 'Alternatives Economiques', '', 'PR002', '00002', '10015'),
('10003', 'Challenges', '', 'PR002', '00002', '10015'),
('10004', 'Rock and Folk', '', 'PR002', '00002', '10016'),
('10005', 'Les Echos', '', 'PR001', '00002', '10015'),
('10006', 'Le Monde', '', 'PR001', '00002', '10018'),
('10007', 'Telerama', '', 'PR002', '00002', '10016'),
('10008', 'L\'Obs', '', 'PR002', '00002', '10018'),
('10009', 'L\'Equipe', '', 'PR001', '00002', '10017'),
('10010', 'L\'Equipe Magazine', '', 'PR002', '00002', '10017'),
('10011', 'Geo', '', 'PR002', '00003', '10016'),
('20001', 'Dune', '', 'DF001', '00002', '10002');

-- --------------------------------------------------------

--
-- Table structure for table `dvd`
--

DROP TABLE IF EXISTS `dvd`;
CREATE TABLE IF NOT EXISTS `dvd` (
  `id` varchar(10) NOT NULL,
  `synopsis` text,
  `realisateur` varchar(20) DEFAULT NULL,
  `duree` int(6) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `dvd`
--

INSERT INTO `dvd` (`id`, `synopsis`, `realisateur`, `duree`) VALUES
('20001', 'L\'histoire de Paul Atreides, jeune homme aussi doué que brillant, voué à connaître un destin hors du commun qui le dépasse totalement. Car s\'il veut préserver l\'avenir de sa famille et de son peuple, il devra se rendre sur la planète la plus dangereuse de l\'univers – la seule à même de fournir la ressource la plus précieuse au monde, capable de décupler la puissance de l\'humanité. Tandis que des forces maléfiques se disputent le contrôle de cette planète, seuls ceux qui parviennent à dominer leur peur pourront survivre…', 'Denis Villeneuve', 156);

--
-- Triggers `dvd`
--
DROP TRIGGER IF EXISTS `insDvd`;
DELIMITER $$
CREATE TRIGGER `insDvd` BEFORE INSERT ON `dvd` FOR EACH ROW BEGIN
DECLARE nb INTEGER;
SELECT COUNT(*) into nb 
FROM livre 
WHERE livre.id = NEW.id;
IF(nb =  1) THEN
SIGNAL SQLSTATE "45000"
SET MESSAGE_TEXT = "opération impossible" ;
END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `etat`
--

DROP TABLE IF EXISTS `etat`;
CREATE TABLE IF NOT EXISTS `etat` (
  `id` char(5) NOT NULL,
  `libelle` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `etat`
--

INSERT INTO `etat` (`id`, `libelle`) VALUES
('00001', 'neuf'),
('00002', 'usagé'),
('00003', 'détérioré'),
('00004', 'inutilisable');

-- --------------------------------------------------------

--
-- Table structure for table `etat_commande`
--

DROP TABLE IF EXISTS `etat_commande`;
CREATE TABLE IF NOT EXISTS `etat_commande` (
  `id` int(2) NOT NULL AUTO_INCREMENT,
  `libelle` varchar(10) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `etat_commande`
--

INSERT INTO `etat_commande` (`id`, `libelle`) VALUES
(1, 'en cours'),
(2, 'livrée'),
(3, 'réglée'),
(4, 'relancée');

-- --------------------------------------------------------

--
-- Table structure for table `exemplaire`
--

DROP TABLE IF EXISTS `exemplaire`;
CREATE TABLE IF NOT EXISTS `exemplaire` (
  `id` varchar(10) NOT NULL,
  `numero` int(11) NOT NULL,
  `dateAchat` date DEFAULT NULL,
  `photo` varchar(100) NOT NULL,
  `idEtat` char(5) NOT NULL,
  PRIMARY KEY (`id`,`numero`),
  KEY `idEtat` (`idEtat`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `exemplaire`
--

INSERT INTO `exemplaire` (`id`, `numero`, `dateAchat`, `photo`, `idEtat`) VALUES
('00001', 3, '2022-02-16', '', '00001'),
('00002', 3, '2022-03-03', '', '00001'),
('00002', 4, '2022-03-03', '', '00001'),
('00002', 5, '2022-03-03', '', '00001'),
('00002', 6, '2022-03-03', '', '00001'),
('00003', 1, '2022-03-09', '', '00002'),
('00007', 2, '2022-03-09', '', '00001'),
('00008', 1, '2022-03-09', '', '00001'),
('00008', 2, '2022-03-09', '', '00001'),
('00008', 3, '2022-03-09', '', '00001'),
('00011', 1, '2022-03-09', '', '00001'),
('00011', 2, '2022-03-09', '', '00001'),
('00015', 1, '2022-03-09', '', '00001'),
('00015', 2, '2022-03-09', '', '00001'),
('00015', 3, '2022-03-09', '', '00001'),
('00017', 2, '2022-03-14', '', '00002'),
('00017', 3, '2022-03-14', '', '00001'),
('00019', 1, '2022-03-09', '', '00001'),
('00019', 2, '2022-03-09', '', '00001'),
('00019', 3, '2022-03-14', '', '00001'),
('00019', 4, '2022-03-14', '', '00001'),
('00019', 5, '2022-03-14', '', '00001'),
('00019', 6, '2022-03-14', '', '00001'),
('00019', 7, '2022-03-14', '', '00001'),
('00020', 1, '2022-03-09', '', '00001'),
('00020', 2, '2022-03-09', '', '00001'),
('00025', 1, '2022-03-11', '', '00001'),
('00025', 2, '2022-03-11', '', '00001'),
('10002', 418, '2021-12-01', '', '00001'),
('10002', 419, '2022-03-20', '', '00001'),
('10002', 420, '2022-04-01', '', '00001'),
('10002', 421, '2022-04-10', '', '00001'),
('10002', 422, '2022-03-17', '', '00001'),
('10007', 3237, '2021-11-23', '', '00001'),
('10007', 3238, '2021-11-30', '', '00001'),
('10007', 3239, '2021-12-07', '', '00001'),
('10007', 3240, '2021-12-21', '', '00001'),
('10011', 506, '2021-04-01', '', '00001'),
('10011', 507, '2021-05-03', '', '00001'),
('10011', 508, '2021-06-05', '', '00001'),
('10011', 509, '2021-07-01', '', '00001'),
('10011', 510, '2021-08-04', '', '00001'),
('10011', 511, '2021-09-01', '', '00001'),
('10011', 512, '2021-10-06', '', '00001'),
('10011', 513, '2021-11-01', '', '00001'),
('10011', 514, '2021-12-01', '', '00001');

-- --------------------------------------------------------

--
-- Table structure for table `genre`
--

DROP TABLE IF EXISTS `genre`;
CREATE TABLE IF NOT EXISTS `genre` (
  `id` varchar(5) NOT NULL,
  `libelle` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `genre`
--

INSERT INTO `genre` (`id`, `libelle`) VALUES
('10000', 'Humour'),
('10001', 'Bande dessinée'),
('10002', 'Science Fiction'),
('10003', 'Biographie'),
('10004', 'Historique'),
('10006', 'Roman'),
('10007', 'Aventures'),
('10008', 'Essai'),
('10009', 'Documentaire'),
('10010', 'Technique'),
('10011', 'Voyages'),
('10012', 'Drame'),
('10013', 'Comédie'),
('10014', 'Policier'),
('10015', 'Presse Economique'),
('10016', 'Presse Culturelle'),
('10017', 'Presse sportive'),
('10018', 'Actualités'),
('10019', 'Fantazy');

-- --------------------------------------------------------

--
-- Table structure for table `livre`
--

DROP TABLE IF EXISTS `livre`;
CREATE TABLE IF NOT EXISTS `livre` (
  `id` varchar(10) NOT NULL,
  `ISBN` varchar(13) DEFAULT NULL,
  `auteur` varchar(20) DEFAULT NULL,
  `collection` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `livre`
--

INSERT INTO `livre` (`id`, `ISBN`, `auteur`, `collection`) VALUES
('00001', '1234569877896', 'Fred Vargas', 'Commissaire Adamsberg'),
('00002', '1236547896541', 'Dennis Lehanne', ''),
('00003', '6541236987410', 'Anne-Laure Bondoux', ''),
('00004', '3214569874123', 'Fred Vargas', 'Commissaire Adamsberg'),
('00005', '3214563214563', 'RJ Ellory', ''),
('00006', '3213213211232', 'Edgar P. Jacobs', 'Blake et Mortimer'),
('00007', '6541236987541', 'Kate Atkinson', ''),
('00008', '1236987456321', 'Jean d\'Ormesson', ''),
('00009', '3,21457E+12', 'Fred Vargas', 'Commissaire Adamsberg'),
('00010', '3,21457E+12', 'Manon Moreau', ''),
('00011', '3,21457E+12', 'Victoria Hislop', ''),
('00012', '3,21457E+12', 'Kate Atkinson', ''),
('00013', '3,21457E+12', 'Raymond Briggs', ''),
('00014', '3,21457E+12', 'RJ Ellory', ''),
('00015', '3,21457E+12', 'Floriane Turmeau', ''),
('00016', '3,21457E+12', 'Julian Press', ''),
('00017', '3,21457E+12', 'Philippe Masson', ''),
('00018', '3,21457E+12', '', 'Guide du Routard'),
('00019', '3,21457E+12', '', 'Guide Vert'),
('00020', '3,21457E+12', '', 'Guide Vert'),
('00021', '3,21457E+12', 'Claudie Gallay', ''),
('00022', '3,21457E+12', 'Claudie Gallay', ''),
('00023', '3,21457E+12', 'Ayrolles - Masbou', 'De cape et de crocs'),
('00024', '3,21457E+12', 'Ayrolles - Masbou', 'De cape et de crocs'),
('00025', '3,21457E+12', 'Ayrolles - Masbou', 'De cape et de crocs'),
('00026', '', 'Pierre Boulle', 'Julliard');

--
-- Triggers `livre`
--
DROP TRIGGER IF EXISTS `insLivre`;
DELIMITER $$
CREATE TRIGGER `insLivre` BEFORE INSERT ON `livre` FOR EACH ROW BEGIN
DECLARE nb INTEGER;
SELECT COUNT(*) into nb 
FROM dvd 
WHERE dvd.id = NEW.id;
IF(nb =  1) THEN
SIGNAL SQLSTATE "45000"
SET MESSAGE_TEXT = "opération impossible" ;
END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `livres_dvd`
--

DROP TABLE IF EXISTS `livres_dvd`;
CREATE TABLE IF NOT EXISTS `livres_dvd` (
  `id` varchar(10) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `livres_dvd`
--

INSERT INTO `livres_dvd` (`id`) VALUES
('00001'),
('00002'),
('00003'),
('00004'),
('00005'),
('00006'),
('00007'),
('00008'),
('00009'),
('00010'),
('00011'),
('00012'),
('00013'),
('00014'),
('00015'),
('00016'),
('00017'),
('00018'),
('00019'),
('00020'),
('00021'),
('00022'),
('00023'),
('00024'),
('00025'),
('00026'),
('20001');

-- --------------------------------------------------------

--
-- Table structure for table `public`
--

DROP TABLE IF EXISTS `public`;
CREATE TABLE IF NOT EXISTS `public` (
  `id` varchar(5) NOT NULL,
  `libelle` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `public`
--

INSERT INTO `public` (`id`, `libelle`) VALUES
('00001', 'Jeunesse'),
('00002', 'Adultes'),
('00003', 'Tous publics'),
('00004', 'Ados');

-- --------------------------------------------------------

--
-- Table structure for table `rayon`
--

DROP TABLE IF EXISTS `rayon`;
CREATE TABLE IF NOT EXISTS `rayon` (
  `id` char(5) NOT NULL,
  `libelle` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `rayon`
--

INSERT INTO `rayon` (`id`, `libelle`) VALUES
('BD001', 'BD Adultes'),
('BL001', 'Beaux Livres'),
('DF001', 'DVD films'),
('DV001', 'Sciences'),
('DV002', 'Maison'),
('DV003', 'Santé'),
('DV004', 'Littérature classique'),
('DV005', 'Voyages'),
('JN001', 'Jeunesse BD'),
('JN002', 'Jeunesse romans'),
('LV001', 'Littérature étrangère'),
('LV002', 'Littérature française'),
('LV003', 'Policiers français étrangers'),
('PR001', 'Presse quotidienne'),
('PR002', 'Magazines');

-- --------------------------------------------------------

--
-- Table structure for table `revue`
--

DROP TABLE IF EXISTS `revue`;
CREATE TABLE IF NOT EXISTS `revue` (
  `id` varchar(10) NOT NULL,
  `empruntable` tinyint(1) DEFAULT NULL,
  `periodicite` varchar(2) DEFAULT NULL,
  `delaiMiseADispo` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `revue`
--

INSERT INTO `revue` (`id`, `empruntable`, `periodicite`, `delaiMiseADispo`) VALUES
('10001', 1, 'MS', 52),
('10002', 1, 'MS', 52),
('10003', 1, 'HB', 15),
('10004', 1, 'HB', 15),
('10005', 0, 'QT', 5),
('10006', 0, 'QT', 5),
('10007', 1, 'HB', 26),
('10008', 1, 'HB', 26),
('10009', 0, 'QT', 5),
('10010', 1, 'HB', 12),
('10011', 1, 'MS', 52);

-- --------------------------------------------------------

--
-- Table structure for table `service`
--

DROP TABLE IF EXISTS `service`;
CREATE TABLE IF NOT EXISTS `service` (
  `IDSERVICE` int(2) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IDSERVICE`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `service`
--

INSERT INTO `service` (`IDSERVICE`, `NOM`) VALUES
(1, 'Administratif'),
(2, 'Médiation culturelle'),
(3, 'Prêt');

-- --------------------------------------------------------

--
-- Table structure for table `utilisateur`
--

DROP TABLE IF EXISTS `utilisateur`;
CREATE TABLE IF NOT EXISTS `utilisateur` (
  `login` varchar(64) DEFAULT NULL,
  `pwd` varchar(64) DEFAULT NULL,
  `idService` int(1) NOT NULL,
  KEY `FK_SERVICE` (`idService`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `utilisateur`
--

INSERT INTO `utilisateur` (`login`, `pwd`, `idService`) VALUES
('Jamy Culture', 'f6d8c409e0e337643788e15a284f2b95c7d31189015880b36d20dbb62f37ae60', 2),
('Karen Administratif', '2250c78a9dad7d9d63413f836767890fb190035806e33ca477af9d798e1e725f', 1),
('Michel Prêt', '17eb75c3fe73ef03a3b737abdae590067dcb4e52f3cc5893b2f49bab8aeca39a', 3);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `abonnement`
--
ALTER TABLE `abonnement`
  ADD CONSTRAINT `abonnement_ibfk_1` FOREIGN KEY (`id`) REFERENCES `commande` (`id`),
  ADD CONSTRAINT `abonnement_ibfk_2` FOREIGN KEY (`idRevue`) REFERENCES `revue` (`id`);

--
-- Constraints for table `commandedocument`
--
ALTER TABLE `commandedocument`
  ADD CONSTRAINT `commandedocument_ibfk_1` FOREIGN KEY (`id`) REFERENCES `commande` (`id`),
  ADD CONSTRAINT `commandedocument_ibfk_2` FOREIGN KEY (`idLivreDvd`) REFERENCES `livres_dvd` (`id`),
  ADD CONSTRAINT `fk_etatcommande` FOREIGN KEY (`idEtatCommande`) REFERENCES `etat_commande` (`id`);

--
-- Constraints for table `document`
--
ALTER TABLE `document`
  ADD CONSTRAINT `document_ibfk_1` FOREIGN KEY (`idRayon`) REFERENCES `rayon` (`id`),
  ADD CONSTRAINT `document_ibfk_2` FOREIGN KEY (`idPublic`) REFERENCES `public` (`id`),
  ADD CONSTRAINT `document_ibfk_3` FOREIGN KEY (`idGenre`) REFERENCES `genre` (`id`);

--
-- Constraints for table `dvd`
--
ALTER TABLE `dvd`
  ADD CONSTRAINT `dvd_ibfk_1` FOREIGN KEY (`id`) REFERENCES `livres_dvd` (`id`);

--
-- Constraints for table `exemplaire`
--
ALTER TABLE `exemplaire`
  ADD CONSTRAINT `exemplaire_ibfk_1` FOREIGN KEY (`id`) REFERENCES `document` (`id`),
  ADD CONSTRAINT `exemplaire_ibfk_2` FOREIGN KEY (`idEtat`) REFERENCES `etat` (`id`);

--
-- Constraints for table `livre`
--
ALTER TABLE `livre`
  ADD CONSTRAINT `livre_ibfk_1` FOREIGN KEY (`id`) REFERENCES `livres_dvd` (`id`);

--
-- Constraints for table `livres_dvd`
--
ALTER TABLE `livres_dvd`
  ADD CONSTRAINT `livres_dvd_ibfk_1` FOREIGN KEY (`id`) REFERENCES `document` (`id`);

--
-- Constraints for table `revue`
--
ALTER TABLE `revue`
  ADD CONSTRAINT `revue_ibfk_1` FOREIGN KEY (`id`) REFERENCES `document` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
