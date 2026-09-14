USE db_combatCode;

CREATE USER IF NOT EXISTS 'administrador'@'localhost' IDENTIFIED BY 'admin321$';
GRANT ALL PRIVILEGES ON *.* TO 'administrador'@'localhost' WITH GRANT OPTION;

CREATE USER IF NOT EXISTS 'desarrollo'@'localhost' IDENTIFIED BY 'dev123$';
GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON db_combatCode.* TO 'desarrollo'@'localhost';

FLUSH PRIVILEGES;