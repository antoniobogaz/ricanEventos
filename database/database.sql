
use ricanEventos; 

DROP TABLE IF EXISTS user_favorite_events;
DROP TABLE IF EXISTS event_prices;
DROP TABLE IF EXISTS event_address;
DROP TABLE IF EXISTS cities;
DROP TABLE IF EXISTS states;
DROP TABLE IF EXISTS event_categories;
DROP TABLE IF EXISTS categories;
DROP TABLE IF EXISTS events;
DROP TABLE IF EXISTS users; 


CREATE TABLE users(
  id       INT IDENTITY NOT NULL,
  name     VARCHAR(255) NOT NULL, 
  email    VARCHAR(120) NOT NULL,
  password VARCHAR(450) NOT NULL,
  CONSTRAINT pkusers_id PRIMARY KEY(id),
  CONSTRAINT ukusers_email UNIQUE(email)
);

CREATE TABLE events(
  id          INT IDENTITY NOT NULL,
  title       VARCHAR(255) NOT NULL, 
  description TEXT  NULL,
  online      INT NOT NULL DEFAULT 0, 
  link        TEXT     NULL, 
  photo       TEXT NULL, 
  date_time   DATETIME NOT NULL,
  user_id     INT NOT NULL,
  deleted     INT NOT NULL DEFAULT 0, 
  CONSTRAINT pkevents_id PRIMARY KEY(id), 
  CONSTRAINT fkevents_user_id FOREIGN KEY(user_id) REFERENCES users(id)
);

CREATE TABLE categories(
  id          INT IDENTITY NOT NULL, 
  name        VARCHAR(255) NOT NULL, 
  description TEXT             NULL,
  CONSTRAINT pkcategories_id PRIMARY KEY(id), 
);

CREATE TABLE event_categories(
  id_event    INT NOT NULL, 
  id_category INT NOT NULL,
  CONSTRAINT pkevent_categories PRIMARY KEY(id_event, id_category),
  CONSTRAINT fkevent_categories_id_event FOREIGN KEY(id_event) REFERENCES events(id), 
  CONSTRAINT fkevent_categories_id_category FOREIGN KEY(id_category) REFERENCES categories(id)
);


CREATE TABLE states(
   id       INT NOT NULL, 
   nome     VARCHAR(255) NOT NULL, 
   sigla_uf VARCHAR(3) NOT NULL, 
   CONSTRAINT pkstates PRIMARY KEY(id),
);

CREATE TABLE cities(
    id       INT NOT NULL IDENTITY, 
    nome     VARCHAR(255) NOT NULL, 
    ibge     VARCHAR(10)  NOT NULL, 
    state_id INT NOT NULL,
    CONSTRAINT pkcities PRIMARY KEY(id),
    CONSTRAINT fkcities_state_id FOREIGN KEY(state_id) REFERENCES states(id),
);


CREATE TABLE event_address(
 id_event    INT NOT NULL,
 cep         VARCHAR(15) NOT NULL, 
 logradouro  VARCHAR(255) NULL, 
 nr_local    VARCHAR(15) NULL,
 bairro      VARCHAR(66) NULL, 
 longitude   TEXT NULL, 
 latitude    TEXT NULL, 
 complemento TEXT NULL, 
 id_city     INT NOT NULL, 
 CONSTRAINT pkevent_address PRIMARY KEY(id_event), 
 CONSTRAINT fkevent_address_id_event FOREIGN KEY(id_event) REFERENCES events(id),
 CONSTRAINT fkevent_address_id_city FOREIGN KEY(id_city) REFERENCES cities(id)
);

CREATE TABLE event_prices(
 id_event INT NOT NULL,
 value    NUMERIC(10,2) NOT NULL, 
 category VARCHAR(255) NULL, 
 CONSTRAINT pkevent_prices PRIMARY KEY(id_event), 
 CONSTRAINT fkevent_prices_id_event FOREIGN KEY(id_event) REFERENCES events(id),
);


CREATE TABLE user_favorite_events(
  id_event INT NOT NULL, 
  user_id  INT NOT NULL,
  CONSTRAINT pkuser_favorite_events PRIMARY KEY(id_event, user_id),
  CONSTRAINT fkuser_favorite_events_id_event FOREIGN KEY(id_event) REFERENCES events(id), 
  CONSTRAINT fkuser_favorite_events_user_id FOREIGN KEY(user_id) REFERENCES users(id)
);

INSERT INTO categories(name) VALUES 
('Show'),
('Festa'),
('Teatro'),
('Barzinho'),
('Clássicos'),
('Dança'),
('Stand up'),
('Músical'),
('Sertanejo'),
('Rock'),
('Balada'),
('Criança'),
('Infantil');