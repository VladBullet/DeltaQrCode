ALTER TABLE `deltatra_client_admin`.`ca_set_anvelope` 
DROP COLUMN `Rand`,
DROP COLUMN `Pozitie`,
DROP COLUMN `Interval`,
ADD COLUMN `PozitieId` INT NULL AFTER `SerieSasiu`;

