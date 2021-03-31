CREATE TABLE `ca_appointments` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `appt_index` int DEFAULT NULL,
  `serviciu` varchar(45) DEFAULT NULL,
  `nr_masina` varchar(45) NOT NULL,
  `nume_client` varchar(45) NOT NULL,
  `nr_telefon` varchar(45) NOT NULL,
  `data_appointment` datetime NOT NULL,
  `data_introducere` varchar(45) NOT NULL,
  `ora_inceput` time NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


CREATE TABLE `ca_client` (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_manopera` varchar(50) NOT NULL,
  `nr_fisa` varchar(30) NOT NULL,
  `tip_factura` varchar(40) NOT NULL,
  `nr_factura` varchar(50) NOT NULL DEFAULT '0',
  `data_facturare` date NOT NULL,
  `nefacturat` int NOT NULL,
  `data_insert` datetime NOT NULL,
  `data_instalari` datetime NOT NULL,
  `data_expirare_abonament` date NOT NULL,
  `data_initiala` date NOT NULL,
  `nr_factura_abonament` varchar(100) NOT NULL,
  `zile_expirare_abonament` int NOT NULL,
  `nume_client` varchar(100) NOT NULL,
  `reprezentant_client` varchar(100) NOT NULL,
  `reprezentant_client_telefon` varchar(40) NOT NULL,
  `reprezentant_client_mail` varchar(100) NOT NULL,
  `nr_masina` varchar(20) NOT NULL,
  `serie_sasiu` varchar(50) NOT NULL,
  `an_fabricatie` int NOT NULL,
  `marca_masina` varchar(100) NOT NULL,
  `tip_auto` varchar(200) NOT NULL,
  `km_bord` float NOT NULL,
  `locatie_montaj` varchar(100) NOT NULL,
  `km_efectuati` float NOT NULL DEFAULT '0',
  `vin` varchar(40) NOT NULL,
  `seria_sim` varchar(50) NOT NULL,
  `nr_telefon` varchar(100) NOT NULL,
  `custodie` int NOT NULL,
  `serie_gps` varchar(30) NOT NULL,
  `note_instalare` varchar(255) NOT NULL,
  `instalator` varchar(255) NOT NULL,
  `tip_serviciu` varchar(40) NOT NULL,
  `nr_bucati` varchar(40) NOT NULL,
  `tip_aparat` varchar(255) NOT NULL,
  `user_account` varchar(255) NOT NULL,
  `firma_prestatoare` int NOT NULL,
  `stare` varchar(100) NOT NULL,
  `perioada_contractuala` float NOT NULL DEFAULT '0',
  `cost_abonament` float NOT NULL DEFAULT '0',
  `tip_vanzare` varchar(50) NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=121568 DEFAULT CHARSET=latin1;

CREATE TABLE `ca_log_operatiune` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `AppointmentId` int DEFAULT NULL,
  `AjunsLaTimp` bit(1) NOT NULL,
  `OperatiuneId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `ca_marca` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `Label` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `ca_operatiune_schimb_anvelope` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NumarInmatriculare` varchar(45) NOT NULL,
  `PersoanaContact` varchar(45) NOT NULL,
  `SetIesireId` int DEFAULT NULL,
  `SetIntrareId` int NOT NULL,
  `Observatii` varchar(256) DEFAULT NULL,
  `DataSchimb` datetime NOT NULL,
  `UserId` int DEFAULT NULL,
  `OraInceput` time NOT NULL,
  `OraSfarsit` time DEFAULT NULL,
  `OperatiuneFinalizata` bit(1) NOT NULL,
  `PasCurentOperatiune` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `ca_servicetypes` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `label` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

CREATE TABLE `ca_set_anvelope` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `NumeClient` varchar(50) NOT NULL,
  `NumarTelefon` varchar(45) NOT NULL,
  `NumarInmatriculare` varchar(45) NOT NULL,
  `Rand` varchar(45) NOT NULL,
  `Pozitie` varchar(45) NOT NULL,
  `Interval` varchar(45) NOT NULL,
  `MarcaId` int DEFAULT NULL,
  `NumeSet` varchar(45) NOT NULL,
  `NrBucati` int NOT NULL,
  `Dimensiuni` varchar(100) NOT NULL,
  `Uzura` varchar(100) NOT NULL,
  `TipSezon` varchar(45) NOT NULL,
  `Evaluare` varchar(100) NOT NULL,
  `StatusCurent` varchar(45) NOT NULL,
  `DataUltimaModificare` datetime NOT NULL,
  `Deleted` bit(1) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

CREATE TABLE `ca_users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_account` varchar(100) NOT NULL,
  `user_first_name` varchar(40) NOT NULL,
  `user_last_name` varchar(40) NOT NULL,
  `user_email_address` varchar(60) NOT NULL,
  `user_mobile` varchar(12) NOT NULL,
  `user_phone` varchar(15) DEFAULT NULL,
  `user_password` varchar(120) NOT NULL,
  `user_company` varchar(100) NOT NULL,
  `user_rights` text NOT NULL,
  `user_rights_admin` varchar(4) NOT NULL DEFAULT '0000',
  `user_rights_bycompany` varchar(255) NOT NULL,
  `user_lock` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `user_account` (`user_account`)
) ENGINE=MyISAM AUTO_INCREMENT=70 DEFAULT CHARSET=latin1;

CREATE TABLE `history_anvelope` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `SetAnvelopeId` int NOT NULL,
  `DataModificare` datetime NOT NULL,
  `Changes` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=latin1;



