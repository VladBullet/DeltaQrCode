DELIMITER $$
DROP TRIGGER IF EXISTS before_update_ca_set_anvelope;
CREATE TRIGGER before_update_ca_set_anvelope
BEFORE UPDATE
ON ca_set_anvelope FOR EACH ROW
BEGIN
    DECLARE changes LONGTEXT;
	DECLARE nowDate DATETIME;
    Set changes = '';
      # ------- NumeClient -------
    IF NEW.NumeClient <> OLD.NumeClient THEN
    SET changes = CONCAT(changes, 'NumeClient: ',
                        OLD.NumeClient,
                        ' => ',
                        NEW.NumeClient, '; ');
	END IF;
	# ------- SerieSasiu -------
    IF NEW.SerieSasiu <> OLD.SerieSasiu THEN
    SET changes = CONCAT(changes, 'SerieSasiu: ',
                        OLD.SerieSasiu,
                        ' => ',
                        NEW.SerieSasiu, '; ');
	END IF;
      # ------- NumarTelefon -------
    IF NEW.NumarTelefon <> OLD.NumarTelefon THEN
    SET changes = CONCAT(changes, 'NumarTelefon: ',
                        OLD.NumarTelefon,
                        ' => ',
                        NEW.NumarTelefon, '; ');
	END IF;
    # ------- NumarInmatriculare -------
    IF NEW.NumarInmatriculare <> OLD.NumarInmatriculare THEN
    SET changes = CONCAT(changes, 'NumarInmatriculare: ',
                        OLD.NumarInmatriculare,
                        ' => ',
                        NEW.NumarInmatriculare, '; ');
	END IF;
      # ------- Marca -------
    IF NEW.MarcaId <> OLD.MarcaId THEN
    BEGIN
	DECLARE marcaVeche VARCHAR(50);
	DECLARE marcaNoua VARCHAR(50);
    SELECT Label INTO marcaVeche from ca_marca where Id = OLD.MarcaId order by Id LIMIT 1;
    SELECT Label INTO marcaNoua from ca_marca where Id = NEW.MarcaId order by Id LIMIT 1;
    SET changes = CONCAT(changes, 'Marca : ',
                        marcaVeche,
                        ' => ',
                        marcaNoua, '; ');
	END;
	END IF;
    # ------- Pozitie -------
    IF NEW.PozitieId <> OLD.PozitieId THEN
    BEGIN
	DECLARE pozitieVeche VARCHAR(50);
	DECLARE pozitieNoua VARCHAR(50);
    SELECT Label INTO pozitieVeche from ca_hotel_positions where Id = OLD.PozitieId order by Id LIMIT 1;
    SELECT Label INTO pozitieNoua from ca_hotel_positions where Id = NEW.PozitieId order by Id LIMIT 1;
    SET changes = CONCAT(changes, 'Pozitie : ',
                        pozitieVeche,
                        ' => ',
                        pozitieNoua, '; ');
	END;
	END IF;
       # ------- Flota -------
    IF NEW.FlotaId <> OLD.FlotaId THEN
    BEGIN
	DECLARE flotaVeche VARCHAR(50);
	DECLARE flotaNoua VARCHAR(50);
    SELECT Label INTO flotaVeche from ca_flota where Id = OLD.FlotaId order by Id LIMIT 1;
    SELECT Label INTO flotaNoua from ca_flota where Id = NEW.FlotaId order by Id LIMIT 1;
    SET changes = CONCAT(changes, 'Flota : ',
                        flotaVeche,
                        ' => ',
                        flotaNoua, '; ');
	END;
	END IF;
     # ------- NrBucati -------
    IF NEW.NrBucati <> OLD.NrBucati THEN
    SET changes = CONCAT(changes, 'NrBucati: ',
                        CAST(OLD.NrBucati as CHAR),
                        ' => ',
                        CAST(NEW.NrBucati AS CHAR), '; ');
	END IF;
      # ------- Dimensiuni -------
    IF NEW.Dimensiuni <> OLD.Dimensiuni THEN
    SET changes = CONCAT(changes, 'Dimensiuni: ',
                        OLD.Dimensiuni,
                        ' => ',
                        NEW.Dimensiuni, '; ');
	END IF;
       # ------- Uzura -------
    IF NEW.Uzura <> OLD.Uzura THEN
    SET changes = CONCAT(changes, 'Uzura: ',
                        OLD.Uzura,
                        ' => ',
                        NEW.Uzura, '; ');
	END IF;
       # ------- TipSezon -------
    IF NEW.TipSezon <> OLD.TipSezon THEN
    SET changes = CONCAT(changes, 'TipSezon: ',
                        OLD.TipSezon,
                        ' => ',
                        NEW.TipSezon, '; ');
	END IF;
       # ------- Observatii -------
    IF NEW.Observatii <> OLD.Observatii THEN
    SET changes = CONCAT(changes, 'Observatii: ',
                        OLD.Observatii,
                        ' => ',
                        NEW.Observatii, '; ');
	END IF;
        # ------- StatusCurent -------
    IF NEW.StatusCurent <> OLD.StatusCurent THEN
    SET changes = CONCAT(changes, 'StatusCurent: ',
                        OLD.StatusCurent,
                        ' => ',
                        NEW.StatusCurent, '; ');
	END IF;
         # ------- DataUltimaModificare -------
    IF NEW.DataUltimaModificare <> OLD.DataUltimaModificare THEN
    SET changes = CONCAT(changes, 'DataUltimaModificare: ',
                        CAST(OLD.DataUltimaModificare as char),
                        ' => ',
                        CAST(NEW.DataUltimaModificare as char), '; ');
	END IF;
        # ------- Deleted -------
    IF NEW.Deleted <> OLD.Deleted THEN
    SET changes = CONCAT(changes, 'Deleted: ',
                        OLD.Deleted,
                        ' => ',
                        NEW.Deleted, '; ');
	END IF;
   
    SELECT NOW() into nowDate;
    
    INSERT INTO history_anvelope(SetAnvelopeId, DataModificare, Changes)
	VALUES
    (NEW.id, nowDate, changes);
  
END $$

DELIMITER ;
