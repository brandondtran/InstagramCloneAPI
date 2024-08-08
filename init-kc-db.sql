-- Check if the database exists and create it if it does not
DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_database
      WHERE datname = 'keycloak'
   ) THEN
      PERFORM dblink_exec('dbname=postgres', 'CREATE DATABASE keycloak');
END IF;
END
$do$;

-- Connect to the keycloak database
\c keycloak

-- Check if the user exists and create it if it does not
DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_roles
      WHERE rolname = 'admin'
   ) THEN
CREATE ROLE admin LOGIN PASSWORD 'ZNL%fkv0Ox9X';
END IF;
END
$do$;

-- Check if the user exists in the keycloak database and grant privileges if it does not
DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_catalog.pg_user
      WHERE usename = 'admin'
   ) THEN
      CREATE USER admin;
      GRANT ALL PRIVILEGES ON DATABASE keycloak TO admin;
END IF;
END
$do$;
