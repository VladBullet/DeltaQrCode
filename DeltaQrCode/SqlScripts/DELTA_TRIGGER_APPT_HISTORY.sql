
DELIMITER $$
DROP TRIGGER IF EXISTS before_update_ca_appointments;
CREATE TRIGGER before_update_ca_appointments
BEFORE UPDATE
ON ca_appointments FOR EACH ROW
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
    
      # ------- Serviciu -------
    IF NEW.ServiciuId <> OLD.ServiciuId THEN
    BEGIN
	DECLARE serviciuVechi VARCHAR(50);
	DECLARE serviciuNou VARCHAR(50);
    SELECT Label INTO serviciuVechi from ca_servicetypes where Id = OLD.ServiciuId order by Id LIMIT 1;
    SELECT Label INTO serviciuNou from ca_servicetypes where Id = NEW.ServiciuId order by Id LIMIT 1;
    SET changes = CONCAT(changes, 'Serviciu : ',
                        serviciuVechi,
                        ' => ',
                        serviciuNou, '; ');
	END;
	END IF;
    
	  # ------- DataAppointment -------
    IF NEW.DataAppointment <> OLD.DataAppointment THEN
    SET changes = CONCAT(changes, 'DataAppointment: ',
                        CAST(OLD.DataAppointment as char),
                        ' => ',
                        CAST(NEW.DataAppointment as char), '; ');
	END IF;   
    
	  # ------- DataIntroducere -------
    IF NEW.DataIntroducere <> OLD.DataIntroducere THEN
    SET changes = CONCAT(changes, 'DataIntroducere: ',
                        CAST(OLD.DataIntroducere as char),
                        ' => ',
                        CAST(NEW.DataIntroducere as char), '; ');
	END IF;
    
	  # ------- OraInceput -------
    IF NEW.OraInceput <> OLD.OraInceput THEN
    SET changes = CONCAT(changes, 'OraInceput: ',
                        CAST(OLD.OraInceput as CHAR),
                        ' => ',
                        CAST(NEW.OraInceput AS CHAR), '; ');
	END IF;
    
	  # ------- Deleted -------
    IF NEW.Deleted <> OLD.Deleted THEN
    SET changes = CONCAT(changes, 'Deleted: ',
                        CAST(OLD.Deleted as char),
                        ' => ',
                        CAST(NEW.Deleted as char), '; ');
	END IF;
    
			# ------- Observatii -------
    IF NEW.Observatii <> OLD.Observatii THEN
    SET changes = CONCAT(changes, 'Observatii: ',
                        OLD.Observatii,
                        ' => ',
                        NEW.Observatii, '; ');
	END IF;
    
            # ------- Confirmed -------
    IF NEW.Confirmed <> OLD.Confirmed THEN
    SET changes = CONCAT(changes, 'Confirmed: ',
                        CAST(OLD.Confirmed as char),
                        ' => ',
                        CAST(NEW.Confirmed as char), '; ');
	END IF;
    
			# ------- ConfirmedDate -------
    IF NEW.ConfirmedDate <> OLD.ConfirmedDate THEN
    SET changes = CONCAT(changes, 'ConfirmedDate: ',
                        CAST(OLD.ConfirmedDate as char),
                        ' => ',
                        CAST(NEW.ConfirmedDate as char), '; ');
	END IF;  
    
			# ------- ConfirmedCode -------
    IF NEW.ConfirmedCode <> OLD.ConfirmedCode THEN
    SET changes = CONCAT(changes, 'ConfirmedCode: ',
                        OLD.ConfirmedCode,
                        ' => ',
                        NEW.ConfirmedCode, '; ');
	END IF;
    
			# ------- LastModified -------
    IF NEW.LastModified <> OLD.LastModified THEN
    SET changes = CONCAT(changes, 'LastModified: ',
                        CAST(OLD.LastModified as char),
                        ' => ',
                        CAST(NEW.LastModified as char), '; ');
	END IF;  
    
			# ------- RampId -------
    IF NEW.RampId <> OLD.RampId THEN
    SET changes = CONCAT(changes, 'RampId: ',
                        CAST(OLD.RampId as char),
                        ' => ',
                        CAST(NEW.RampId as char), '; ');
    END IF;  
    
      # ------- EmailClient -------
    IF NEW.EmailClient <> OLD.EmailClient THEN
    SET changes = CONCAT(changes, 'EmailClient: ',
                        OLD.EmailClient,
                        ' => ',
                        NEW.EmailClient, '; ');
    END IF;   
    
		# ------- DurataInMinute -------
    IF NEW.DurataInMinute <> OLD.DurataInMinute THEN
    SET changes = CONCAT(changes, 'DurataInMinute: ',
                        CAST(OLD.DurataInMinute as char),
                        ' => ',
                        CAST(NEW.DurataInMinute as char), '; ');
    END IF; 
    	  # ------- ChangedByClient -------
    IF NEW.ChangedByClient <> OLD.ChangedByClient THEN
    SET changes = CONCAT(changes, 'ChangedByClient: ',
                        CAST(OLD.ChangedByClient as char),
                        ' => ',
                        CAST(NEW.ChangedByClient as char), '; ');
	END IF;

   
    SELECT NOW() into nowDate;
    
    INSERT INTO history_appointments(AppointmentId, DataModificare, Changes)
	VALUES
    (NEW.id, nowDate, changes);
  
END $$

DELIMITER ;