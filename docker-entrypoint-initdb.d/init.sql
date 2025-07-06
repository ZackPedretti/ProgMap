CREATE TABLE band (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    lat DOUBLE PRECISION,
    lon DOUBLE PRECISION,
    wiki TEXT,
    lastfm TEXT,
    discogs TEXT
);