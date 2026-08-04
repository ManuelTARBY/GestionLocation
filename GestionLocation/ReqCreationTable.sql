/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `gestionlocation`
--

-- --------------------------------------------------------

--
-- Structure de la table `bien`
--

DROP TABLE IF EXISTS `bien`;
CREATE TABLE IF NOT EXISTS `bien` (
  `idbien` int NOT NULL,
  `nombien` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `loyerhc` float(13,2) DEFAULT NULL,
  `charges` float(13,2) DEFAULT NULL,
  `loyercc` float(13,2) DEFAULT NULL,
  `adressebien` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `cpbien` varchar(5) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `villebien` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `chargesimputables` float(13,2) DEFAULT NULL,
  `chargeannuelles` int DEFAULT NULL,
  `bienarchive` tinyint(1) DEFAULT NULL,
  `numerofiscal` varchar(14) COLLATE utf8mb4_unicode_ci NOT NULL,
  `classeDPE` varchar(1) COLLATE utf8mb4_unicode_ci NOT NULL,
  `estimationconsommation` varchar(140) COLLATE utf8mb4_unicode_ci NOT NULL,
  `anneereference` varchar(140) COLLATE utf8mb4_unicode_ci NOT NULL,
  `typehabitat` varchar(21) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `regimejuridique` varchar(14) COLLATE utf8mb4_unicode_ci NOT NULL,
  `periodeconstruction` varchar(4) COLLATE utf8mb4_unicode_ci NOT NULL,
  `superficie` varchar(8) COLLATE utf8mb4_unicode_ci NOT NULL,
  `nbpiece` int NOT NULL,
  `description` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `elementequip` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `autre` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `prodchauff` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `prodeauchaude` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`idbien`),
  UNIQUE KEY `nombien` (`nombien`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- --------------------------------------------------------

--
-- Structure de la table `caution`
--

DROP TABLE IF EXISTS `caution`;
CREATE TABLE IF NOT EXISTS `caution` (
  `idcaution` int NOT NULL,
  `prenomcaution` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `nomcaution` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `nomcompletcaution` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `adressecaution` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `cpcaution` varchar(5) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `villecaution` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `telephonecaution` varchar(14) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `emailcaution` varchar(128) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `cautionarchivee` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idcaution`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- --------------------------------------------------------

--
-- Structure de la table `chargesannuelles`
--

DROP TABLE IF EXISTS `chargesannuelles`;
CREATE TABLE IF NOT EXISTS `chargesannuelles` (
  `idchargeannuelle` int NOT NULL,
  `idbien` int NOT NULL,
  `libelle` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `refFrequence` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `annee` int DEFAULT NULL,
  `montantcharge` float(13,2) DEFAULT NULL,
  `chargeannuelle` float(13,2) DEFAULT NULL,
  `imputable` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idchargeannuelle`),
  KEY `i_fk_chargesannuelles_bien1` (`idbien`),
  KEY `i_fk_chargesannuelles_libelleFreq` (`refFrequence`) USING BTREE,
  KEY `idx_annee` (`annee`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `frequencepaiement`
--

DROP TABLE IF EXISTS `frequencepaiement`;
CREATE TABLE IF NOT EXISTS `frequencepaiement` (
  `libelle` varchar(128) COLLATE utf8mb4_unicode_ci NOT NULL,
  `occurrence` float NOT NULL,
  PRIMARY KEY (`libelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `frequencepaiement`
--

INSERT INTO `frequencepaiement` (`libelle`, `occurrence`) VALUES
('10x par an', 10),
('11x par an', 11),
('3x par an', 3),
('5x par an', 5),
('6x par an', 6),
('7x par an', 7),
('Annuelle', 1),
('Bimensuelle', 6),
('Hebdomadaire', 52),
('Mensuelle', 12),
('Ponctuelle', 1),
('Semestrielle', 2),
('Trimestrielle', 4);

-- --------------------------------------------------------

--
-- Structure de la table `grpedebiens`
--

DROP TABLE IF EXISTS `grpedebiens`;
CREATE TABLE IF NOT EXISTS `grpedebiens` (
  `idgroupe` int NOT NULL,
  `nomdugroupe` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`idgroupe`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `lignegroupe`
--

DROP TABLE IF EXISTS `lignegroupe`;
CREATE TABLE IF NOT EXISTS `lignegroupe` (
  `idgroupe` int NOT NULL,
  `idbien` int NOT NULL,
  PRIMARY KEY (`idgroupe`,`idbien`),
  KEY `i_fk_lignegroupe_grpedebiens1` (`idgroupe`),
  KEY `i_fk_lignegroupe_bien1` (`idbien`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `locataire`
--

DROP TABLE IF EXISTS `locataire`;
CREATE TABLE IF NOT EXISTS `locataire` (
  `idlocataire` int NOT NULL,
  `prenomlocataire` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `nomlocataire` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `nomcompletlocataire` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `adresselocataire` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `cplocataire` varchar(5) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `villelocataire` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `datenaissancelocataire` date DEFAULT NULL,
  `lieunaissancelocataire` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `telephonelocataire` varchar(14) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `emailocataire` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `locatairearchive` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idlocataire`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `location`
--

DROP TABLE IF EXISTS `location`;
CREATE TABLE IF NOT EXISTS `location` (
  `idlocation` int NOT NULL,
  `idbien` int NOT NULL,
  `idcaution` int NOT NULL,
  `idlocataire` int NOT NULL,
  `debutlocation` date DEFAULT NULL,
  `finlocation` date DEFAULT NULL,
  `depotgarantie` float(13,2) DEFAULT NULL,
  `locationarchivee` tinyint(1) DEFAULT NULL,
  `numcontratvisale` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`idlocation`),
  KEY `i_fk_location_bien1` (`idbien`),
  KEY `i_fk_location_caution1` (`idcaution`),
  KEY `i_fk_location_locataire1` (`idlocataire`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `paiement`
--

DROP TABLE IF EXISTS `paiement`;
CREATE TABLE IF NOT EXISTS `paiement` (
  `idpaiement` int NOT NULL AUTO_INCREMENT,
  `idlocation` int NOT NULL,
  `datepaiement` date DEFAULT NULL,
  `montantpaye` float(13,2) DEFAULT NULL,
  `periodefacturee` date DEFAULT NULL,
  `montantdu` float(13,2) DEFAULT NULL,
  `resteapayer` float(13,2) DEFAULT NULL,
  `loyerregle` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idpaiement`),
  KEY `i_fk_paiement_location1` (`idlocation`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

DROP TABLE IF EXISTS `utilisateur`;
CREATE TABLE IF NOT EXISTS `utilisateur` (
  `iduser` int NOT NULL,
  `login` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `pwd` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `prenomuser` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `nomuser` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `adresseuser` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `cpuser` varchar(6) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `villeuser` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `emailuser` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `pwdemail` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `adresseserveursmtp` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `port` int NOT NULL,
  `clientid` varchar(128) COLLATE utf8mb4_unicode_ci NOT NULL,
  `clientsecret` varchar(128) COLLATE utf8mb4_unicode_ci NOT NULL,
  `signature` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Doublure de structure pour la vue `v_ca_annuel`
-- (Voir ci-dessous la vue réelle)
--
DROP VIEW IF EXISTS `v_ca_annuel`;
CREATE TABLE IF NOT EXISTS `v_ca_annuel` (
`Année` int
,`CA` double(19,2)
);

-- --------------------------------------------------------

--
-- Structure de la vue `v_ca_annuel`
--
DROP TABLE IF EXISTS `v_ca_annuel`;

DROP VIEW IF EXISTS `v_ca_annuel`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_ca_annuel`  AS SELECT year(`paiement`.`periodefacturee`) AS `Année`, sum(`paiement`.`montantpaye`) AS `CA` FROM ((`paiement` join `location` on((`paiement`.`idlocation` = `location`.`idlocation`))) join `bien` on((`location`.`idbien` = `bien`.`idbien`))) WHERE (`bien`.`nombien` = 'Maison 4') GROUP BY year(`paiement`.`periodefacturee`) ORDER BY year(`paiement`.`periodefacturee`) ASC  ;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `chargesannuelles`
--
ALTER TABLE `chargesannuelles`
  ADD CONSTRAINT `chargesannuelles_ibfk_1` FOREIGN KEY (`idbien`) REFERENCES `bien` (`idbien`),
  ADD CONSTRAINT `chargesannuelles_ibfk_2` FOREIGN KEY (`refFrequence`) REFERENCES `frequencepaiement` (`libelle`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Contraintes pour la table `lignegroupe`
--
ALTER TABLE `lignegroupe`
  ADD CONSTRAINT `lignegroupe_ibfk_1` FOREIGN KEY (`idgroupe`) REFERENCES `grpedebiens` (`idgroupe`),
  ADD CONSTRAINT `lignegroupe_ibfk_2` FOREIGN KEY (`idbien`) REFERENCES `bien` (`idbien`);

--
-- Contraintes pour la table `location`
--
ALTER TABLE `location`
  ADD CONSTRAINT `location_ibfk_1` FOREIGN KEY (`idbien`) REFERENCES `bien` (`idbien`),
  ADD CONSTRAINT `location_ibfk_2` FOREIGN KEY (`idcaution`) REFERENCES `caution` (`idcaution`),
  ADD CONSTRAINT `location_ibfk_3` FOREIGN KEY (`idlocataire`) REFERENCES `locataire` (`idlocataire`);

--
-- Contraintes pour la table `paiement`
--
ALTER TABLE `paiement`
  ADD CONSTRAINT `paiement_ibfk_1` FOREIGN KEY (`idlocation`) REFERENCES `location` (`idlocation`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
