const db = require('./conn.js');

const exists = "Select * from events";

const generateDB = `
DROP TABLE IF EXISTS events;
CREATE TABLE
	events
(
	id SERIAL NOT NULL
	, start_date timestamp NOT NULL
	, end_date timestamp NOT NULL
    , text VARCHAR(255) NULL
,
CONSTRAINT Pk_Events_EventsID PRIMARY KEY
(
	id
)
);`;



module.exports.createDB = async function createDB() {


    try {
        const exist = await db.pool.query(exists);
        // table already exists
        if (exist) return;
    } catch (error) {
        console.log("Database Exists Already");
    }

    try {
        const res = await db.pool.query(generateDB);
        console.log("Generating a new database");
    } catch (error) {
        console.log("Error while generating\n ", error);
    }


}